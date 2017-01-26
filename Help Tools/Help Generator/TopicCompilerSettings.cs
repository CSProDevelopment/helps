using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Help_Generator
{
    enum LinkType { Topic, ExternalTopic, Other };

    interface TopicCompilerSettingsInterface
    {
        string GetStartingHtml(Preprocessor.TopicPreprocessor preprocessedTopic);
        string EndingHtml { get; }
        string GetTitle(string title);
        string GetHtmlFilename(object preprocessorObject);
        string ConstructLink(LinkType linkType,object linkObject,ref string extras);
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

        public string GetStartingHtml(Preprocessor.TopicPreprocessor preprocessedTopic) { return ""; }

        public string EndingHtml { get { return ""; } }

        public string GetTitle(string title) { return title; }

        public string GetHtmlFilename(object preprocessorObject)
        {
            if( preprocessorObject is Preprocessor.TopicPreprocessor )
                return TopicViewerForm.TopicUrlPrefix + Path.GetFileName(((Preprocessor.TopicPreprocessor)preprocessorObject).Filename);

            else
                return new Uri(((Preprocessor.ImagePreprocessor)preprocessorObject).Filename).AbsoluteUri;
        }

        public string ConstructLink(LinkType linkType,object linkObject,ref string extras)
        {
            if( linkType == LinkType.Topic )
                return GetHtmlFilename(linkObject);

            else if( linkType == LinkType.ExternalTopic )
            {
                string[] parameters = (string[])linkObject;
                extras = String.Format("onclick=\"return alert('External link to {0} - {1}!')\"",parameters[0],parameters[1]);
                return "";                
            }
            
            else // LinkType.Other
            {
                string url = (string)linkObject;

                if( url.IndexOf("http") == 0 ) // open external links in a separate window
                    extras = "target=\"_blank\"";

                return url;
            }
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


    abstract class GenerateHelpsTopicCompilerSettings : TopicCompilerSettingsInterface
    {
        public HashSet<string> UsedImageFilenames { get; set; }

        public GenerateHelpsTopicCompilerSettings()
        {
            UsedImageFilenames = new HashSet<string>();
        }

        public abstract string GetStartingHtml(Preprocessor.TopicPreprocessor preprocessedTopic);
        public abstract string EndingHtml { get; }

        public abstract string GetTitle(string title);

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

        public abstract string ConstructLink(LinkType linkType,object linkObject,ref string extras);

        public virtual string GetTopicStylesheet()
        {
            return String.Format("<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" />",Constants.TopicStylesheetFilename);
        }

        public string Title { get; set; }

        public abstract void AddContextSensitiveHelp(Preprocessor.TopicPreprocessor preprocessedTopic,string defineId);
        public abstract bool ChmCreationMode { get; }
    }


    class GenerateChmTopicCompilerSettings : GenerateHelpsTopicCompilerSettings
    {
        public Dictionary<Preprocessor.TopicPreprocessor,List<string>> ContextSensitiveHelps;

        public GenerateChmTopicCompilerSettings()
        {
            ContextSensitiveHelps = new Dictionary<Preprocessor.TopicPreprocessor,List<string>>();
        }

        public override string GetStartingHtml(Preprocessor.TopicPreprocessor preprocessedTopic) { return ""; }

        public override string EndingHtml { get { return ""; } }

        public override string GetTitle(string title) { return title; }

        public override string ConstructLink(LinkType linkType,object linkObject,ref string extras)
        {
            if( linkType == LinkType.Topic )
                return GetHtmlFilename(linkObject);

            else if( linkType == LinkType.ExternalTopic )
            {
                string[] parameters = (string[])linkObject;
                string filename = Path.GetFileNameWithoutExtension(parameters[1]) + Constants.TopicOutputExtension;
                return String.Format("{0}{1}::{2}",parameters[0],Constants.ChmFileExtension,filename);
            }
            
            else // LinkType.Other
            {
                string url = (string)linkObject;

                if( url.IndexOf("http") == 0 ) // open external links in a separate window
                    extras = "target=\"_blank\"";

                return url;
            }
        }

        public override void AddContextSensitiveHelp(Preprocessor.TopicPreprocessor preprocessedTopic,string defineId)
        {
            if( !ContextSensitiveHelps.ContainsKey(preprocessedTopic) )
                ContextSensitiveHelps.Add(preprocessedTopic,new List<string>());

            ContextSensitiveHelps[preprocessedTopic].Add(defineId); 
        }

        public override bool ChmCreationMode { get { return true; } }
    }


    class GenerateWebsiteTopicCompilerSettings : GenerateHelpsTopicCompilerSettings
    {
        private HelpComponents _helpComponents;

        public GenerateWebsiteTopicCompilerSettings(HelpComponents helpComponents)
        {
            _helpComponents = helpComponents;
        }

        public override string GetStartingHtml(Preprocessor.TopicPreprocessor preprocessedTopic)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<div id=\"container\">");

            sb.AppendLine("<div id=\"left\">");
            _helpComponents.tableOfContents.GenerateForWebsite(sb,preprocessedTopic);
            sb.AppendLine("</div>");

            sb.AppendLine("<div id=\"middle_spacing1\"></div>");
            sb.AppendLine("<div id=\"middle\"></div>");
            sb.AppendLine("<div id=\"middle_spacing2\"></div>");

            sb.AppendLine("<div id=\"right\">");

            return sb.ToString();
        }

        public override string EndingHtml { get { return "</div></div>"; } }

        public override string GetTitle(string title)
        {
            return title + " - " + _helpComponents.settings.HelpsTitle;
        }

        public override string ConstructLink(LinkType linkType,object linkObject,ref string extras)
        {
            if( linkType == LinkType.Topic )
                return GetHtmlFilename(linkObject);

            else if( linkType == LinkType.ExternalTopic )
            {
                string[] parameters = (string[])linkObject;
                string filename = Path.GetFileNameWithoutExtension(parameters[1]) + Constants.TopicOutputExtension;
                return String.Format("../{0}/{1}",parameters[0],filename);
            }
            
            else // LinkType.Other
                return (string)linkObject;
        }

        public override string GetTopicStylesheet()
        {
            return base.GetTopicStylesheet() + String.Format("<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" />",Constants.WebsiteStylesheetFilename);
        }

        public override void AddContextSensitiveHelp(Preprocessor.TopicPreprocessor preprocessedTopic,string defineId)
        {
        }

        public override bool ChmCreationMode { get { return false; } }
    }
}
