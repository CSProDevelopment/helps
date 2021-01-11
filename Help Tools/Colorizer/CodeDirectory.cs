namespace Colorizer
{
    class CodeDirectory
    {
        public const string CodeDirectoryFilename = "ResourceFileRootDirectory.txt";

        private static string _codeDirectoryFilename;

        public static string GetCodeDirectory()
        {
            if( _codeDirectoryFilename == null )
                _codeDirectoryFilename = HelperFunctions.ReferenceFileLocate(CodeDirectoryFilename);

            return ExternalTextFileReader.ReadAllLines(_codeDirectoryFilename)[0].Trim();
        }
    }
}
