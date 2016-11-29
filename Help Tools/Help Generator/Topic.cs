using System;
using System.IO;
using System.Text;

namespace Help_Generator
{
    class Topic : TextEditableInterface
    {
        private Preprocessor.TopicPreprocessor _preprocessedTopic;

        public Topic(Preprocessor.TopicPreprocessor preprocessedTopic)
        {
            _preprocessedTopic = preprocessedTopic;
        }

        public Preprocessor.TopicPreprocessor PreprocessedTopic { get { return _preprocessedTopic; } }
        
        // elements of the TextEditableInterface
        public string Filename { get { return _preprocessedTopic.Filename; } }

        public string Title { get { return _preprocessedTopic.Title; } }

        public string HelpText { get { throw new NotImplementedException(); } }

        public void Compile(string[] lines,Preprocessor preprocessor)
        {
            throw new NotImplementedException();
        }

        public string Format(string[] lines,Preprocessor preprocessor)
        {
            throw new Exception("The topic file does not support automatic formatting.");
        }

        public string CompileForHtml(string[] lines,Preprocessor preprocessor,TopicCompilerSettingsInterface topicCompilerSettings)
        {
            StringBuilder sb = new StringBuilder();

            return sb.ToString();
        }

        public static string CompileForTitle(string filename)
        {
            try
            {
                string text = File.ReadAllText(filename);

                string openTag = "<" + TagTitle + ">";
                string closeTag = "</" + TagTitle + ">";

                int openTagPos = text.IndexOf(openTag);
                int closeTagPos = text.IndexOf(closeTag);

                if( openTagPos >= 0 && closeTagPos > openTagPos )
                {
                    int titlePos = openTagPos + openTag.Length;
                    return text.Substring(titlePos,closeTagPos - titlePos);
                }
            }

            catch( Exception ) // ignore errors
            {
            }

            return null;
        }


        private const string TagTitle = "title";
    }
}
