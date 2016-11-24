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
    }
}
