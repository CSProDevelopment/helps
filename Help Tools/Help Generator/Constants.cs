using System;

namespace Help_Generator
{
    class Constants
    {
        public static readonly string ChmButtonText1 = "CSPro";
        public static readonly string ChmButtonLink1 = "http://www.census.gov/population/international/software/cspro/";
        public static readonly string ChmButtonText2 = "Forum";
        public static readonly string ChmButtonLink2 = "http://www.csprousers.org/forum/";

        public static readonly string TopicsDirectoryName = "topics";
        public static readonly string ImagesDirectoryName = "images";
        public static readonly string TemporaryFileDirectoryName = "zhg_temp";
        public static readonly string ResourceFileRootDirectoryFilename = "ResourceFileRootDirectory.txt";

        public static readonly string SettingsFileExtension = ".hghelp";
        public static readonly string IndexFileExtension = ".hgindex";
        public static readonly string TableOfContentsFileExtension = ".hgtoc";
        public static readonly string TopicExtension = ".hgt";
        public static readonly string PreprocessorFilename = "zhg_preprocessor.bin";

        public static readonly string RegistrySettingsDocumentName = "HelpGenerator.Document";
        public static readonly string RegistryTopicDocumentName = "HelpGeneratorTopic.Document";

        public static string CompilationSuccessfulMessage { get { return "Compiled successfully at " + DateTime.Now.ToLongTimeString(); } }
        public static string ComplationErrorMessage { get { return String.Format("Compiler error at {0}:\r\n\r\n",DateTime.Now.ToLongTimeString()); } }
        public static string FormattedSuccessfulMessage { get { return "Formatted successfully at " + DateTime.Now.ToLongTimeString(); } }
    }
}
