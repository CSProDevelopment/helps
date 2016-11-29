using System;
using System.Collections.Generic;
using System.IO;

namespace Help_Generator
{
    interface TopicCompilerSettingsInterface
    {
        string GetHtmlFilename(object preprocessorObject);
        string Title { set; }
        void AddContextSensitiveHelp(Preprocessor.TopicPreprocessor preprocessedTopic,string defineId);
    }


    class TextEditFormTopicCompilerSettings : TopicCompilerSettingsInterface
    {
        public string GetHtmlFilename(object preprocessorObject)
        {
            string filename = ( preprocessorObject is Preprocessor.TopicPreprocessor ) ?
                ((Preprocessor.TopicPreprocessor)preprocessorObject).Filename :  ((Preprocessor.ImagePreprocessor)preprocessorObject).Filename;

            return new Uri(filename).AbsoluteUri;
        }

        public string Title { get; set; }

        public void AddContextSensitiveHelp(Preprocessor.TopicPreprocessor preprocessedTopic,string defineId)
        {
        }
    }


    class GenerateHelpsFormTopicCompilerSettings : TopicCompilerSettingsInterface
    {
        public HashSet<string> UsedImageFilenames { get; set; }
        public Dictionary<Preprocessor.TopicPreprocessor,List<string>> ContextSensitiveHelps;

        public GenerateHelpsFormTopicCompilerSettings()
        {
            UsedImageFilenames = new HashSet<string>();
            ContextSensitiveHelps = new Dictionary<Preprocessor.TopicPreprocessor,List<string>>();
        }

        public string GetHtmlFilename(object preprocessorObject)
        {
            if( preprocessorObject is Preprocessor.TopicPreprocessor )
            {
                string filename = ((Preprocessor.TopicPreprocessor)preprocessorObject).Filename;
                return Path.GetFileNameWithoutExtension(filename) + Constants.TopicOutputExtension;
            }

            else
            {
                string filename = ((Preprocessor.ImagePreprocessor)preprocessorObject).Filename;
                UsedImageFilenames.Add(filename);
                return Path.GetFileName(filename);
            }
        }

        public string Title { get; set; }

        public void AddContextSensitiveHelp(Preprocessor.TopicPreprocessor preprocessedTopic,string defineId)
        {
            if( !ContextSensitiveHelps.ContainsKey(preprocessedTopic) )
                ContextSensitiveHelps.Add(preprocessedTopic,new List<string>());

            ContextSensitiveHelps[preprocessedTopic].Add(defineId); 
        }
    }
}
