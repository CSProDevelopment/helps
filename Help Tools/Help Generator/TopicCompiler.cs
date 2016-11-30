using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Help_Generator
{
    class TopicCompiler
    {
        private delegate string TagHandlerDelegate(string[] startTagComponents,string endTagInnerText);

        private class TagSettings
        {
            public bool IsPairedTag;
            public string StartHtmlTag;
            public string EndHtmlTag;
            public TagHandlerDelegate TagHandler;
            public int MinTagComponents;
            public int MaxTagComponents;

            public TagSettings(bool isPairedTag,string startHtmlTag,string endHtmlTag,TagHandlerDelegate tagHandler,int minTagComponents,int maxTagComponents)
            {
                IsPairedTag = isPairedTag;
                StartHtmlTag = startHtmlTag;
                EndHtmlTag = endHtmlTag;
                TagHandler = tagHandler;
                MinTagComponents = minTagComponents;
                MaxTagComponents = maxTagComponents;
            }

            public TagSettings(string startHtmlTag,string endHtmlTag) :
                this(true,startHtmlTag,endHtmlTag,null,0,0)
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
            _tagSettings.Add(TagTitle,new TagSettings(true,null,null,TitleHandler,0,0));
            _tagSettings.Add(ContextTag,new TagSettings(false,null,null,ContextHandler,1,1));
            //_tagSettings.Add(IndentTag,new TagSettings(true,"","",null,0,0));
            //_tagSettings.Add(CenterTag,new TagSettings(true,"","",null,0,0));
            _tagSettings.Add(BoldTag,new TagSettings("<b>","</b>"));
            _tagSettings.Add(ItalicsTag,new TagSettings("<i>","</i>"));
            //_tagSettings.Add(MonospaceTag,new TagSettings(true,"","",null,0,0));
            //_tagSettings.Add(FontColorTag,new TagSettings(true,"","",null,0,0));
            //_tagSettings.Add(FontSizeTag,new TagSettings(true,"","",null,0,0));
            //_tagSettings.Add(UnorderedListTag,new TagSettings(true,"","",null,0,0));
            //_tagSettings.Add(OrderedListTag,new TagSettings(true,"","",null,0,0));
            //_tagSettings.Add(ListItemTag,new TagSettings(true,"","",null,0,0));
            //_tagSettings.Add(HeaderTag,new TagSettings(true,"","",null,0,0));
            //_tagSettings.Add(SubheaderTag,new TagSettings(true,"","",null,0,0));
            //_tagSettings.Add(ImageTag,new TagSettings(true,"","",null,0,0));
            //_tagSettings.Add(LinkTag,new TagSettings(true,"","",null,0,0));
            //_tagSettings.Add(LogicTag,new TagSettings(true,"","",null,0,0));
            //_tagSettings.Add(PffTag,new TagSettings(true,"","",null,0,0));
            //_tagSettings.Add(HtmlTag,new TagSettings(true,"","",null,0,0));
            //_tagSettings.Add(DefinitionTag,new TagSettings(true,"","",null,0,0));
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

            string[] tagComponents = Regex.Split(fullTag,@"\s+");

            string tag = tagComponents[0];

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

            int numTagComponents = tagComponents.Length - 1;

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
            if( thisTagSettings.TagHandler == null )
                return thisTagSettings.StartHtmlTag;

            else
                return thisTagSettings.TagHandler(tagComponents,null);
        }

        private string EndTag(TagSettings thisTagSettings,string innerText)
        {
            if( thisTagSettings.TagHandler == null )
                return thisTagSettings.EndHtmlTag;

            else
                return thisTagSettings.TagHandler(null,innerText);
        }


        private string TitleHandler(string[] startTagComponents,string endTagInnerText)
        {
            if( endTagInnerText != null ) // end tag
            {
                if( _title != null )
                    throw new Exception("Only one title can be defined.");

                _title = endTagInnerText;
            }

            return "";
        }

        private string ContextHandler(string[] startTagComponents,string endTagInnerText)
        {
            _topicCompilerSettings.AddContextSensitiveHelp(_preprocessedTopic,startTagComponents[1]);
            return "";
        }


        public const string TagTitle = "title";
        private const string ContextTag = "context";
        private const string IndentTag = "indent";
        private const string CenterTag = "center";
        private const string BoldTag = "b";
        private const string ItalicsTag = "i";
        private const string MonospaceTag = "monospace";
        private const string FontColorTag = "fontcolor";
        private const string FontSizeTag = "fontsize";
        private const string UnorderedListTag = "ul";
        private const string OrderedListTag = "ol";
        private const string ListItemTag = "li";
        private const string HeaderTag = "header";
        private const string SubheaderTag = "subheader";
        private const string ImageTag = "image";
        private const string ImageNoChmAttribute = "nochm";
        private const string LinkTag = "link";
        private const string LogicTag = "logic";
        private const string PffTag = "pff";
        private const string HtmlTag = "html";
        private const string DefinitionTag = "definition";
    }
}
