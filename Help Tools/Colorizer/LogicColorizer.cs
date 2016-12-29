using System;
using System.Collections.Generic;
using System.Text;

namespace Colorizer
{
    class LogicColorizer
    {
        private SortedSet<string> _reservedWords;
        private LogicColorizerInterface _defaultLogicColorizer;

        public LogicColorizer()
        {
            HighlightWordReader hwr = new HighlightWordReader(HighlightWordReader.LogicFilename);
            _reservedWords = hwr.ReadWordBlock(false);
        }

        public LogicColorizer(LogicColorizerInterface defaultLogicColorizer) : this()
        {
            _defaultLogicColorizer = defaultLogicColorizer;
        }

        public string Colorize(string sourceText,bool inlineColorization = false)
        {
            return Colorize(_defaultLogicColorizer,sourceText,inlineColorization);
        }

        private enum ProcessType { LineComment, BracketComment, QuotedString, Number, Keyword, Newline, None };

        public string Colorize(LogicColorizerInterface logicColorizer,string sourceText,bool inlineColorization = false)
        {
            StringBuilder sb = new StringBuilder();

            logicColorizer.StartBlock(sb,inlineColorization);

            sourceText = HelperFunctions.TabsToSpaces(sourceText);

            int lastTextStartBlock = 0;
            int lastWordStartBlock = 0;
            int numBracketComments = 0;
            Char quoteChar = '\'';
            ProcessType processType = ProcessType.None;
            
            for( int i = 0; i < sourceText.Length; i++ )
            {
                bool additionalCharacterExists = ( ( i + 1 ) < sourceText.Length );
                bool processPreviousBlock = true;
                bool processPreviousBlockIncludeThisCharacter = false;
                bool reprocessCharacter = false;
                ProcessType savedProcessType = processType;
                int savedI = i;

                // newlines
                if( sourceText[i] == '\r' || sourceText[i] == '\n' )
                {
                    if( additionalCharacterExists && ( sourceText[i + 1] == '\r' || sourceText[i + 1] == '\n' ) )
                        i++;

                    processType = ProcessType.Newline;
                }


                // line comments
                else if( savedProcessType != ProcessType.QuotedString && additionalCharacterExists && sourceText[i] == '/' && sourceText[i + 1] == '/' )
                {
                    i++;
                    processType = ProcessType.LineComment;
                }

                else if( savedProcessType == ProcessType.LineComment )
                    processPreviousBlock = false;


                // bracket comments
                else if( savedProcessType != ProcessType.QuotedString && savedProcessType != ProcessType.LineComment && sourceText[i] == '{' )
                {
                    numBracketComments++;
                    processType = ProcessType.BracketComment;

                    if( savedProcessType == ProcessType.BracketComment ) // in a nested comment
                        processPreviousBlock = false;
                }

                else if( savedProcessType == ProcessType.BracketComment )
                {
                    if( sourceText[i] == '}' )
                    {
                        numBracketComments = Math.Max(0,numBracketComments - 1);

                        if( numBracketComments == 0 )
                        {
                            processPreviousBlockIncludeThisCharacter = true;
                            processType = ProcessType.None;
                        }
                    }

                    else
                        processPreviousBlock = false;
                }


                // numbers
                else if( savedProcessType == ProcessType.None && Char.IsDigit(sourceText[i]) )
                    processType = ProcessType.Number;

                else if( savedProcessType == ProcessType.Number )
                {
                    if( Char.IsDigit(sourceText[i]) || sourceText[i] == '.' )
                        processPreviousBlock = false;

                    else
                    {
                        processType = ProcessType.None;
                        reprocessCharacter = true;
                    }
                }


                // quoted strings
                else if( sourceText[i] == '\'' || sourceText[i] == '"' )
                {
                    if( processType == ProcessType.QuotedString )
                    {
                        // make sure that it's not an escaped quotation character
                        if( sourceText[i] == quoteChar && sourceText[i - 1] != '\\' )
                        {
                            processPreviousBlockIncludeThisCharacter = true;
                            processType = ProcessType.None;
                        }

                    }

                    else
                    {
                        processType = ProcessType.QuotedString;
                        quoteChar = sourceText[i];
                    }
                }

                else if( processType == ProcessType.QuotedString )
                    processPreviousBlock = false;


                // keywords
                else if( savedProcessType == ProcessType.None && Char.IsLetter(sourceText[i]) )
                {
                    lastWordStartBlock = i;
                    processType = ProcessType.Keyword;
                }

                else if( savedProcessType == ProcessType.Keyword )
                {
                    if( Char.IsLetterOrDigit(sourceText[i]) || sourceText[i] == '_' )
                        processPreviousBlock = false;

                    else
                        processType = ProcessType.None;
                }

                
                // anything that doesn't fit the above checks
                else
                    processPreviousBlock = false;


                // process the block if there is no more code to read
                bool atEndOfBuffer = ( !reprocessCharacter && ( ( i + 1 ) == sourceText.Length ) );

                if( atEndOfBuffer )
                {
                    processPreviousBlock = true;
                    processPreviousBlockIncludeThisCharacter = ( processType != ProcessType.Newline );
                }


                // process the previous block (if applicable)
                if( processPreviousBlock )
                {
                    string text = sourceText.Substring(lastTextStartBlock,savedI - lastTextStartBlock + ( processPreviousBlockIncludeThisCharacter ? 1 : 0 ));
                    bool resetLastTextStartBlock = true;

                    if( savedProcessType == ProcessType.LineComment || savedProcessType == ProcessType.BracketComment )
                        logicColorizer.AddComment(sb,text);

                    else if( savedProcessType == ProcessType.Number )
                        logicColorizer.AddNumber(sb,text);

                    else if( savedProcessType == ProcessType.QuotedString )
                        logicColorizer.AddQuotedString(sb,text);
                    
                    else if( savedProcessType == ProcessType.Keyword )
                    {
                        bool includeLastCharacter = ( processPreviousBlockIncludeThisCharacter && processType == ProcessType.Keyword );
                        bool outputLastCharacterAsText = ( atEndOfBuffer && !includeLastCharacter && processPreviousBlockIncludeThisCharacter && processType != ProcessType.Keyword );
                        string keyword = sourceText.Substring(lastWordStartBlock,savedI - lastWordStartBlock + ( includeLastCharacter ? 1 : 0 ));

                        if( _reservedWords.Contains(keyword.ToUpper()) )
                        {
                            logicColorizer.AddText(sb,text.Substring(0,text.Length - keyword.Length - ( outputLastCharacterAsText ? 1 : 0 )));
                            logicColorizer.AddKeyword(sb,keyword);

                            if( outputLastCharacterAsText )
                                logicColorizer.AddText(sb,text.Substring(keyword.Length));
                        }

                        else if( processType == ProcessType.Newline || atEndOfBuffer )
                            logicColorizer.AddText(sb,text);

                        else
                            resetLastTextStartBlock = false;
                    }

                    else if( savedProcessType == ProcessType.None )
                        logicColorizer.AddText(sb,text);

                    if( processType == ProcessType.Newline )
                    {
                        logicColorizer.AddNewLine(sb);
                        processType = ( numBracketComments > 0 ) ? ProcessType.BracketComment : ProcessType.None;
                        lastTextStartBlock = i + 1; // skip over the newline character
                    }

                    else if( resetLastTextStartBlock )
                        lastTextStartBlock = savedI + ( processPreviousBlockIncludeThisCharacter ? 1 : 0 );
                }

                if( reprocessCharacter )
                    i--;
            }

            logicColorizer.EndBlock(sb,inlineColorization);

            return sb.ToString();
        }
    }
}
