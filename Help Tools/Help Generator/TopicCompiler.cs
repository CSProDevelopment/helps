using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Colorizer;

namespace Help_Generator
{
    class TopicCompiler : LogicColorizerHtmlHelp.GetHtmlFilenameForKeywordInterface
    {
        private delegate string StartTagHandlerDelegate(string[] startTagComponents);
        private delegate string EndTagHandlerDelegate(string endTagInnerText);

        private class TagSettings
        {
            public bool IsPairedTag;
            public object StartHtmlObject;
            public object EndHtmlObject;
            public int MinTagComponents;
            public int MaxTagComponents;

            public TagSettings(bool isPairedTag,object startHtmlObject,object endHtmlObject,int minTagComponents,int maxTagComponents)
            {
                IsPairedTag = isPairedTag;
                StartHtmlObject = startHtmlObject;
                EndHtmlObject = endHtmlObject;
                MinTagComponents = minTagComponents;
                MaxTagComponents = maxTagComponents;
            }

            public TagSettings(string startHtmlTag,string endHtmlTag) :
                this(true,startHtmlTag,endHtmlTag,0,0)
            {
            }
        }

        private class TableSettings
        {
            public int Columns;
            public bool HasHeader;
            public bool HasNoWrap;
            public bool HasCenter;
            public bool HasBorder;
            public int CellCount;

            public TableSettings(int columns,bool hasHeader,bool hasNoWrap,bool hasCenter,bool hasBorder)
            {
                Columns = columns;
                HasHeader = hasHeader;
                HasNoWrap = hasNoWrap;
                HasCenter = hasCenter;
                HasBorder = hasBorder;
                CellCount = 0;
            }
        }

        private Dictionary<string,TagSettings> _tagSettings;
        private Dictionary<string,string> _blockTags;

        private Stack<string> _tagStack;
        private bool _inBlockTag;
        private Stack<string> _filledEndTagStack;
        private Stack<TableSettings> _tableStack;

        private HelpComponents _helpComponents;
        private Preprocessor.TopicPreprocessor _preprocessedTopic;
        private TopicCompilerSettingsInterface _topicCompilerSettings;

        private StringBuilder _sb;
        private string _title;
        private bool _titleIsHeader;

