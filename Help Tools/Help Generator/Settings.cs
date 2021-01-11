using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Help_Generator
{
    class Settings : TextEditableInterface
    {
        private string _settingsFilename;

        public string HelpsTitle { get; private set; }

        public bool ChmOutput { get; private set; }
        public bool HtmlOutput { get; private set; }
        public bool PdfOutput { get; private set; }

        public Preprocessor.TopicPreprocessor DefaultTopic { get; private set; }
        public Preprocessor.TopicPreprocessor PdfCoverTopic { get; private set; }

        private Dictionary<string,string> _definitions;
        private ResourceIdSet _resourceIdSet;

        static private AttributesParser.AttributeType[] _settingsAttributes = new AttributesParser.AttributeType[]
        {
            new AttributesParser.AttributeType(AttributesTitle,true,false,null),
            new AttributesParser.AttributeType(AttributesOutput,true,true,new string[] { OutputValueChm, OutputValueHtml, OutputValuePdf }),
            new AttributesParser.AttributeType(AttributesDefaultTopic,false,false,null),
            new AttributesParser.AttributeType(AttributesPdfCoverTopic,false,false,null),
            new AttributesParser.AttributeType(AttributesDefinitionsFile,false,true,null),
            new AttributesParser.AttributeType(AttributesResourceFile,false,true,null)
        };

        private AttributesParser _settingAttributesParser;
        private AttributesParser _definitionAttributesParser;

        public Settings(string projectPath)
        {
            string projectName = new DirectoryInfo(projectPath).Name;
            _settingsFilename = Path.Combine(projectPath,projectName + Constants.SettingsFileExtension);

            if( !File.Exists(_settingsFilename) )
                CreateNewSettingsFile();

            _settingAttributesParser = new AttributesParser(_settingsAttributes);

            _definitionAttributesParser = new AttributesParser(null);
        }

        private void CreateNewSettingsFile()
        {
            using( TextWriter tw = new StreamWriter(_settingsFilename,false,Encoding.UTF8) )
            {
                string fillInText = "=<fill in>";
                string fillInOrDeleteText = "=<fill in or delete>";

                foreach( var attribute_type in _settingsAttributes )
                {
                    if( attribute_type.PossibleValues == null )
                    {
                        for( int i = 0; i < ( attribute_type.MultipleAllowed ? 2 : 1 ); i++ )
                            tw.WriteLine(attribute_type.OriginalCaseName + ( attribute_type.Required ? fillInText : fillInOrDeleteText ));
                    }

                    else
                    {
                        foreach( string value in attribute_type.PossibleValues )
                            tw.WriteLine($"{attribute_type.OriginalCaseName}={value}");
                    }
                }
            }
        }

        // elements of the TextEditableInterface
        public string Filename { get { return _settingsFilename; } }

        public string Title { get { return "Project Settings File"; } }

        public string HelpText { get { return Properties.Resources.SettingsHelp; } }

        private string GetRequiredTopic(string attribute_name)
        {
            string value = _settingAttributesParser.GetSingleValue(attribute_name);

            if( value == null )
                throw new Exception($"The required attribute {attribute_name} (for the current outputs) was not defined");

            return value;
        }

        public void Compile(string[] lines,Preprocessor preprocessor)
        {
            _settingAttributesParser.Parse(lines);

            HelpsTitle = _settingAttributesParser.GetSingleValue(AttributesTitle);

            var output_values = _settingAttributesParser.GetValues(AttributesOutput);
            ChmOutput = output_values.Contains(OutputValueChm);
            HtmlOutput = output_values.Contains(OutputValueHtml);
            PdfOutput = output_values.Contains(OutputValuePdf);
            
            DefaultTopic = ( ChmOutput || HtmlOutput ) ? preprocessor.GetTopic(GetRequiredTopic(AttributesDefaultTopic)) : null;
            PdfCoverTopic = PdfOutput ? preprocessor.GetTopic(GetRequiredTopic(AttributesPdfCoverTopic)) : null;

            _definitions = new Dictionary<string,string>();

            // add the definitions in the settings file
            foreach( var kp in _settingAttributesParser.GetPairs() )
            {
                if( _settingsAttributes.FirstOrDefault(x => ( kp.Key == x.Name )) == null )
                    _definitions.Add(kp.Key, kp.Value[0]);
            }

            foreach( var definitionFilename in _settingAttributesParser.GetValues(AttributesDefinitionsFile) )
            {
                _definitionAttributesParser.Parse(File.ReadAllLines(definitionFilename));

                foreach( var kp in _definitionAttributesParser.GetPairs() )
                {
                    if( _definitions.ContainsKey(kp.Key) )
                        throw new Exception($"The definition {kp.Key} cannot be specified in multiple files");

                    _definitions.Add(kp.Key, kp.Value[0]);
                }
            }

            // add the resource header IDs
            _resourceIdSet = new ResourceIdSet();

            foreach( var resourceFileProjectName in _settingAttributesParser.GetValues(AttributesResourceFile) )
                _resourceIdSet.AddProject(resourceFileProjectName);
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
        private const string AttributesOutput = "Output";
        private const string OutputValueChm = "CHM";
        private const string OutputValueHtml = "HTML";
        private const string OutputValuePdf = "PDF";
        private const string AttributesDefaultTopic = "DefaultTopic";
        private const string AttributesPdfCoverTopic = "PdfCoverTopic";
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
                tw.WriteLine("Title={0}",HelpsTitle);
                tw.WriteLine("Contents File={0}",tableOfContentsFilename);
                tw.WriteLine("Index File={0}",indexFilename);
                tw.WriteLine("Default topic={0}",outputTopicFilenames[DefaultTopic]);
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
                    outputTopicFilenames[DefaultTopic], // home topic
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


                // handle the resource file IDs
                HashSet<string> _resourceFileIdsCopy = _resourceIdSet.GetAllContext();

                if( _resourceFileIdsCopy.Count > 0 )
                {
                    tw.WriteLine("[ALIAS]");

                    foreach( var kp in contextSensitiveHelps )
                    {
                        foreach( var alias in kp.Value )
                        {
                            tw.WriteLine($"{alias}={outputTopicFilenames[kp.Key]}");
                            _resourceFileIdsCopy.Remove(alias);
                        }
                    }

                    foreach( string alias in _resourceFileIdsCopy )
                        tw.WriteLine($"{alias}={outputTopicFilenames[DefaultTopic]}");

                    
                    tw.WriteLine("[MAP]");

                    foreach( var context in _resourceIdSet.GetAllContext() )
                        tw.WriteLine($"#define {context} {_resourceIdSet.GetId(context)}");
                }


                if( mergeFiles.Count > 0 )
                {
                    tw.WriteLine("[MERGE FILES]");

                    foreach( string mergeFile in mergeFiles )
                        tw.WriteLine(mergeFile);
                }
            }
        }

        public string GetDefinition(string attribute,Preprocessor preprocessor)
        {
            attribute = attribute.ToUpper();

            // compile the file if it hasn't been compiled yet or if the definition is missing
            // (because the definitions file may have changed since the last compilation)
            if( _definitions == null || !_definitions.ContainsKey(attribute) )
                Compile(preprocessor);

            if( !_definitions.ContainsKey(attribute) )
                throw new Exception(String.Format("The definition with attribute {0} has not been defined.",attribute));

            return _definitions[attribute];
        }

        public void CheckContextExists(string context, Preprocessor preprocessor)
        {
            if( _resourceIdSet != null )
            {
                try
                {
                    _resourceIdSet.CheckContextExists(context);
                    return;
                }
                catch { }
            }

            Compile(preprocessor);
            _resourceIdSet.CheckContextExists(context);
        }
    }
}
