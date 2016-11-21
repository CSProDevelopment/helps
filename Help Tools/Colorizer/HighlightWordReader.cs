using System.Collections.Generic;
using System.IO;

namespace Colorizer
{
    class HighlightWordReader
    {
        public static string LogicFilename = "Logic Words.txt";
        public static string PffFilename = "PFF Words.txt";

        string[] _fileLines;
        int _lineItr;

        public HighlightWordReader(string wordFilename)
        {
            _fileLines = File.ReadAllLines(HelperFunctions.ReferenceFileLocate(wordFilename));
            _lineItr = 0;
        }

        public SortedSet<string> ReadWordBlock(bool breakOnNewline)
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

                words.Add(trimmedWord.ToUpper());
            }

            return words;
        }
    }
}
