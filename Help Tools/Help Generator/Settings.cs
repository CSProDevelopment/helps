using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Help_Generator
{
    class Settings : TextEditableInterface
    {
        private string _settingsFilename;

        private string _helpsTitle;
        private Preprocessor.TopicPreprocessor _defaultTopic;
        private Dictionary<string,string> _definitions;
        private List<string> _resourceFiles;

        private AttributesParser _settingAttributesParser;
        private AttributesParser _definitionAttributesParser;

        public Settings(string projectPath)
        {
            string projectName = new DirectoryInfo(projectPath).Name;
            _settingsFilename = Path.Combine(projectPath,projectName + Constants.SettingsFileExtension);

            if( !File.Exists(_settingsFilename) )
                CreateNewSettingsFile();

            _settingAttributesParser = new AttributesParser(new AttributesParser.AttributeType[]
                {
                    new AttributesParser.AttributeType(AttributesTitle,true,false),
                    new AttributesParser.AttributeType(AttributesDefaultTopic,true,false),
                    new AttributesParser.AttributeType(AttributesDefinitionsFile,false,false),
                    new AttributesParser.AttributeType(AttributesResourceFile,false,true)
                }
            );

            _definitionAttributesParser = new AttributesParser(null);
        }

        private void CreateNewSettingsFile()
        {
            using( TextWriter tw = new StreamWriter(_settingsFilename,false,Encoding.UTF8) )
            {
                string fillInText = "=<fill in>";
                string fillInOrDeleteText = "=<fill in or delete>";

                tw.WriteLine(AttributesTitle + fillInText);
                tw.WriteLine(AttributesDefaultTopic + fillInText);
                tw.WriteLine(AttributesDefinitionsFile + fillInOrDeleteText);
                tw.WriteLine(AttributesResourceFile + fillInOrDeleteText);
                tw.WriteLine(AttributesResourceFile + fillInOrDeleteText);
            }
        }

        // elements of the TextEditableInterface
        public string Filename { get { return _settingsFilename; } }

        public string Title { get { return "Project Settings File"; } }

        public string HelpText { get { return Properties.Resources.SettingsHelp; } }

        public void Compile(string[] lines,Preprocessor preprocessor)
        {
            _settingAttributesParser.Parse(lines);

            _helpsTitle = _settingAttributesParser.GetSingleValue(AttributesTitle);
            _defaultTopic = preprocessor.GetTopic(_settingAttributesParser.GetSingleValue(AttributesDefaultTopic));

            _definitions = new Dictionary<string,string>();
            string definitionFilename = _settingAttributesParser.GetSingleValue(AttributesDefinitionsFile);

            if( definitionFilename != null )
            {
                _definitionAttributesParser.Parse(File.ReadAllLines(definitionFilename));

                foreach( var kp in _definitionAttributesParser.GetPairs() )
                    _definitions.Add(kp.Key,kp.Value[0]);
            }

            _resourceFiles = new List<string>();

            foreach( string resourceFilename in _settingAttributesParser.GetValues(AttributesResourceFile) )
            {
                if( File.Exists(resourceFilename) )
                    _resourceFiles.Add(resourceFilename);

                else
                    throw new Exception("The resource file could not be located: " + resourceFilename);
            }
        }

        public void Compile(Preprocessor preprocessor)
        {
            Compile(File.ReadAllLines(_settingsFilename),preprocessor);
        }

        public string Format(string[] lines,Preprocessor preprocessor)
        {
            throw new Exception("The settings file does not support automatic formatting.");
        }

        private readonly string AttributesTitle = "Title";
        private readonly string AttributesDefaultTopic = "DefaultTopic";
        private readonly string AttributesDefinitionsFile = "DefinitionsFile";
        private readonly string AttributesResourceFile = "ResourceFile";
    }
}
