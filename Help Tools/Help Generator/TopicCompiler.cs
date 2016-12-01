using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Colorizer;

namespace Help_Generator
{
    class TopicCompiler
    {
        private delegate string StartTagHandlerDelegate(string[] startTagComponents);
        private delegate string EndTagHandlerDelegate(string endTagInnerText);

        private class TagSettings
        {
            public bool IsPairedTag;
            public string StartHtmlTag;
            public string EndHtmlTag;
            public StartTagHandlerDelegate StartTagHandler;
            public EndTagHandlerDelegate EndTagHandler;
            public int MinTagComponents;
            public int MaxTagComponents;

            public TagSettings(bool isPairedTag,string startHtmlTag,string endHtmlTag,StartTagHandlerDelegate startTagHandler,
                EndTagHandlerDelegate endTagHandler,int minTagComponents,int maxTagComponents)
            {
                IsPairedTag = isPairedTag;
                StartHtmlTag = startHtmlTag;
                EndHtmlTag = endHtmlTag;
                StartTagHandler = startTagHandler;
                EndTagHandler = endTagHandler;
                MinTagComponents = minTagComponents;
                MaxTagComponents = maxTagComponents;
            }

            public TagSettings(string startHtmlTag,string endHtmlTag) :
                this(true,startHtmlTag,endHtmlTag,null,null,0,0)
            {
            }
        }

        private Dictionary<string,TagSettings> _tagSettings;
        private Dictionary<string,string> _blockTags;

        private Stack<string> _tagStack;
        private bool _inBlockTag;

        private HelpComponents _helpComponents;
        private Preprocessor.TopicPreprocessor _preprocessedTopic;
        private TopicCompilerSettingsInterface _topicCompilerSettings;

        private StringBuilder _sb;
        private string _title;

        public TopicCompiler(HelpComponents helpComponents,Preprocessor.TopicPreprocessor preprocessedTopic,TopicCompilerSettingsInterface topicCompilerSettings)
        {
            _helpComponents = helpComponents;
            _preprocessedTopic = preprocessedTopic;
            _topicCompilerSettings = topicCompilerSettings;
            _sb = new StringBuilder();

            _tagSettings = new Dictionary<string,TagSettings>();
            _tagSettings.Add(TagTitle,new TagSettings(true,null,null,null,EndTitleHandler,0,0));
            _tagSettings.Add(ContextTag,new TagSettings(false,null,null,StartContextHandler,null,1,1));
            //_tagSettings.Add(IndentTag,new TagSettings(true,"","",null,null,0,0));
            _tagSettings.Add(CenterTag,new TagSettings(true,"<div align=\"center\">","</div>",null,null,0,0));
            _tagSettings.Add(BoldTag,new TagSettings("<b>","</b>"));
            _tagSettings.Add(ItalicsTag,new TagSettings("<i>","</i>"));
            //_tagSettings.Add(MonospaceTag,new TagSettings(true,"","",null,null,0,0));
            //_tagSettings.Add(FontColorTag,new TagSettings(true,"","",null,null,0,0));
            _tagSettings.Add(FontSizeTag,new TagSettings(true,null,"</div>",StartFontSizeHandler,null,1,1));
            //_tagSettings.Add(UnorderedListTag,new TagSettings(true,"","",null,null,0,0));
            //_tagSettings.Add(OrderedListTag,new TagSettings(true,"","",null,null,0,0));
            //_tagSettings.Add(ListItemTag,new TagSettings(true,"","",null,null,0,0));
            _tagSettings.Add(HeaderTag,new TagSettings("<div class=\"header_size header\">","</div>"));
            _tagSettings.Add(TitleHeaderTag,new TagSettings(false,null,null,StartTitleHeaderHandler,null,0,0));
            _tagSettings.Add(SubheaderTag,new TagSettings("<div class=\"subheader_size subheader\">","</div>"));
            _tagSettings.Add(ImageTag,new TagSettings(false,null,null,StartImageHandler,null,1,2));
            _tagSettings.Add(TopicTag,new TagSettings(false,null,null,StartTopicHandler,null,1,1));
            _tagSettings.Add(LinkTag,new TagSettings(true,null,"</a>",StartLinkHandler,null,1,1));
            _tagSettings.Add(LogicTag,new TagSettings(true,"","",null,EndLogicHandler,0,0));
            _tagSettings.Add(PffTag,new TagSettings(true,"","",null,EndPffHandler,0,0));
            _tagSettings.Add(HtmlTag,new TagSettings("",""));
            _tagSettings.Add(DefinitionTag,new TagSettings(false,null,null,StartDefinitionHandler,null,1,1));

            _blockTags = new Dictionary<string,string>();
            _blockTags.Add(MakeTag(LogicTag,true),MakeTag(LogicTag,false));
            _blockTags.Add(MakeTag(PffTag,true),MakeTag(PffTag,false));
            _blockTags.Add(MakeTag(HtmlTag,true),MakeTag(HtmlTag,false));
        }

