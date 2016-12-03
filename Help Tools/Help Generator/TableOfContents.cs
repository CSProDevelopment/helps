using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Help_Generator
{
    class TableOfContents : TextEditableInterface
    {
        private string _tableOfContentsFilename;
        private TopicListParser.TopicListNode _tableOfContentsRootNode;
        private TopicListParser _tableOfContentsParser;

        private bool _processedFirstTopicChapter;
        private bool _processedLastTopicChapter;


        public TableOfContents(string projectPath)
        {
            string projectName = new DirectoryInfo(projectPath).Name;
            _tableOfContentsFilename = Path.Combine(projectPath,projectName + Constants.TableOfContentsFileExtension);
            
            if( !File.Exists(_tableOfContentsFilename) )
                File.WriteAllText(_tableOfContentsFilename,"",Encoding.UTF8);

            _tableOfContentsParser = new TopicListParser(false);
        }

        // elements of the TextEditableInterface
        public string Filename { get { return _tableOfContentsFilename; } }

        public string Title { get { return "Table of Contents File"; } }

        public string HelpText { get { return Properties.Resources.TableOfContentsHelp; } }

        public void Compile(string[] lines,Preprocessor preprocessor)
        {
            _tableOfContentsRootNode = _tableOfContentsParser.Parse(lines,preprocessor);
        }

        public void Compile(Preprocessor preprocessor)
        {
            Compile(File.ReadAllLines(_tableOfContentsFilename),preprocessor);
        }

        public string Format(string[] lines,Preprocessor preprocessor)
        {
            Compile(lines,preprocessor);
            return TopicListParser.Format(_tableOfContentsRootNode,false);
        }

        public void SaveForChm(string filename,Dictionary<Preprocessor.TopicPreprocessor,string> outputTopicFilenames)
        {
            using( TextWriter tw = new StreamWriter(filename,false,Encoding.ASCII) )
            {
                tw.WriteLine("<html>");
                tw.WriteLine("<object type=\"text/site properties\">");
                tw.WriteLine("<param name=\"SiteType\" value=\"toc\"/>");
                tw.WriteLine("<param name=\"Image Width\" value=\"16\"/>");
                tw.WriteLine("</object>");

                _processedFirstTopicChapter = false;
                _processedLastTopicChapter = false;

                SaveForChmNode(tw,outputTopicFilenames,_tableOfContentsRootNode);

                tw.WriteLine("</html>");
            }
        }

        private void SaveForChmNode(TextWriter tw,Dictionary<Preprocessor.TopicPreprocessor,string> outputTopicFilenames,TopicListParser.TopicListNode node)
        {
            bool ulStartWritten = false;

            while( node != null )
            {
                string linkedChmFilename = TopicListParser.GetLinkToChm(node.Title);
                string textAfterRecursion = null;

                if( linkedChmFilename != null ) // a link to another help file
                {
                    if( _processedFirstTopicChapter ) 
                        _processedLastTopicChapter = true;

                    if( ulStartWritten )
                    {
                        tw.WriteLine("</ul>");
                        ulStartWritten = false;
                    }

                    linkedChmFilename = Path.GetFileNameWithoutExtension(linkedChmFilename);

                    tw.WriteLine("<object type=\"text/sitemap\">");
                    tw.WriteLine("<param name=\"Name\" value=\"{0}.chm::/{0}.hhc\"/>",linkedChmFilename);
                    tw.WriteLine("<param name=\"Merge\" value=\"{0}.chm::/{0}.hhc\"/>",linkedChmFilename);
                    tw.WriteLine("</object>");
                }

                else // a chapter or topic
                {
                    _processedFirstTopicChapter = true;

                    if( !ulStartWritten )
                    {
                        tw.WriteLine("<ul>");
                        ulStartWritten = true;
                    }

                    tw.WriteLine("<li><object type=\"text/sitemap\">");

                    if( _processedLastTopicChapter )
                        throw new Exception("Linked help files must come before or after all chapters and topics.");

                    tw.WriteLine("<param name=\"Name\" value=\"{0}\"/>",node.Title);

                    if( node.Topic != null )
                        tw.WriteLine("<param name=\"Local\" value=\"{0}\"/>",outputTopicFilenames[node.Topic]);

                    tw.WriteLine("</object>");

                    textAfterRecursion = "</li>";
                }


                if( node.ChildNode != null )
                    SaveForChmNode(tw,outputTopicFilenames,node.ChildNode);

                if( textAfterRecursion != null )
                    tw.WriteLine(textAfterRecursion);

                node = node.SiblingNode;
            }

            if( ulStartWritten )
                tw.WriteLine("</ul>");
        }
    }
}
