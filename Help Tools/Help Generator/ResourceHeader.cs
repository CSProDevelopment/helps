using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Help_Generator
{
    class ResourceHeader
    {
        // only add menus/accelerators, dialogs, and commands
        public class ResourceType
        {
            public string Prefix;
            public int IdIndex;
        }

        public static ResourceType[] ResourceTypes = new ResourceType[]
        {
            new ResourceType { Prefix = "IDR_", IdIndex = 0x20000 },
            new ResourceType { Prefix = "IDD_", IdIndex = 0x20000 },
            new ResourceType { Prefix = "ID_",  IdIndex = 0x10000 }
        };
        

        private static Dictionary<string, Dictionary<string, int>> _readIds = new Dictionary<string, Dictionary<string, int>>();

        public static Dictionary<string, int> ReadIds(string project_name)
        {
            string code_directory;

            try
            {
                code_directory = Colorizer.CodeDirectory.GetCodeDirectory();
            }

            catch
            {
                throw new Exception($"The resource file root directory specification file with the name {Colorizer.CodeDirectory.CodeDirectoryFilename} could not be found");
            }

            string resource_filename = Path.Combine(code_directory, project_name, "resource.h");
            string shared_resource_filename = Path.Combine(code_directory, project_name, "resource_shared.h");

            Dictionary<string, int> ids;

            // read the IDs if they haven't been read or if they have changed
            if( Colorizer.ExternalTextFileReader.ContentsNeedLoading(resource_filename) ||
                ( File.Exists(shared_resource_filename) && Colorizer.ExternalTextFileReader.ContentsNeedLoading(shared_resource_filename) ) ||
                !_readIds.TryGetValue(project_name, out ids) )
            {
                ids = new Dictionary<string, int>();

                var regex = new Regex("#define\\s+([a-zA-Z]\\S+)\\s+(\\d+)");

                Action<string> read_ids = ( filename ) =>
                {
                    if( !File.Exists(filename) )
                        throw new Exception($"The resource header file could not be located: {filename}");

                    foreach( string line in Colorizer.ExternalTextFileReader.ReadAllLines(filename) )
                    {
                        var match = regex.Match(line);

                        if( match.Success )
                        {
                            string context = match.Groups[1].Value;

                            if( ResourceTypes.Any(x => ( context.IndexOf(x.Prefix) == 0 )) )
                                ids[context] = int.Parse(match.Groups[2].Value);
                        }
                    }
                };

                read_ids(resource_filename);

                if( File.Exists(shared_resource_filename) )
                    read_ids(shared_resource_filename);                

                _readIds[project_name] = ids;
            }
            
            return ids;
        }
    }
}