        public string CompileForHtml(string[] lines)
        {
            _sb.Append(
                "<html>" +
                "<head>" +
                "<meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\" />" +
                "<title>");

            int titlePos = _sb.Length;

            _sb.Append("</title>");
            _sb.Append(_topicCompilerSettings.GetTopicStylesheet());
            _sb.Append(
                "</head>" +
                "<body>");

            List<string> paragraphs = LinesToParagraphs(lines);

            foreach( string paragraph in paragraphs )
            {
                string html = ProcessParagraph(paragraph);

                if( html.Length > 0 )
                    _sb.AppendFormat("<div>{0}</div>\n",html);
            }

            if( _title == null )
                throw new Exception("A title must be specified for the topic.");

            _sb.Insert(titlePos,_title);

            _topicCompilerSettings.Title = _title;

            _sb.Append(
                "</body>" +
                "</html>");

            return _sb.ToString();
        }

        public static string MakeTag(string tag,bool startTag)
        {
            return String.Format("<{0}{1}>",startTag ? "" : "/",tag);
        }

        private List<string> LinesToParagraphs(string[] lines)
        {
            List<string> paragraphs = new List<string>();
            string constructedLine = "";
            string inBlockEndTag = null;

            for( int i = 0; i < lines.Length; i++ )
            {
                string line = lines[i];
                bool lineIsBlank = String.IsNullOrWhiteSpace(line);

                if( inBlockEndTag != null )
                    constructedLine = constructedLine + "\r\n" + line.TrimEnd();

                else if( !lineIsBlank )
                    constructedLine = constructedLine + ( ( constructedLine.Length > 0 ) ? " " : "" ) + line.Trim();

                if( inBlockEndTag == null ) // see if there is a block tag specifier
                {
                    foreach( var kp in _blockTags )
                    {
                        if( line.Contains(kp.Key) )
                        {
                            inBlockEndTag = kp.Value;
                            break;
                        }
                    }
                }

                if( inBlockEndTag != null ) // if in a block, see if it has ended
                {
                    if( line.Contains(inBlockEndTag) )
                        inBlockEndTag = null;
                }

                if( ( inBlockEndTag == null && !lineIsBlank ) || ( i + 1 ) == lines.Length )
                {
                    if( constructedLine.Length > 0 )
                        paragraphs.Add(constructedLine);

                    constructedLine = "";
                }
            }

            if( inBlockEndTag != null )
                throw new Exception(String.Format("The block ending tag {0} was not found.",inBlockEndTag));

            return paragraphs;
        }


        private string ProcessParagraph(string paragraph)
        {
            _tagStack = new Stack<string>();
            _inBlockTag = false;

            string html = "";

            while( !String.IsNullOrWhiteSpace(paragraph) )
                html = html + ProcessText(ref paragraph);

            if( _tagStack.Count > 0 )
                throw new Exception(String.Format("Missing end tag {0} at the end of the paragraph: {1}",_tagStack.Pop(),paragraph));

            return html;
        }