        public TopicCompiler(HelpComponents helpComponents,Preprocessor.TopicPreprocessor preprocessedTopic,TopicCompilerSettingsInterface topicCompilerSettings)
        {
            _helpComponents = helpComponents;
            _preprocessedTopic = preprocessedTopic;
            _topicCompilerSettings = topicCompilerSettings;
            _sb = new StringBuilder();

            _tagSettings = new Dictionary<string,TagSettings>();
            _tagSettings.Add(TagTitle,new TagSettings(true,(StartTagHandlerDelegate)StartTitleHandler,(EndTagHandlerDelegate)EndTitleHandler,0,1));
            _tagSettings.Add(ContextTag,new TagSettings(false,(StartTagHandlerDelegate)StartContextHandler,null,1,Int32.MaxValue));
            _tagSettings.Add(IndentTag,new TagSettings(true,(StartTagHandlerDelegate)StartIndentHandler,(EndTagHandlerDelegate)EndTagHandlerUsingFilledEndTagStack,0,1));
            _tagSettings.Add(CenterTag,new TagSettings("<div align=\"center\">","</div>"));
            _tagSettings.Add(BoldTag,new TagSettings("<b>","</b>"));
            _tagSettings.Add(ItalicsTag,new TagSettings("<i>","</i>"));
            _tagSettings.Add(FontTag,new TagSettings(true,(StartTagHandlerDelegate)StartFontHandler,"</span>",1,3));
            _tagSettings.Add(ListTag,new TagSettings(true,(StartTagHandlerDelegate)StartListHandler,(EndTagHandlerDelegate)EndTagHandlerUsingFilledEndTagStack,0,1));
            _tagSettings.Add(ListItemTag,new TagSettings("<li>","</li>"));
            _tagSettings.Add(HeaderTag,new TagSettings("<div class=\"header_size header\">","</div>"));
            _tagSettings.Add(SubheaderTag,new TagSettings("<div class=\"subheader_size subheader\">","</div>"));
            _tagSettings.Add(ImageTag,new TagSettings(false,(StartTagHandlerDelegate)StartImageHandler,null,1,2));
            _tagSettings.Add(TopicTag,new TagSettings(false,(StartTagHandlerDelegate)StartTopicHandler,null,1,1));
            _tagSettings.Add(LinkTag,new TagSettings(true,(StartTagHandlerDelegate)StartLinkHandler,"</a>",1,1));
            _tagSettings.Add(TableTag,new TagSettings(true,(StartTagHandlerDelegate)StartTableHandler,(EndTagHandlerDelegate)EndTableHandler,1,4));
            _tagSettings.Add(TableCellTag,new TagSettings(true,(StartTagHandlerDelegate)StartTableCellHandler,(EndTagHandlerDelegate)EndTableCellHandler,0,1));
            _tagSettings.Add(SeeAlsoTag,new TagSettings(false,(StartTagHandlerDelegate)StartSeeAlsoHandler,null,1,Int32.MaxValue));
            _tagSettings.Add(LogicTag,new TagSettings(true,"",(EndTagHandlerDelegate)EndLogicHandler,0,0));
            _tagSettings.Add(LogicSyntaxTag,new TagSettings(true,"",(EndTagHandlerDelegate)EndLogicSyntaxHandler,0,0));
            _tagSettings.Add(LogicColorTag,new TagSettings(true,"",(EndTagHandlerDelegate)EndLogicColorHandler,0,0));
            _tagSettings.Add(LogicArgumentTag,new TagSettings("<span class=\"code_colorization_argument\">","</span>"));
            _tagSettings.Add(LogicTableTag,new TagSettings(false,(StartTagHandlerDelegate)StartLogicTableHandler,null,1,1));
            _tagSettings.Add(PffTag,new TagSettings(true,"",(EndTagHandlerDelegate)EndPffHandler,0,0));
            _tagSettings.Add(PffColorTag,new TagSettings(true,"",(EndTagHandlerDelegate)EndPffColorHandler,0,0));
            _tagSettings.Add(HtmlTag,new TagSettings("",""));

            _blockTags = new Dictionary<string,string>();
            _blockTags.Add(MakeTag(LogicTag,true),MakeTag(LogicTag,false));
            _blockTags.Add(MakeTag(LogicSyntaxTag,true),MakeTag(LogicSyntaxTag,false));
            _blockTags.Add(MakeTag(PffTag,true),MakeTag(PffTag,false));
            _blockTags.Add(MakeTag(HtmlTag,true),MakeTag(HtmlTag,false));
        }

