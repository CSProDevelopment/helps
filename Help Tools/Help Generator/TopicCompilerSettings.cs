using System;
using System.Collections.Generic;
using System.IO;

namespace Help_Generator
{
    interface TopicCompilerSettingsInterface
    {
        string GetHtmlFilename(object preprocessorObject);
        string GetTopicStylesheet();
        string Title { set; }
        void AddContextSensitiveHelp(Preprocessor.TopicPreprocessor preprocessedTopic,string defineId);
        bool ChmCreationMode { get; }
    }


    class TextEditFormTopicCompilerSettings : TopicCompilerSettingsInterface
    {
        public TextEditFormTopicCompilerSettings()
        {
            ChmCreationMode = true;
        }

        public string GetHtmlFilename(object preprocessorObject)
        {
            if( preprocessorObject is Preprocessor.TopicPreprocessor )
                return TopicViewerForm.TopicUrlPrefix + Path.GetFileName(((Preprocessor.TopicPreprocessor)preprocessorObject).Filename);

            else
                return new Uri(((Preprocessor.ImagePreprocessor)preprocessorObject).Filename).AbsoluteUri;
        }
        
        public string GetTopicStylesheet()
        {
            return "<style>" + Properties.Resources.TopicStylesheet + "</style>";
        }

        public string Title { get; set; }

        public void AddContextSensitiveHelp(Preprocessor.TopicPreprocessor preprocessedTopic,string defineId)
        {
        }

        public bool ChmCreationMode { get; set; }
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

        public string GetTopicStylesheet()
        {
            return String.Format("<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" media=\"screen\" />",Constants.TopicStylesheetFilename);
        }

        public string Title { get; set; }

        public void AddContextSensitiveHelp(Preprocessor.TopicPreprocessor preprocessedTopic,string defineId)
        {
            if( !ContextSensitiveHelps.ContainsKey(preprocessedTopic) )
                ContextSensitiveHelps.Add(preprocessedTopic,new List<string>());

            ContextSensitiveHelps[preprocessedTopic].Add(defineId); 
        }

        public bool ChmCreationMode { get { return true; } }
    }
}
