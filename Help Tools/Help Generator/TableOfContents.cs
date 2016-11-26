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
                tw.WriteLine("<param name=\"SiteType\" value=\"toc\" />");
                tw.WriteLine("<param name=\"Image Width\" value=\"16\" />");
                tw.WriteLine("</object>");

                SaveForChmNode(tw,outputTopicFilenames,_tableOfContentsRootNode);

                tw.WriteLine("</html>");
            }
        }

        private void SaveForChmNode(TextWriter tw,Dictionary<Preprocessor.TopicPreprocessor,string> outputTopicFilenames,TopicListParser.TopicListNode node)
        {
            tw.WriteLine("<ul>");
            
            while( node != null )
            {
                tw.WriteLine("<li><object type=\"text/sitemap\">");
                tw.WriteLine("<param name=\"Name\" value=\"{0}\" />",node.Title);

                if( node.Topic != null )
                    tw.WriteLine("<param name=\"Local\" value=\"{0}\" />",outputTopicFilenames[node.Topic]);

                tw.WriteLine("</object>");

                if( node.ChildNode != null )
                    SaveForChmNode(tw,outputTopicFilenames,node.ChildNode);

                tw.WriteLine("</li>");

                node = node.SiblingNode;
            }

            tw.WriteLine("</ul>");
        }
    }
}
