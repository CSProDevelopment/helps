using System;
using System.IO;
using System.Windows.Forms;

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

                string startTag = TopicCompiler.MakeTag(TopicCompiler.TagTitle,true);
                string endTag = TopicCompiler.MakeTag(TopicCompiler.TagTitle,false);

                int startTagPos = text.IndexOf(startTag);
                int endTagPos = text.IndexOf(endTag);

                if( startTagPos >= 0 && endTagPos > startTagPos )
                {
                    int titlePos = startTagPos + startTag.Length;
                    return text.Substring(titlePos,endTagPos - titlePos);
                }
            }

            catch( Exception ) // ignore errors
            {
            }

            return null;
        }

        public string CompileForHtml(Form form, string text, HelpComponents helpComponents, TopicCompilerSettingsInterface topicCompilerSettings)
        {
            TopicCompiler topicCompiler = new TopicCompiler(form, helpComponents,_preprocessedTopic,topicCompilerSettings);
            return topicCompiler.CompileForHtml(text);
        }
    }
}
