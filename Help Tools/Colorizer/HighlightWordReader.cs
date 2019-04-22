using System.Collections.Generic;
using System.IO;

namespace Colorizer
{
    class HighlightWordReader
    {
        public const string PffFilename = "PFF Words.txt";

        public const char CommentChar = '#';

        private string[] _fileLines;
        private int _lineItr;

        public HighlightWordReader(string wordFilename)
        {
            _fileLines = File.ReadAllLines(HelperFunctions.ReferenceFileLocate(wordFilename));
            _lineItr = 0;
        }

        public SortedSet<string> ReadWordBlock()
        {
            SortedSet<string> words = new SortedSet<string>();

            while( _lineItr < _fileLines.Length )
            {
                string trimmedWord = _fileLines[_lineItr++].Trim();

                if( trimmedWord.Length == 0 )
                    break;

                if( trimmedWord[0] == CommentChar )
                    continue;
                
                words.Add(trimmedWord.ToUpper());
            }

            return words;
        }
    }
}
