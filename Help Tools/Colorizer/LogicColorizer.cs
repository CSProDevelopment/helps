using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Colorizer
{
    class LogicColorizer
    {
		public enum SourceType { Logic, Message };

        private SortedSet<string> _reservedWords;
        private SortedSet<string> _otherWordsWithHelps;
        private Dictionary<string,string> _helpTopics;
        private LogicColorizerInterface _defaultLogicColorizer;

        public List<string> ReservedWordsList { get { return _reservedWords.ToList(); } }
        public LogicColorizerInterface DefaultLogicColorizer { get { return _defaultLogicColorizer; } }

        public LogicColorizer(SourceType sourceType = SourceType.Logic)
        {
            HighlightWordReader hwr = new HighlightWordReader(HighlightWordReader.LogicFilename);

            _helpTopics = new Dictionary<string,string>();
            _otherWordsWithHelps = new SortedSet<string>();

			if( sourceType == SourceType.Logic )
				_reservedWords = hwr.ReadWordBlock(false,_otherWordsWithHelps,_helpTopics);

			else
				_reservedWords = new SortedSet<string>();
        }

        public LogicColorizer(LogicColorizerInterface defaultLogicColorizer,SourceType sourceType = SourceType.Logic)
			: this(sourceType)
        {
            _defaultLogicColorizer = defaultLogicColorizer;
        }

        public string Colorize(string sourceText)
        {
            return Colorize(_defaultLogicColorizer,sourceText);
        }

        private enum ProcessType { LineComment, BracketComment, QuotedString, Number, Keyword, Newline, None };

        public string Colorize(LogicColorizerInterface logicColorizer,string sourceText)
        {
            StringBuilder sb = new StringBuilder();

            logicColorizer.StartBlock(sb);

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
                int processPreviousBlockCharactersToInclude = 0;
                bool reprocessCharacter = false;
                bool justProcessedLineComment = false;
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
                    justProcessedLineComment = true;
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
                            processPreviousBlockCharactersToInclude = 1;
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
                            processPreviousBlockCharactersToInclude = 1;
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
                    processPreviousBlockCharactersToInclude = ( processType == ProcessType.Newline ) ? 0 : 1;

                    // this fixes a problem when the last character in the buffer is a number
                    if( processType == ProcessType.Number && savedProcessType != ProcessType.Number )
                    {
                        processPreviousBlockCharactersToInclude = 0;
                        reprocessCharacter = true;
                    }

                    // or if the last characters are a line comment
                    if( justProcessedLineComment )
                    {
                        if( savedProcessType == ProcessType.LineComment )
                            processPreviousBlockCharactersToInclude = 2;

                        else
                        {
                            processPreviousBlockCharactersToInclude = 0;
                            reprocessCharacter = true;
                            i--; // because it was incremented above
                        }
                    }
                }


                // process the previous block (if applicable)
                if( processPreviousBlock )
                {
                    string text = sourceText.Substring(lastTextStartBlock,savedI - lastTextStartBlock + processPreviousBlockCharactersToInclude);
                    bool resetLastTextStartBlock = true;

                    if( savedProcessType == ProcessType.LineComment || savedProcessType == ProcessType.BracketComment )
                        logicColorizer.AddComment(sb,text);

                    else if( savedProcessType == ProcessType.Number )
                        logicColorizer.AddNumber(sb,text);

                    else if( savedProcessType == ProcessType.QuotedString )
                        logicColorizer.AddQuotedString(sb,text);

                    else if( savedProcessType == ProcessType.Keyword )
                    {
                        bool includeLastCharacter = ( ( processPreviousBlockCharactersToInclude > 0 ) && processType == ProcessType.Keyword );
                        bool outputLastCharacterAsText = ( atEndOfBuffer && !includeLastCharacter && ( processPreviousBlockCharactersToInclude > 0 ) && processType != ProcessType.Keyword );
                        string keyword = sourceText.Substring(lastWordStartBlock,savedI - lastWordStartBlock + ( includeLastCharacter ? 1 : 0 ));
                        string upperCaseKeyword = keyword.ToUpper();

                        bool isReservedWord = _reservedWords.Contains(upperCaseKeyword);

                        if( isReservedWord || _otherWordsWithHelps.Contains(upperCaseKeyword) )
                        {
                            string helpTopic = _helpTopics.ContainsKey(upperCaseKeyword) ? _helpTopics[upperCaseKeyword] : null;

                            logicColorizer.AddText(sb,text.Substring(0,text.Length - keyword.Length - ( outputLastCharacterAsText ? 1 : 0 )));

                            if( isReservedWord )
                                logicColorizer.AddKeyword(sb,keyword,helpTopic);

                            else
                                logicColorizer.AddTextWithHelp(sb,keyword,helpTopic);

                            if( outputLastCharacterAsText )
                                logicColorizer.AddText(sb,text.Substring(keyword.Length));
                        }

                        else if( processType == ProcessType.Newline || processType == ProcessType.LineComment || atEndOfBuffer )
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
                        lastTextStartBlock = savedI + processPreviousBlockCharactersToInclude;
                }

                if( reprocessCharacter )
                    i--;
            }

            logicColorizer.EndBlock(sb);

            return sb.ToString();
        }
    }
}
