using System;
using System.IO;

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


        public static string CompileForTitle(string filename)
        {
            try
            {
                string text = File.ReadAllText(filename);

                string openTag = "<" + TopicCompiler.TagTitle + ">";
                string closeTag = "</" + TopicCompiler.TagTitle + ">";

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

        public string CompileForHtml(string[] lines,HelpComponents helpComponents,TopicCompilerSettingsInterface topicCompilerSettings)
        {
            TopicCompiler topicCompiler = new TopicCompiler(helpComponents,_preprocessedTopic,topicCompilerSettings);
            return topicCompiler.CompileForHtml(lines);
        }
    }
}
