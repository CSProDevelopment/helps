using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Help_Generator
{
    class Index : TextEditableInterface
    {
        private string _indexFilename;
        private TopicListParser.TopicListNode _indexRootNode;
        private TopicListParser _indexParser;

        private List<string> _mergeFiles;

        public Index(string projectPath)
        {
            string projectName = new DirectoryInfo(projectPath).Name;
            _indexFilename = Path.Combine(projectPath,projectName + Constants.IndexFileExtension);

            if( !File.Exists(_indexFilename) )
                File.WriteAllText(_indexFilename,"",Encoding.UTF8);

            _indexParser = new TopicListParser(true);
        }

        // elements of the TextEditableInterface
        public string Filename { get { return _indexFilename; } }

        public string Title { get { return "Index File"; } }

        public string HelpText { get { return Properties.Resources.IndexHelp; } }

        public void Compile(string[] lines,Preprocessor preprocessor)
        {
            _indexRootNode = _indexParser.Parse(lines,preprocessor);
        }

        public void Compile(Preprocessor preprocessor)
        {
            Compile(File.ReadAllLines(_indexFilename),preprocessor);
        }

        public string Format(string[] lines,Preprocessor preprocessor)
        {
            Compile(lines,preprocessor);
            return TopicListParser.Format(_indexRootNode,true);
        }

        public List<string> MergeFiles { get { return _mergeFiles; } }

        public void SaveForChm(string filename,Dictionary<Preprocessor.TopicPreprocessor,string> outputTopicFilenames)
        {
            _mergeFiles = new List<string>();

            using( TextWriter tw = new StreamWriter(filename,false,Encoding.ASCII) )
            {
                tw.WriteLine("<html>");
                tw.WriteLine("<object type=\"text/site properties\">");
                tw.WriteLine("<param name=\"SiteType\" value=\"index\"/>");
                tw.WriteLine("</object>");

                SaveForChmNode(tw,outputTopicFilenames,_indexRootNode);

                tw.WriteLine("</html>");
            }
        }

        private void SaveForChmNode(TextWriter tw,Dictionary<Preprocessor.TopicPreprocessor,string> outputTopicFilenames,TopicListParser.TopicListNode node)
        {
            tw.WriteLine("<ul>");
            
            while( node != null )
            {
                if( node.Topic == null )
                    _mergeFiles.Add(TopicListParser.GetLinkToChm(node.Title));

                else
                {
                    tw.WriteLine("<li><object type=\"text/sitemap\">");
                    tw.WriteLine("<param name=\"Name\" value=\"{0}\"/>",node.Title);
                    tw.WriteLine("<param name=\"Name\" value=\"{0}\"/>",node.Topic.Title);
                    tw.WriteLine("<param name=\"Local\" value=\"{0}\"/>",outputTopicFilenames[node.Topic]);
                    tw.WriteLine("</object></li>");
                }

                if( node.ChildNode != null )
                    SaveForChmNode(tw,outputTopicFilenames,node.ChildNode);

                node = node.SiblingNode;
            }

            tw.WriteLine("</ul>");
        }
    }
}
