using System.IO;
using System.Text;

namespace Help_Generator
{
    class Settings
    {
        private string _projectPath;
        private string _projectName;
        private string _settingsFilename;

        public Settings(string projectPath)
        {
            _projectPath = projectPath;
            _projectName = new DirectoryInfo(_projectPath).Name;
            _settingsFilename = Path.Combine(projectPath,_projectName + Properties.Resources.SettingsFileExtension);

            if( !File.Exists(_settingsFilename) )
                CreateNewSettingsFile();
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

        private readonly string AttributesTitle = "Title";
        private readonly string AttributesDefaultTopic = "DefaultTopic";
        private readonly string AttributesDefinitionsFile = "DefinitionsFile";
        private readonly string AttributesResourceFile = "ResourceFile";
    }
}
