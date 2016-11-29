using System;

namespace Help_Generator
{
    class Constants
    {
        public const string ChmButtonText1 = "CSPro";
        public const string ChmButtonLink1 = "http://www.census.gov/population/international/software/cspro/";
        public const string ChmButtonText2 = "Forum";
        public const string ChmButtonLink2 = "http://www.csprousers.org/forum/";

        public const string TopicsDirectoryName = "topics";
        public const string ImagesDirectoryName = "images";
        public const string TemporaryFileDirectoryName = "zhg_temp";
        public const string ResourceFileRootDirectoryFilename = "ResourceFileRootDirectory.txt";

        public const string SettingsFileExtension = ".hghelp";
        public const string IndexFileExtension = ".hgindex";
        public const string TableOfContentsFileExtension = ".hgtoc";
        public const string TopicExtension = ".hgt";
        public const string TopicOutputExtension = ".html";
        public const string PreprocessorFilename = "zhg_preprocessor.bin";

        public const string RegistrySettingsDocumentName = "HelpGenerator.Document";
        public const string RegistryTopicDocumentName = "HelpGeneratorTopic.Document";

        public static string CompilationSuccessfulMessage { get { return "Compiled successfully at " + DateTime.Now.ToLongTimeString(); } }
        public static string ComplationErrorMessage { get { return String.Format("Compiler error at {0}:\r\n\r\n",DateTime.Now.ToLongTimeString()); } }
        public static string FormattedSuccessfulMessage { get { return "Formatted successfully at " + DateTime.Now.ToLongTimeString(); } }
    }
}
