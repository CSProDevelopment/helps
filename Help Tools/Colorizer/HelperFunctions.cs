using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Colorizer
{
    class HelperFunctions
    {
        public static string ReferenceFileLocate(string filename)
        {
            string exeDirectory = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

            // first see if the file is located in the same directory as the executable
            string fullFilename = Path.Combine(exeDirectory,filename);

            // if not, see if it is exists one folder up from the root of the solution (if the executable is run from the Debug/Release directory)
            if( !File.Exists(fullFilename) )
                fullFilename = Path.GetFullPath(Path.Combine(exeDirectory,@"..\..\..\..\",filename));

            if( !File.Exists(fullFilename) )
                throw new FileNotFoundException("Missing reference file: " + fullFilename);

            return fullFilename;
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
