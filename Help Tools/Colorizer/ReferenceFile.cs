using System.Diagnostics;
using System.IO;

namespace Colorizer
{
    class ReferenceFile
    {
        public static string Locate(string filename)
        {
            string exeDirectory = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

            // first see if the file is located in the same directory as the executable
            string fullFilename = Path.Combine(exeDirectory,filename);

            // if not, see if it is at the root of the solution (if the executable is run from the Debug/Release directory)
            if( !File.Exists(fullFilename) )
                fullFilename = Path.GetFullPath(Path.Combine(exeDirectory,@"..\..\..\",filename));

            if( !File.Exists(fullFilename) )
                throw new FileNotFoundException("Missing reference file: " + fullFilename);

            return fullFilename;
        }
    }
}
