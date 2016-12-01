using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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
        private Stack<string> _tagStack;

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
            //_tagSettings.Add(CenterTag,new TagSettings(true,"","",null,null,0,0));
            _tagSettings.Add(BoldTag,new TagSettings("<b>","</b>"));
            _tagSettings.Add(ItalicsTag,new TagSettings("<i>","</i>"));
            //_tagSettings.Add(MonospaceTag,new TagSettings(true,"","",null,null,0,0));
            //_tagSettings.Add(FontColorTag,new TagSettings(true,"","",null,null,0,0));
            //_tagSettings.Add(FontSizeTag,new TagSettings(true,"","",null,null,0,0));
            //_tagSettings.Add(UnorderedListTag,new TagSettings(true,"","",null,null,0,0));
            //_tagSettings.Add(OrderedListTag,new TagSettings(true,"","",null,null,0,0));
            //_tagSettings.Add(ListItemTag,new TagSettings(true,"","",null,null,0,0));
            //_tagSettings.Add(HeaderTag,new TagSettings(true,"","",null,null,0,0));
            //_tagSettings.Add(SubheaderTag,new TagSettings(true,"","",null,null,0,0));
            _tagSettings.Add(ImageTag,new TagSettings(false,null,null,StartImageHandler,null,1,2));
            _tagSettings.Add(TopicTag,new TagSettings(false,null,null,StartTopicHandler,null,1,1));
            _tagSettings.Add(LinkTag,new TagSettings(true,null,"</a>",StartLinkHandler,null,1,1));
            //_tagSettings.Add(LogicTag,new TagSettings(true,"","",null,null,0,0));
            //_tagSettings.Add(PffTag,new TagSettings(true,"","",null,null,0,0));
            //_tagSettings.Add(HtmlTag,new TagSettings(true,"","",null,null,0,0));
            _tagSettings.Add(DefinitionTag,new TagSettings(false,null,null,StartDefinitionHandler,null,1,1));
        }

        public string CompileForHtml(string[] lines)
        {
            _sb.Append(
                "<html><head>" +
                "<meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\" />" +
                "<title>");

            int titlePos = _sb.Length;

            _sb.Append("</title></head><body>");

            List<string> paragraphs = LinesToParagraphs(lines);

            foreach( string paragraph in paragraphs )
            {
                string html = ProcessParagraph(paragraph);

                if( html.Length > 0 )
                    _sb.AppendFormat("<div>{0}</div>",html);
            }

            if( _title == null )
                throw new Exception("A title must be specified for the topic.");

            _sb.Insert(titlePos,_title);

            _topicCompilerSettings.Title = _title;

            _sb.Append("</body></html>");

            return _sb.ToString();
        }

        private List<string> LinesToParagraphs(string[] lines)
        {
            List<string> paragraphs = new List<string>();
            string constructedLine = "";

            for( int i = 0; i < lines.Length; i++ )
            {
                string line = lines[i].Trim();

                if( line.Length > 0 )
                    constructedLine = constructedLine + ( ( constructedLine.Length > 0 ) ? " " : "" ) + line;

                if( line.Length == 0 || ( i + 1 ) == lines.Length )
                {
                    if( constructedLine.Length > 0 )
                        paragraphs.Add(constructedLine);

                    constructedLine = "";
                }
            }

            return paragraphs;
        }


        private string ProcessParagraph(string paragraph)
        {
            _tagStack = new Stack<string>();

            string html = ProcessText(ref paragraph);

            if( _tagStack.Count > 0 )
                throw new Exception("Missing end tag at the end of the paragraph: " + paragraph);

            return html;
        }

        private string ProcessText(ref string text)
        {
            int startTagPos = text.IndexOf('<');

            if( startTagPos < 0 ) // no more tags so get out
                return text;

            int endTagPos = text.IndexOf('>',startTagPos + 1);

            if( endTagPos < 0 )
                throw new Exception("Invalid tag construction around: " + text.Substring(startTagPos));

            string beforeTagText = text.Substring(0,startTagPos);
            string fullTag = text.Substring(startTagPos + 1,endTagPos - startTagPos - 1).Trim();
            string afterTagText = text.Substring(endTagPos + 1);

            string[] fullTagComponents = Regex.Split(fullTag,@"\s+");

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

                string lastStartTag = _tagStack.Pop();

                if( !lastStartTag.Equals(tag) )
                    throw new Exception(String.Format("End tag </{0}> does not match the last start tag <{1}> around: {2}",tag,lastStartTag,text.Substring(startTagPos)));

                text = afterTagText;

                return beforeTagText;
            }


            // processing start tags
            if( !_tagSettings.ContainsKey(tag) )
                throw new Exception("Invalid tag around: " + text.Substring(startTagPos));

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
                string endTagOutput = EndTag(thisTagSettings,innerText);
                endOutput = innerText + endTagOutput;
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

        private string EndTag(TagSettings thisTagSettings,string innerText)
        {
            if( thisTagSettings.EndTagHandler == null )
                return thisTagSettings.EndHtmlTag;

            else
                return thisTagSettings.EndTagHandler(innerText);
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
