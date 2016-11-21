using System.Collections.Generic;

namespace Colorizer
{
    class LogicColorizer
    {
        SortedSet<string> _reservedWords;

        public LogicColorizer()
        {
            HighlightWordReader hwr = new HighlightWordReader(HighlightWordReader.LogicFilename);
            _reservedWords = hwr.ReadWordBlock(false);
        }
    }
}
