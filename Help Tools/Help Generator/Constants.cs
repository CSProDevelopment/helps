using System;

namespace Help_Generator
{
    class Constants
    {
        public static readonly string TopicsDirectoryName = "html";
        public static readonly string ImagesDirectoryName = "images";

        public static readonly string SettingsFileExtension = ".cshelp";
        public static readonly string PreprocessorFilename = "zhg_preprocessor.bin";

        public static string CompilationSuccessfulMessage { get { return "Compiled successfully at " + DateTime.Now.ToLongTimeString(); } }
        public static string ComplationErrorMessage { get { return String.Format("Compiler error at {0}:\r\n\r\n",DateTime.Now.ToLongTimeString()); } }
    }
}
