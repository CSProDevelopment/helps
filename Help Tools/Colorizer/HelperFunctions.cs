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

            // if not, see if it is at the root of the solution (if the executable is run from the Debug/Release directory)
            if( !File.Exists(fullFilename) )
                fullFilename = Path.GetFullPath(Path.Combine(exeDirectory,@"..\..\..\",filename));

            if( !File.Exists(fullFilename) )
                throw new FileNotFoundException("Missing reference file: " + fullFilename);

            return fullFilename;
        }

        public static string Htmlize(string text)
        {
            return HtmlizeWhitespace(TabsToSpaces(HtmlizeEscapes(text)));
        }

        public static string TabsToSpaces(string text)
        {
            return text.Replace("\t","    ");
        }

        public static string HtmlizeEscapes(string text)
        {
            return text.Replace("&","&amp;").Replace("<","&lt;").Replace(">","&gt;");
        }

        public static string HtmlizeWhitespace(string text)
        {
            StringBuilder sb = new StringBuilder();

            bool lastCharacterWasSpace = false;

            foreach( char ch in text )
            {
                if( Char.IsWhiteSpace(ch) )
                {
                    if( lastCharacterWasSpace )
                    {
                        sb.Append("&nbsp;");
                        lastCharacterWasSpace = false;
                    }

                    else
                    {
                        sb.Append(' ');
                        lastCharacterWasSpace = true;
                    }
                } 

                else
                {
                    sb.Append(ch);
                    lastCharacterWasSpace = false;
                }
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
