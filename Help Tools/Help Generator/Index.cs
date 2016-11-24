using System.IO;
using System.Text;

namespace Help_Generator
{
    class Index : TextEditableInterface
    {
        private string _indexFilename;

        private TopicListParser.TopicListNode _indexRootNode;

        private TopicListParser _indexParser;

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
    }
}
