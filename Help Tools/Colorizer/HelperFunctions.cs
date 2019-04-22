using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Win32;

namespace Colorizer
{
    class HelperFunctions
    {
        public static string ReferenceFileLocate(string filename, string directory = null)
        {
            // search in the executable directory and keep moving up to a parent folder in case the file isn't found
            if( directory == null )
                directory = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

            string possible_filename = Path.Combine(directory, filename);

            if( File.Exists(possible_filename) )
                return possible_filename;

            else
            {
                string parent_directory = Path.GetFullPath(Path.Combine(directory, ".."));

                if( parent_directory.Equals(directory) )
                    throw new FileNotFoundException("Missing reference file: " + filename);

                return ReferenceFileLocate(filename, parent_directory);
            }
        }

        public static string CalculateResourceFileRoot()
        {
            try
            {
                return File.ReadAllLines(ReferenceFileLocate(Constants.ResourceFileRootDirectoryFilename))[0].Trim();
            }

            catch
            {
                return null;
            }
        }

        public enum ProgramFilesDirectory { x86, x64 };
        public static string GetProgramFilesDirectory(ProgramFilesDirectory directory_type)
        {
            string directory = null;

            if( directory_type == ProgramFilesDirectory.x64 )
                directory = (string)Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion")?.GetValue("ProgramW6432Dir");

            return ( directory != null ) ? directory : Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
        }

        public static Assembly AssemblyResolver(object sender, ResolveEventArgs args)
        {
            string assembly_name = new AssemblyName(args.Name).Name;

            if( assembly_name.Equals("zInterfaceCLR") )
            {
                var possible_directories = new List<string>();

                // first look for zInterfaceCLR using the resource file directory (debug\bin and then release\bin)
                try
                {
                    string resource_file_root = CalculateResourceFileRoot();
                    possible_directories.Add(Path.Combine(resource_file_root, @"debug\bin"));
                    possible_directories.Add(Path.Combine(resource_file_root, @"release\bin"));
                }
                catch { }

                // also look in Program Files (x86)
                var program_files_directory_info = new DirectoryInfo(GetProgramFilesDirectory(ProgramFilesDirectory.x86));
                possible_directories.AddRange(program_files_directory_info.GetDirectories("CSPro*").Select(x => x.FullName).Reverse());

                foreach( string possible_directory in possible_directories )
                {
                    string dll_filename = Path.Combine(possible_directory, assembly_name + ".dll");

                    if( File.Exists(dll_filename) )
                        return Assembly.LoadFrom(dll_filename);
                }
            }

            return null;
        }

        public static string Htmlize(string text)
        {
            return HtmlizeWhitespace(HtmlizeEscapes(text));
        }

        public static string TabsToSpaces(string text)
        {
            const int SpacesInTab = 4;

            StringBuilder sb = new StringBuilder();
            int column = 0;

            foreach( char ch in text )
            {
                if( ch == '\t' )
                {
                    int spacesToAdd = SpacesInTab - ( column % SpacesInTab );
                    sb.Append(' ',spacesToAdd);
                    column += spacesToAdd;
                }

                else
                {
                    sb.Append(ch);

                    if( ch == '\r' || ch == '\n' )
                        column = 0;

                    else
                        column++;
                }
            }

            return sb.ToString();
        }

        public static string HtmlizeEscapes(string text)
        {
            return text.Replace("&","&amp;").Replace("<","&lt;").Replace(">","&gt;");
        }

        public static string UnHtmlizeEscapes(string text)
        {
            return text.Replace("&lt;","<").Replace("&gt;",">").Replace("&amp;","&");
        }

        public static string HtmlizeWhitespace(string text)
        {
            return ReplaceWhitespace(text,"&nbsp;");
        }

        public static string ReplaceWhitespace(string text,string replacement)
        {
            StringBuilder sb = new StringBuilder();

            bool lastCharacterWasSpace = false;
            bool firstCharacter = true;

            foreach( char ch in text )
            {
                bool outputCharacter = true;

                if( ch == ' ' )
                {
                    if( lastCharacterWasSpace || firstCharacter )
                    {
                        sb.Append(replacement);
                        lastCharacterWasSpace = false;
                        outputCharacter = false;
                    }

                    else
                        lastCharacterWasSpace = true;
                }

                else
                    lastCharacterWasSpace = false;

                if( outputCharacter )
                    sb.Append(ch);

                firstCharacter = false;
            }

            return sb.ToString();
        }

        public static string TrimTrailingSpace(string text)
        {
            StringBuilder sb = new StringBuilder();

            bool keepProcessing = true;

            for( int i = 0; keepProcessing; i++ )
            {
                keepProcessing = ( i < text.Length );
                bool trimSpaces = ( !keepProcessing || text[i] == '\r' || text[i] == '\n' );

                if( trimSpaces )
                {
                    int lastNonSpace = sb.Length - 1;

                    while( lastNonSpace >= 0 && ( Char.IsWhiteSpace(sb[lastNonSpace]) && sb[lastNonSpace] != '\r' && sb[lastNonSpace] != '\n' ) )
                        lastNonSpace--;

                    sb.Remove(lastNonSpace + 1,sb.Length - lastNonSpace - 1);
                }

                if( keepProcessing )
                    sb.Append(text[i]);
            }

            return sb.ToString();
        }
    }
}