        private string ProcessText(ref string text)
        {
            int startTagPos = text.IndexOf('<');

            if( startTagPos < 0 ) // no more tags so get out
            {
                string savedText = text;
                text = "";
                return savedText;
            }

            int endTagPos = text.IndexOf('>',startTagPos + 1);

            if( endTagPos < 0 )
                throw new Exception("Invalid tag construction around: " + text.Substring(startTagPos));

            string beforeTagText = text.Substring(0,startTagPos);
            string fullTag = text.Substring(startTagPos,endTagPos - startTagPos + 1);
            string fullTagContents = fullTag.Substring(1,fullTag.Length - 2).Trim();
            string afterTagText = text.Substring(endTagPos + 1);

            string[] fullTagComponents = Regex.Split(fullTagContents,@"\s+");

            string tag = fullTagComponents[0];

            if( String.IsNullOrWhiteSpace(tag) )
                throw new Exception("Empty tag around: " + text.Substring(startTagPos));

            bool isEndTag = ( tag[0] == '/' );

            // processing end tags
            if( isEndTag )
            {
                tag = tag.Substring(1);

                if( _tagStack.Count == 0 )
                    throw new Exception("End tag without a start tag around: " + text.Substring(startTagPos));

                // if in a block, ignore ending tags unless they are ending the block tag
                if( _inBlockTag && !_tagStack.Peek().Equals(tag) )
                {
                    text = afterTagText;
                    return beforeTagText + fullTag;
                }

                string lastStartTag = _tagStack.Pop();

                if( !lastStartTag.Equals(tag) )
                    throw new Exception(String.Format("End tag </{0}> does not match the last start tag <{1}> around: {2}",tag,lastStartTag,text.Substring(startTagPos)));

                _inBlockTag = false;

                text = afterTagText;
                return beforeTagText;
            }


            // processing start tags

            // only definition tags are allowed while in a block
            if( _inBlockTag && !tag.Equals(DefinitionTag) )
            {
                text = text.Substring(startTagPos + 1);
                return beforeTagText + "<" + ProcessText(ref text);
            }

            if( !_tagSettings.ContainsKey(tag) )
                throw new Exception("Invalid tag around: " + text.Substring(startTagPos));

            // see if this is a block tag
            if( _blockTags.ContainsKey(MakeTag(tag,true)) )
                _inBlockTag = true;

            TagSettings thisTagSettings = _tagSettings[tag];

            if( !thisTagSettings.IsPairedTag ) // check that the tag ends correctly
            {
                if( fullTagComponents.Length == 1 || !fullTagComponents[fullTagComponents.Length - 1].Equals("/") )
                    throw new Exception(String.Format("The tag {0} is not paired and must end with / around: {1}",tag,text.Substring(startTagPos)));
            }

            int numTagComponents = fullTagComponents.Length - 1 - ( thisTagSettings.IsPairedTag ? 0 : 1 );
            string[] tagComponents = new string[numTagComponents];
            Array.Copy(fullTagComponents,1,tagComponents,0,numTagComponents);

            if( numTagComponents < thisTagSettings.MinTagComponents )
                throw new Exception(String.Format("The tag {0} must have at least {1} argument{2} around: {3}",
                    tag,thisTagSettings.MinTagComponents,(thisTagSettings.MinTagComponents == 1 ) ? "" : "s",text.Substring(startTagPos)));

            if( numTagComponents > thisTagSettings.MaxTagComponents )
                throw new Exception(String.Format("The tag {0} must have at most {1} argument{2} around: {3}",
                    tag,thisTagSettings.MaxTagComponents,(thisTagSettings.MaxTagComponents == 1 ) ? "" : "s",text.Substring(startTagPos)));

            string startOutput = StartTag(thisTagSettings,tagComponents);
            string endOutput = "";

            if( thisTagSettings.IsPairedTag )
            {
                _tagStack.Push(tag);

                string innerText = ProcessText(ref afterTagText);
                endOutput = EndTag(thisTagSettings,innerText);
            }

            string html = beforeTagText + startOutput + endOutput + ProcessText(ref afterTagText);

            text = afterTagText;
            return html;
        }

        private string StartTag(TagSettings thisTagSettings,string[] tagComponents)
        {
            if( thisTagSettings.StartTagHandler == null )
                return thisTagSettings.StartHtmlTag;

            else
                return thisTagSettings.StartTagHandler(tagComponents);
        }

        private string EndTag(TagSettings thisTagSettings,string endTagInnerText)
        {
            if( thisTagSettings.EndTagHandler == null )
                return endTagInnerText + thisTagSettings.EndHtmlTag;

            else
                return thisTagSettings.EndTagHandler(endTagInnerText);
        }


        private string EndTitleHandler(string endTagInnerText)
        {
            if( _title != null )
                throw new Exception("Only one title can be defined.");

            _title = endTagInnerText;

            return "";
        }

        private string StartContextHandler(string[] startTagComponents)
        {
            _topicCompilerSettings.AddContextSensitiveHelp(_preprocessedTopic,startTagComponents[0]);
            return "";
        }

        private string StartFontSizeHandler(string[] startTagComponents)
        {
            if( startTagComponents[0].Equals(HeaderTag) || startTagComponents[0].Equals(SubheaderTag) )
                return String.Format("<div class=\"{0}_size\">",startTagComponents[0]);

            try
            {
                double em = double.Parse(startTagComponents[0]);
                return String.Format("<div style=\"font-size: {0}em;\">",em);
            }

            catch( Exception )
            {
                throw new Exception("The fontsize tag takes either: header, subheader, or an em value.");
            }
        }

