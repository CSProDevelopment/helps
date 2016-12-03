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
        private string _resourceFileRootDirectory;

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
            _resourceFileRootDirectory = null;
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
                string evaluatedResourceFilename = EvaluateResourceFilename(resourceFilename);

                if( File.Exists(evaluatedResourceFilename) )
                    _resourceFiles.Add(evaluatedResourceFilename);

                else
                    throw new Exception(String.Format("The resource file could not be located: {0}{1}",evaluatedResourceFilename,
                        ( _resourceFileRootDirectory == null ) ? ( "\r\n\r\nPerhaps add a resource file root directory specification file with the name " + Constants.ResourceFileRootDirectoryFilename) : ""));
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

        private const string AttributesTitle = "Title";
        private const string AttributesDefaultTopic = "DefaultTopic";
        private const string AttributesDefinitionsFile = "DefinitionsFile";
        private const string AttributesResourceFile = "ResourceFile";

        public void SaveForChm(string filename,string outputChmFilename,string tableOfContentsFilename,string indexFilename,
            Dictionary<Preprocessor.TopicPreprocessor,string> outputTopicFilenames,HashSet<string> usedImageFilenames,
            Dictionary<Preprocessor.TopicPreprocessor,List<string>> contextSensitiveHelps,
            List<string> mergeFiles)
        {
            using( TextWriter tw = new StreamWriter(filename,false,Encoding.ASCII) )
            {
                tw.WriteLine("[OPTIONS]");

                tw.WriteLine("Compiled File={0}",outputChmFilename);
                tw.WriteLine("Title={0}",_helpsTitle);
                tw.WriteLine("Contents File={0}",tableOfContentsFilename);
                tw.WriteLine("Index File={0}",indexFilename);
                tw.WriteLine("Default topic={0}",outputTopicFilenames[_defaultTopic]);
                tw.WriteLine("Default Window=main");
                tw.WriteLine("Auto Index=No");
                tw.WriteLine("Binary Index=Yes");
                tw.WriteLine("Binary TOC=No");
                tw.WriteLine("Flat=No");
                tw.WriteLine("Full-text search=Yes");
                tw.WriteLine("Language=0x409 English (United States)");


                tw.WriteLine("[WINDOWS]");

                tw.WriteLine("main=\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",{9},{10},{11},{12}",
                    "", // window caption
                    tableOfContentsFilename,
                    indexFilename,
                    "", // default topic
                    outputTopicFilenames[_defaultTopic], // home topic
                    Constants.ChmButtonLink1,
                    Constants.ChmButtonText1,
                    Constants.ChmButtonLink2,
                    Constants.ChmButtonText2,
                    "0x60520", // HHWIN_PROP_ settings from htmlhelp.h
                    0, // navigation pane width
                    "0xc284e", // HHWIN_BUTTON_ settings
                    "[0,0,800,600]"); // default window position


                tw.WriteLine("[FILES]");

                foreach( var kp in outputTopicFilenames )
                    tw.WriteLine(kp.Value);

                foreach( string imageFilename in usedImageFilenames )
                    tw.WriteLine(imageFilename);

                tw.WriteLine(Constants.TopicStylesheetFilename);


                if( contextSensitiveHelps.Count > 0 )
                {
                    tw.WriteLine("[ALIAS]");

                    foreach( var kp in contextSensitiveHelps )
                    {
                        foreach( var alias in kp.Value )
                            tw.WriteLine("{0}={1}",alias,outputTopicFilenames[kp.Key]);
                    }
                }

                if( _resourceFiles.Count > 0 )
                {
                    tw.WriteLine("[MAP]");

                    foreach( string resourceFile in _resourceFiles )
                        tw.WriteLine("#include {0}",resourceFile);
                }


                if( mergeFiles.Count > 0 )
                {
                    tw.WriteLine("[MERGE FILES]");

                    foreach( string mergeFile in mergeFiles )
                        tw.WriteLine(mergeFile);
                }
            }
        }

        private string EvaluateResourceFilename(string filename)
        {
            if( !File.Exists(filename) )
            {
                if( _resourceFileRootDirectory == null )
                    _resourceFileRootDirectory = CalculateResourceFileRoot(Directory.GetCurrentDirectory());

                if( _resourceFileRootDirectory != null )
                    filename = Path.GetFullPath(Path.Combine(_resourceFileRootDirectory,filename));
            }

            return filename;
        }

        private string CalculateResourceFileRoot(string directory)
        {
            string possibleFilename = Path.Combine(directory,Constants.ResourceFileRootDirectoryFilename);

            if( File.Exists(possibleFilename) )
                return File.ReadAllLines(possibleFilename)[0].Trim();

            else
            {
                string parentDirectory = Path.GetFullPath(Path.Combine(directory,".."));
                return parentDirectory.Equals(directory) ? null : CalculateResourceFileRoot(parentDirectory);
            }
        }

        public string GetDefinition(string attribute,Preprocessor preprocessor)
        {
            attribute = attribute.ToUpper();

            // compile the file if it hasn't been compiled yet or if the definition is missing (because
            // the definitions file may have changed since the last compilation)
            if( _definitions == null || !_definitions.ContainsKey(attribute) )
                Compile(preprocessor);

            if( !_definitions.ContainsKey(attribute) )
                throw new Exception(String.Format("The definition with attribute {0} has not been defined.",attribute));

            return _definitions[attribute];
        }
    }
}
