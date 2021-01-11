using System.Collections.Generic;
using System.IO;

namespace Colorizer
{
    class ExternalTextFileReader
    {
        private static Dictionary<string, FileInfo> lastWriteTimeMap = new Dictionary<string, FileInfo>();
        private static Dictionary<string, string[]> readLinesMap = new Dictionary<string, string[]>();

        public static bool ContentsNeedLoading(string filename)
        {
            FileInfo previous_fi;
            FileInfo current_fi = new FileInfo(filename);

            if( !lastWriteTimeMap.TryGetValue(filename, out previous_fi) || previous_fi.LastWriteTimeUtc != current_fi.LastWriteTimeUtc )
            {
                lastWriteTimeMap[filename] = current_fi;
                return true;
            }

            return false;
        }

        public static string[] ReadAllLines(string filename)
        {
            if( ContentsNeedLoading(filename) || !readLinesMap.ContainsKey(filename) )
                readLinesMap[filename] = File.ReadAllLines(filename);

            return readLinesMap[filename];
        }
    }
}
