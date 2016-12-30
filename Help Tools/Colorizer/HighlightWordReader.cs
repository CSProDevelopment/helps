using System.Collections.Generic;
using System.IO;

namespace Colorizer
{
    class HighlightWordReader
    {
        public const string LogicFilename = "Logic Words.txt";
        public const string PffFilename = "PFF Words.txt";

        private string[] _fileLines;
        private int _lineItr;

        public HighlightWordReader(string wordFilename)
        {
            _fileLines = File.ReadAllLines(HelperFunctions.ReferenceFileLocate(wordFilename));
            _lineItr = 0;
        }

        public SortedSet<string> ReadWordBlock(bool breakOnNewline,Dictionary<string,string> helpTopics = null)
        {
            SortedSet<string> words = new SortedSet<string>();

            for( ; _lineItr < _fileLines.Length; _lineItr++ )
            {
                string trimmedWord = _fileLines[_lineItr].Trim();

                if( trimmedWord.Length == 0 )
                {
                    if( breakOnNewline )
                    {
                        _lineItr++;
                        break;
                    }

                    else
                        continue; // skip blank lines
                }

                if( trimmedWord[0] == '#' ) // a comment
                    continue;

                string word = trimmedWord.ToUpper();

                int tabPos = trimmedWord.IndexOf('\t');

                if( tabPos > 0 )
                {
                    word = trimmedWord.Substring(0,tabPos).Trim().ToUpper();
                    string helpTopic = trimmedWord.Substring(tabPos + 1).Trim();

                    if( helpTopics != null )
                        helpTopics.Add(word,helpTopic);
                }

                words.Add(word);
            }

            return words;
        }
    }
}
