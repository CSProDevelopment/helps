using System.Collections.Generic;

namespace Colorizer
{
    class PffColorizer
    {
        SortedSet<string> _typeWords;
        SortedSet<string> _headingWords;
        SortedSet<string> _attributeWords;

        public PffColorizer()
        {
            HighlightWordReader hwr = new HighlightWordReader(HighlightWordReader.PffFilename);
            _typeWords = hwr.ReadWordBlock(true);
            _headingWords = hwr.ReadWordBlock(true);
            _attributeWords = hwr.ReadWordBlock(true);
        }
    }
}
