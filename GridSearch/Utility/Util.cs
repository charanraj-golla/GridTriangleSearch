using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridSearch.Utility
{
    public static class Util
    {
        public static List<int> AlphabetRange(char startCharacter = 'A', char endCharacter = 'Z', bool reverseList = false) {
            var range = Enumerable.Range(startCharacter, endCharacter);
            range = reverseList ? range.Reverse() : range;
            return range.ToList();
        }
    }
}