        private string StartTitleHeaderHandler(string[] startTagComponents)
        {
            if( _title == null )
                throw new Exception("You cannot use a title header until a title has been defined.");

            return String.Format("<div class=\"header_size header\">{0}</div>",_title);
        }

        private string StartImageHandler(string[] startTagComponents)
        {
            if( startTagComponents.Length == 2 )
            {
                if( startTagComponents[0].Equals(ImageNoChmAttribute) )
                {
                    if( _topicCompilerSettings.ChmCreationMode )
                        return "";
                }

                else
                    throw new Exception(String.Format("Unknown image tags {0} / {1}",startTagComponents[0],startTagComponents[1]));
            }

            Preprocessor.ImagePreprocessor image = _helpComponents.preprocessor.GetImage(startTagComponents[startTagComponents.Length - 1]);

            return String.Format("<img src=\"{0}\" />",_topicCompilerSettings.GetHtmlFilename(image));
        }

        private string StartTopicHandler(string[] startTagComponents)
        {
            Preprocessor.TopicPreprocessor topic = _helpComponents.preprocessor.GetTopic(startTagComponents[0]);
            return String.Format("<a href=\"{0}\">{1}</a>",_topicCompilerSettings.GetHtmlFilename(topic),topic.Title);
        }

        private string StartLinkHandler(string[] startTagComponents)
        {
            string url;

            try
            {
                Preprocessor.TopicPreprocessor topic = _helpComponents.preprocessor.GetTopic(startTagComponents[0]);
                url = _topicCompilerSettings.GetHtmlFilename(topic);
            }

            catch( Exception ) // the topic wasn't found so it should be a URL
            {
                url = startTagComponents[0];

                if( url.IndexOf("http") < 0 )
                    throw new Exception(String.Format("The URL {0} is invalid and must begin with http",url));
            }

            return String.Format("<a href=\"{0}\">",url);
        }

        private string TrimOnlyOneNewlineBothEnds(string text)
        {
            int startTrimChars = ( text.Length >= 2 && text[0] == '\r' && text[1] == '\n' ) ? 2 : 0;
            int endTrimChars = ( text.Length >= ( 2 + startTrimChars ) && text[text.Length - 2] == '\r' && text[text.Length - 1] == '\n' ) ? 2 : 0;
            return text.Substring(startTrimChars,text.Length - startTrimChars - endTrimChars);
        }

        private string EndLogicHandler(string endTagInnerText)
        {
            if( _helpComponents._logicColorizer == null )
                _helpComponents._logicColorizer = new LogicColorizer(new LogicColorizerHtmlHelp());

            return _helpComponents._logicColorizer.Colorize(TrimOnlyOneNewlineBothEnds(endTagInnerText));
        }

        private string EndPffHandler(string endTagInnerText)
        {
            if( _helpComponents._pffColorizer == null )
                _helpComponents._pffColorizer = new Colorizer.PffColorizer(new PffColorizerHtmlHelp());

            return _helpComponents._pffColorizer.Colorize(TrimOnlyOneNewlineBothEnds(endTagInnerText));
        }

        private string StartDefinitionHandler(string[] startTagComponents)
        {
            return _helpComponents.settings.GetDefinition(startTagComponents[0],_helpComponents.preprocessor);
        }


        public const string TagTitle = "title";
        public const string ContextTag = "context";
        public const string IndentTag = "indent";
        public const string CenterTag = "center";
        public const string BoldTag = "b";
        public const string ItalicsTag = "i";
        public const string MonospaceTag = "monospace";
        public const string FontColorTag = "fontcolor";
        public const string FontSizeTag = "fontsize";
        public const string UnorderedListTag = "ul";
        public const string OrderedListTag = "ol";
        public const string ListItemTag = "li";
        public const string HeaderTag = "header";
        public const string TitleHeaderTag = "titleheader";
        public const string SubheaderTag = "subheader";
        public const string ImageTag = "image";
        public const string ImageNoChmAttribute = "nochm";
        public const string TopicTag = "topic";
        public const string LinkTag = "link";
        public const string LogicTag = "logic";
        public const string PffTag = "pff";
        public const string HtmlTag = "html";
        public const string DefinitionTag = "definition";
    }
}
