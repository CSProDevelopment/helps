using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Colorizer
{
    class PffColorizer
    {
        private SortedSet<string> _typeWords;
        private SortedSet<string> _headingWords;
        private SortedSet<string> _attributeWords;
        private PffColorizerInterface _defaultPffColorizer;

        public PffColorizer()
        {
            _typeWords = new SortedSet<string>(CSPro.Util.PFF.GetAppTypeWords().Select(x => x.ToUpper()));
            _headingWords = new SortedSet<string>(CSPro.Util.PFF.GetHeadingWords().Select(x => x.ToUpper()));
            _attributeWords = new SortedSet<string>(CSPro.Util.PFF.GetAttributeWords().Select(x => x.ToUpper()));
       }

        public PffColorizer(PffColorizerInterface defaultPffColorizer) : this()
        {
            _defaultPffColorizer = defaultPffColorizer;
        }

        public string Colorize(string sourceText)
        {
            return Colorize(_defaultPffColorizer,sourceText);
        }

        public string Colorize(PffColorizerInterface pffColorizer,string sourceText)
        {
            sourceText = HelperFunctions.TabsToSpaces(sourceText);

            StringBuilder sb = new StringBuilder();

            pffColorizer.StartBlock(sb);

            int lastTextStartBlock = 0;
            int lastWordStartBlock = 0;
            bool inWordBlock = false;
            bool wordBlockEndsAtRightBracket = false;
            bool keepProcessing = true;

            for( int i = 0; keepProcessing; i++ )
            {
                keepProcessing = ( i < sourceText.Length );

                bool newLine = ( keepProcessing && ( sourceText[i] == '\r' || sourceText[i] == '\n' ) );

                if( keepProcessing && ( sourceText[i] != '=' ) && !newLine && ( !Char.IsWhiteSpace(sourceText[i]) || wordBlockEndsAtRightBracket ) )
                {
                    if( !inWordBlock )
                    {
                        inWordBlock = true;
                        lastWordStartBlock = i;
                        wordBlockEndsAtRightBracket = ( sourceText[i] == '[' );
                    }

                    else if( wordBlockEndsAtRightBracket && sourceText[i] == ']' )
                        wordBlockEndsAtRightBracket = false;

                    continue;
                }

                if( inWordBlock )
                {
                    string word = sourceText.Substring(lastWordStartBlock,i - lastWordStartBlock);
                    string upperCaseWord = word.ToUpper();

                    int iWordType = // 0 - None, 1 - Heading, 2 - Type, 3 - Attribute
                        _headingWords.Contains(upperCaseWord) ? 1 : _typeWords.Contains(upperCaseWord) ? 2 : 
                        _attributeWords.Contains(upperCaseWord) ? 3 : 0;

                    int lastTextBlockLength = i - lastTextStartBlock - ( ( iWordType == 0 ) ? 0 : word.Length );
                    string preWordText = sourceText.Substring(lastTextStartBlock,lastTextBlockLength);

                    if( ( preWordText.Length > 0 ) && ( !keepProcessing || newLine || iWordType > 0 ) )
                    {
                        pffColorizer.AddText(sb,preWordText);
                        lastTextStartBlock = i;
                    }

                    if( iWordType > 0 )
                    {
                        if( iWordType == 1 )
                            pffColorizer.AddHeading(sb,word);

                        else if( iWordType == 2 )
                            pffColorizer.AddType(sb,word);

                        else if( iWordType == 3 )
                            pffColorizer.AddAttribute(sb,word);

                        lastTextStartBlock = i;
                    }

                    inWordBlock = false;
                }

                if( newLine )
                {
                    pffColorizer.AddText(sb,sourceText.Substring(lastTextStartBlock,i - lastTextStartBlock));
                    pffColorizer.AddNewLine(sb);

                    if( ( sourceText[i] == '\r' ) && ( ( i + 1 ) < sourceText.Length ) && ( sourceText[i + 1] == '\n' ) )
                        i++;

                    lastTextStartBlock = i + 1;
                }
            }

            // add any final text at the end
            pffColorizer.AddText(sb,sourceText.Substring(lastTextStartBlock));

            pffColorizer.EndBlock(sb);

            return sb.ToString();
        }

        public string ColorizeWord(string word)
        {
            StringBuilder sb = new StringBuilder();
            string upperCaseWord = word.ToUpper();

            if( _headingWords.Contains(upperCaseWord) )
                _defaultPffColorizer.AddHeading(sb,word);

            else if( _typeWords.Contains(upperCaseWord) )
                _defaultPffColorizer.AddType(sb,word);

            else if( _attributeWords.Contains(upperCaseWord) )
                _defaultPffColorizer.AddAttribute(sb,word);

            else
                throw new Exception("PFF files do not have the word " + word);

            return sb.ToString();
        }
    }
}