        public string CompileForHtml(string[] lines)
        {
            _sb.Append(
                "<html>" +
                "<head>" +
                "<meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\">" +
                "<title>");

            int titlePos = _sb.Length;

            _sb.Append("</title>");
            _sb.Append(_topicCompilerSettings.GetTopicStylesheet());
            _sb.Append(
                "</head>" +
                "<body>" +
                _topicCompilerSettings.GetStartingHtml(_preprocessedTopic));

            List<string> paragraphs = LinesToParagraphs(lines);

            foreach( string paragraph in paragraphs )
            {
                string html = ProcessParagraph(paragraph);

                if( html.Length > 0 )
                    _sb.AppendFormat("<div class=\"paragraph\">{0}</div>\n",html);
            }

            if( _title == null )
                throw new Exception("A title must be specified for the topic.");

            _sb.Insert(titlePos,_topicCompilerSettings.GetTitle(_title));

            _topicCompilerSettings.Title = _title;

            _sb.Append(
                _topicCompilerSettings.EndingHtml +
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
                string line = lines[i].TrimEnd();
                bool lineIsBlank = String.IsNullOrWhiteSpace(line);

                if( inBlockEndTag != null )
                    constructedLine = constructedLine + "\r\n" + line;

                else if( !lineIsBlank )
                    constructedLine = constructedLine + ( ( constructedLine.Length > 0 ) ? NewLineMarker : "" ) + line.TrimStart();

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

                if( ( inBlockEndTag == null && lineIsBlank ) || ( i + 1 ) == lines.Length )
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
            _filledEndTagStack = new Stack<string>();
            _tableStack = new Stack<TableSettings>();

            string preprocessedParagraph = ProcessDefinitions(paragraph);
            string text = "";

            try
            {
                while( !String.IsNullOrWhiteSpace(preprocessedParagraph) )
                    text = text + ProcessText(ref preprocessedParagraph);

                if( _tagStack.Count > 0 )
                    throw new Exception(String.Format("Missing end tag {0} at the end of the paragraph.",_tagStack.Pop()));
            }

            catch( Exception exception )
            {
                throw new Exception(String.Format("Error \"{0}\" processing: {1}",exception.Message,paragraph));
            }

            return ReplaceNewlinesWithBreaks(text);
        }

        private string ReplaceNewlinesWithBreaks(string text)
        {
            int newlinePos = -1;

            // if a newline immediately follows or preceeds a tag, it won't be considered a break
            while( ( ( newlinePos + 1 ) < text.Length ) && ( ( newlinePos = text.IndexOf(NewLineMarker,newlinePos + 1) ) >= 0 ) )
            {
                if( ( newlinePos > 0 && text[newlinePos - 1] == '>' ) ||
                    ( ( newlinePos + NewLineMarker.Length ) < text.Length && text[newlinePos + NewLineMarker.Length] == '<' ) )
                {
                    text = text.Remove(newlinePos,NewLineMarker.Length);
                }
            }

            return text.Replace(NewLineMarker,"<br />\n");
        }

        private string ProcessDefinitions(string text)
        {
            int startTagPos = text.IndexOf('<');

            if( startTagPos < 0 )
                return text;

            int endTagPos = text.IndexOf('>',startTagPos + 1);

            if( endTagPos > startTagPos )
            {
                string beforeTagText = text.Substring(0,startTagPos);
                string fullTag = text.Substring(startTagPos,endTagPos - startTagPos + 1);
                string fullTagContents = fullTag.Substring(1,fullTag.Length - 2).Trim();

                string[] fullTagComponents = Regex.Split(fullTagContents,@"\s+");

                if( fullTagComponents.Length == 3 && fullTagComponents[0].Equals(DefinitionTag) && fullTagComponents[2].Equals("/") )
                    return beforeTagText + _helpComponents.settings.GetDefinition(fullTagComponents[1],_helpComponents.preprocessor) + ProcessDefinitions(text.Substring(endTagPos + 1));

                else
                    return beforeTagText + "<" + ProcessDefinitions(text.Substring(startTagPos + 1));
            }

            return text;
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
            bool tagIsBlank = String.IsNullOrWhiteSpace(tag);

            if( tagIsBlank && !_inBlockTag )
                throw new Exception("Empty tag around: " + text.Substring(startTagPos));

            bool isEndTag = ( !tagIsBlank && tag[0] == '/' );

            // processing end tags
            if( isEndTag )
            {
                tag = tag.Substring(1);

                if( _tagStack.Count == 0 )
                    throw new Exception("End tag without a start tag around: " + text.Substring(startTagPos));

                // if in a block, ignore ending tags unless they are ending the block tag
                if( _inBlockTag && !_tagStack.Peek().Equals(tag) )
                {
                    string blockHtml = beforeTagText + fullTag + ProcessText(ref afterTagText);

                    text = afterTagText;

                    return blockHtml;
                }

                string lastStartTag = _tagStack.Pop();

                if( !lastStartTag.Equals(tag) )
                    throw new Exception(String.Format("End tag </{0}> does not match the last start tag <{1}> around: {2}",tag,lastStartTag,text.Substring(startTagPos)));

                _inBlockTag = false;

                text = afterTagText;
                return beforeTagText;
            }


            // processing start tags

            // tags are ignored while in a block
            if( _inBlockTag )
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

        private string StartTag(TagSettings thisTagSettings,string[] startTagComponents)
        {
            string text = "";

            if( thisTagSettings.StartHtmlObject != null )
            {
                if( thisTagSettings.StartHtmlObject is string )
                    text = (string)thisTagSettings.StartHtmlObject;

                else if( thisTagSettings.StartHtmlObject is StartTagHandlerDelegate )
                    text = ((StartTagHandlerDelegate)thisTagSettings.StartHtmlObject)(startTagComponents);
            }

            return text;
        }

        private string EndTag(TagSettings thisTagSettings,string endTagInnerText)
        {
            string text = "";

            if( thisTagSettings.EndHtmlObject != null )
            {
                if( thisTagSettings.EndHtmlObject is string )
                    text = endTagInnerText + (string)thisTagSettings.EndHtmlObject;

                else if( thisTagSettings.EndHtmlObject is EndTagHandlerDelegate )
                    text = ((EndTagHandlerDelegate)thisTagSettings.EndHtmlObject)(endTagInnerText);
            }

            else
                text = endTagInnerText;

            return text;
        }


        private string EndTagHandlerUsingFilledEndTagStack(string endTagInnerText)
        {
            return endTagInnerText + _filledEndTagStack.Pop();
        }


        private string StartTitleHandler(string[] startTagComponents)
        {
            if( _title != null )
                throw new Exception("Only one title can be defined.");

            _titleIsHeader = true;

            if( startTagComponents.Length == 1 )
            {
                if( startTagComponents[0].Equals(NoHeaderAttribute) )
                    _titleIsHeader = false;

                else
                    throw new Exception("Unknown title tag " + startTagComponents[0]);
            }

            return "";
        }

        private string EndTitleHandler(string endTagInnerText)
        {
            _title = endTagInnerText;
            return _titleIsHeader ? String.Format("<span class=\"header_size header\">{0}</span>",_title) : "";
        }

        private string StartContextHandler(string[] startTagComponents)
        {
            foreach( string startTagComponent in startTagComponents )
            {
                bool useIfExists = ( startTagComponent.Length > 0 && startTagComponent[0] == '!' );
                string context = useIfExists ? startTagComponent.Substring(1) : startTagComponent;

                if( !useIfExists )
                    _helpComponents.settings.CheckContextExists(context,_helpComponents.preprocessor);

                _topicCompilerSettings.AddContextSensitiveHelp(_preprocessedTopic,context);
            }

            return "";
        }

        private string StartIndentHandler(string[] startTagComponents)
        {
            int numIndents = 1;

            if( ( startTagComponents.Length == 1 && !Int32.TryParse(startTagComponents[0],out numIndents) ) || numIndents < 1 )
                throw new Exception("The indent tag has an invalid attribute: " + startTagComponents[0]);

            string text = "";
            string endText = "";

            for( int i = 0; i < numIndents; i++ )
            {
                text = text + "<div class=\"indent\">";
                endText = endText + "</div>";
            }

            _filledEndTagStack.Push(endText);

            return text;
        }

        private string StartFontHandler(string[] startTagComponents)
        {
            string classes = "";
            string styles = "";
            int numTypes = 0;
            int numSizes = 0;
            int numColors = 0;

            foreach( string startTagComponent in startTagComponents )
            {
                double em = 0;

                // check for the font type
                if( startTagComponent.Equals(FontMonospaceAttribute) )
                {
                    classes = classes + "monospace ";
                    numTypes++;
                }

                // check for the font size
                else if( startTagComponent.Equals(HeaderTag) || startTagComponent.Equals(SubheaderTag) )
                {
                    classes = classes + String.Format("{0}_size ",startTagComponent);
                    numSizes++;
                }

                else if( double.TryParse(startTagComponent,out em) )
                {
                    styles = styles + String.Format("font-size: {0}em; ",em);
                    numSizes++;
                }

                // check for font color
                else
                {
                    try
                    {
                        Color color = ColorTranslator.FromHtml(startTagComponent);
                        styles = styles + String.Format("color: {0}; ",ColorTranslator.ToHtml(color));
                        numColors++;
                    }

                    catch( Exception )
                    {
                        throw new Exception("The font tag has an invalid attribute: " + startTagComponent);
                    }
                }

                if( numTypes > 1 || numSizes > 1 || numColors > 1)
                    throw new Exception("The font tag cannot have more than one type, size, or color attribute.");
            }

            classes = ( classes.Length > 0 ) ? String.Format(" class=\"{0}\"",classes) : "";
            styles = ( styles.Length > 0 ) ? String.Format(" style=\"{0}\"",styles) : "";

            return String.Format("<span{0}{1}>",classes,styles);
        }

        private string StartListHandler(string[] startTagComponents)
        {
            bool unorderedList = true;

            if( ( startTagComponents.Length == 1 ) )
            {
                if( startTagComponents[0].Equals(OrderedAttribute) )
                    unorderedList = false;

                else
                    throw new Exception("The list tag has an invalid attribute: " + startTagComponents[0]);
            }

            _filledEndTagStack.Push(unorderedList ? "</ul>" : "</ol>");

            return unorderedList ? "<ul>" : "<ol>";
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

            // for accessibility, set the title to the name of the image, replacing underscores with spaces
            string imageTitle = Path.GetFileNameWithoutExtension(image.Filename).Replace('_',' ');

            return String.Format("<img src=\"{0}\" title=\"{1}\"/>",_topicCompilerSettings.GetHtmlFilename(image),imageTitle);
        }

        private string StartTopicHandler(string[] startTagComponents)
        {
            Preprocessor.TopicPreprocessor topic = _helpComponents.preprocessor.GetTopic(startTagComponents[0]);
            return String.Format("<a href=\"{0}\">{1}</a>",_topicCompilerSettings.GetHtmlFilename(topic),topic.Title);
        }

        private string StartLinkHandler(string[] startTagComponents)
        {
            string url = startTagComponents[0];
            string extras = "";

            try
            {
                if( Path.GetExtension(url).Equals(Constants.TopicExtension,StringComparison.InvariantCultureIgnoreCase) )
                {
                    int colonPos = url.IndexOf("::");

                    if( colonPos >= 0 ) // a link to another help document
                    {
                        string document = url.Substring(0,colonPos);
                        string topic = url.Substring(colonPos + 2);

                        _helpComponents.preprocessor.CheckExternalTopic(document,topic);
                        url = _topicCompilerSettings.ConstructLink(LinkType.ExternalTopic,new string[] { document, topic },ref extras);
                    }

                    else
                    {
                        Preprocessor.TopicPreprocessor topic = _helpComponents.preprocessor.GetTopic(startTagComponents[0]);
                        url = _topicCompilerSettings.ConstructLink(LinkType.Topic,topic,ref extras);
                    }
                }

                else
                    url = _topicCompilerSettings.ConstructLink(LinkType.Other,url,ref extras);
            }

            catch( Exception exception ) // the topic wasn't found
            {
                throw new Exception(String.Format("The topic {0} is invalid ({1})",url,exception.Message));
            }

            return String.Format("<a href=\"{0}\" {1}>",url,extras);
        }

        private string StartTableHandler(string[] startTagComponents)
        {
            int columns = 0;
            bool hasHeader = false;
            bool hasNoWrap = false;
            bool hasCenter = false;
            bool hasBorder = false;

            foreach( string startTagComponent in startTagComponents )
            {
                if( !hasHeader && startTagComponent.Equals(HeaderTag) )
                    hasHeader = true;

                else if( !hasNoWrap && startTagComponent.Equals(NoWrapAttribute) )
                    hasNoWrap = true;

                else if( !hasCenter && startTagComponent.Equals(CenterTag) )
                    hasCenter = true;

                else if( !hasBorder && startTagComponent.Equals(BorderAttribute) )
                    hasBorder = true;
 
                else if( columns == 0 && Int32.TryParse(startTagComponent,out columns) )
                {
                    if( columns < 1 )
                        throw new Exception("The number of columns in a table must be a positive integer");
                }

                else
                    throw new Exception("The table tag has an invalid attribute: " + startTagComponent);
            }

            if( columns == 0 )
                throw new Exception("The number of columns in a table must be specified");

            _tableStack.Push(new TableSettings(columns,hasHeader,hasNoWrap,hasCenter,hasBorder));

            return hasBorder ? "<table class=\"bordered_table\">" : "<table>";
        }

        private TableSettings GetCurrentTable()
        {
            if( _tableStack.Count == 0 )
                throw new Exception("You cannot have a table cell without being in a table.");

            return _tableStack.Peek();
        }

        private string EndTableHandler(string endTagInnerText)
        {
            TableSettings tableSettings = GetCurrentTable();

            int cellIndex = tableSettings.CellCount % tableSettings.Columns;

            if( cellIndex != 0 )
                throw new Exception(String.Format("You cannot end the table without specifying an additional {0} cells",tableSettings.Columns - cellIndex));

            _tableStack.Pop();

            return endTagInnerText + "</table>";
        }

        private string StartTableCellHandler(string[] startTagComponents)
        {
            TableSettings tableSettings = GetCurrentTable();
            int cellIndex = tableSettings.CellCount % tableSettings.Columns;
            int columns = 1;

            if( startTagComponents.Length == 1 && ( !Int32.TryParse(startTagComponents[0],out columns) || columns < 1 ) )
                throw new Exception("The number of columns in a table must be a positive integer");

            if( ( cellIndex + columns ) > tableSettings.Columns )
                throw new Exception("The number of columns including a span cannot exceed the number of columns");

            bool isFirstCellInRow = ( cellIndex == 0 );
            bool isHeaderRow = tableSettings.HasHeader && ( tableSettings.CellCount < tableSettings.Columns );

            tableSettings.CellCount += columns;

            string rowPrefix = isFirstCellInRow ? "<tr>" : "";
            string cellType = "t" + ( isHeaderRow ? 'h' : 'd' );
            string span = ( columns > 1 ) ? String.Format(" colspan=\"{0}\"",columns) : "";

            string style = "";
            style += ( isFirstCellInRow && tableSettings.HasNoWrap ) ? "white-space: nowrap; " : "";
            style += ( tableSettings.HasCenter ) ? "text-align: center;" : "";
            
            string classStr = tableSettings.HasBorder ? " class=\"bordered_table_cell\"" : "";

            if( style.Length != 0 )
                style = " style=\"" + style.Trim() + "\"";

            return String.Format("{0}<{1}{2}{3}{4}>",rowPrefix,cellType,style,span,classStr);
        }

        private string StartSeeAlsoHandler(string[] startTagComponents)
        {
            bool orderedList = false;
            List<Preprocessor.TopicPreprocessor> topics = new List<Preprocessor.TopicPreprocessor>();

            foreach( string startTagComponent in startTagComponents )
            {
                if( startTagComponent.Equals(OrderedAttribute) )
                {
                    if( orderedList || topics.Count > 0 )
                        throw new Exception("The starttag's ordered attribute can only appear once and must appear first");

                    orderedList = true;
                }

                else
                    topics.Add(_helpComponents.preprocessor.GetTopic(startTagComponent));
            }

            if( topics.Count == 0 )
                throw new Exception("You must provide at least one topic");

            if( orderedList )
                topics.Sort((x,y) => x.Title.CompareTo(y.Title));

            StringBuilder sb = new StringBuilder();

            foreach( Preprocessor.TopicPreprocessor topic in topics )
            {
                sb.AppendFormat("{0} <a href=\"{1}\">{2}</a>",
                    sb.Length == 0 ? "<b>See also</b>:" : ",",
                    _topicCompilerSettings.GetHtmlFilename(topic),topic.Title);
            }

            return sb.ToString();
        }

        private string EndTableCellHandler(string endTagInnerText)
        {
            TableSettings tableSettings = GetCurrentTable();

            bool isHeaderRow = tableSettings.HasHeader && ( tableSettings.CellCount <= tableSettings.Columns );
            bool isLastCellInRow = ( ( tableSettings.CellCount % tableSettings.Columns ) == 0 );

            return endTagInnerText + ( isHeaderRow ? "</th>" : "</td>" ) + ( isLastCellInRow ? "</tr>\n" : "" );
        }

        private string TrimOnlyOneNewlineBothEnds(string text)
        {
            int startTrimChars = ( text.Length >= 2 && text[0] == '\r' && text[1] == '\n' ) ? 2 : 0;
            int endTrimChars = ( text.Length >= ( 2 + startTrimChars ) && text[text.Length - 2] == '\r' && text[text.Length - 1] == '\n' ) ? 2 : 0;
            return text.Substring(startTrimChars,text.Length - startTrimChars - endTrimChars);
        }

        private void CreateLogicPffColorizers()
        {
            if( _helpComponents._logicColorizer == null )
                _helpComponents._logicColorizer = new LogicColorizer(new LogicColorizerHtmlHelp(this));

            if( _helpComponents._inlineLogicColorizer == null )
                _helpComponents._inlineLogicColorizer = new LogicColorizer(new LogicColorizerHtmlHelpInline(this));

            if( _helpComponents._pffColorizer == null )
                _helpComponents._pffColorizer = new Colorizer.PffColorizer(new PffColorizerHtmlHelp());
        }

        public string GetHtmlFilenameForKeyword(string helpTopic)
        {
            try
            {
                Preprocessor.TopicPreprocessor topic = _helpComponents.preprocessor.GetTopic(helpTopic);
                return _topicCompilerSettings.GetHtmlFilename(topic);
            }

            catch( Exception )
            {
                return null;
            }
        }

        private string EndLogicHandler(string endTagInnerText)
        {
            CreateLogicPffColorizers();
            return _helpComponents._logicColorizer.Colorize(TrimOnlyOneNewlineBothEnds(endTagInnerText));
        }

        private string EndLogicSyntaxHandler(string endTagInnerText)
        {
            StringBuilder logic = new StringBuilder(EndLogicHandler(endTagInnerText));

            // this assumes that the syntax is set up with proper start/end brackets and tags

            // colorize the text between the optional arguments
            logic.Replace("『","<span class=\"code_colorization_optional_text\"><font class=\"code_colorization_bracket\">『</font>");
            logic.Replace("』","<font class=\"code_colorization_bracket\">』</font></span>");

            // colorize the optional arguments
            logic.Replace("&lt;arg&gt;","<span class=\"code_colorization_argument\">");
            logic.Replace("&lt;/arg&gt;","</span>");

            // colorize the multiple arguments separator
            logic.Replace("‖","<font class=\"code_colorization_bracket\">‖</font>");

            return logic.ToString();
        }

        private string EndLogicColorHandler(string endTagInnerText)
        {
            CreateLogicPffColorizers();
            return _helpComponents._inlineLogicColorizer.Colorize(endTagInnerText.Trim());
        }

        private string StartLogicTableHandler(string[] startTagComponents)
        {
            int columns = 0;

            if( !Int32.TryParse(startTagComponents[0],out columns) || columns < 1 )
                throw new Exception("The number of columns in a table must be a positive integer");

            CreateLogicPffColorizers();

            List<string> reservedWords = _helpComponents._inlineLogicColorizer.ReservedWordsList;

            int rows = (int)Math.Ceiling((double)reservedWords.Count / columns);

            StringBuilder sb = new StringBuilder();
            sb.Append("<div align=\"center\"><table class=\"bordered_table\">");

            for( int r = 0; r < rows; r++ )
            {
                sb.Append("<tr>");

                for( int c = 0; c < columns; c++ )
                {
                    sb.Append("<td class=\"bordered_table_cell\">");

                    int index = ( c * rows ) + r;

                    if( index < reservedWords.Count )
                        sb.Append(_helpComponents._inlineLogicColorizer.Colorize(reservedWords[index].ToLower()));

                    sb.Append("</td>");
                }

                sb.Append("</tr>");
            }

            sb.Append("</table></div>");

            return sb.ToString();
        }

        private string EndPffHandler(string endTagInnerText)
        {
            CreateLogicPffColorizers();
            return _helpComponents._pffColorizer.Colorize(TrimOnlyOneNewlineBothEnds(endTagInnerText));
        }

        private string EndPffColorHandler(string endTagInnerText)
        {
            CreateLogicPffColorizers();
            return _helpComponents._pffColorizer.ColorizeWord(endTagInnerText.Trim());
        }


        public const string TagTitle = "title";
        public const string ContextTag = "context";
        public const string IndentTag = "indent";
        public const string CenterTag = "center";
        public const string BoldTag = "b";
        public const string ItalicsTag = "i";
        public const string FontTag = "font";
        public const string ListTag = "list";
        public const string ListItemTag = "li";
        public const string HeaderTag = "header";
        public const string SubheaderTag = "subheader";
        public const string ImageTag = "image";
        public const string TopicTag = "topic";
        public const string LinkTag = "link";
        public const string TableTag = "table";
        public const string TableCellTag = "cell";
        public const string SeeAlsoTag = "seealso";
        public const string LogicTag = "logic";
        public const string LogicSyntaxTag = "logicsyntax";
        public const string LogicColorTag = "logiccolor";
        public const string LogicArgumentTag = "arg";
        public const string LogicTableTag = "logictable";
        public const string PffTag = "pff";
        public const string PffColorTag = "pffcolor";
        public const string HtmlTag = "html";
        public const string DefinitionTag = "definition";

        public const string NoHeaderAttribute = "noheader";
        public const string FontMonospaceAttribute = "monospace";
        public const string ImageNoChmAttribute = "nochm";
        public const string OrderedAttribute = "ordered";
        public const string NoWrapAttribute = "nowrap";
        public const string BorderAttribute = "border";

        public const string NewLineMarker = "~!~";
    }
}
