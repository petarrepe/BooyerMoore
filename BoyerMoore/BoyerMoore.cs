using System;
using System.Linq;

namespace BoyerMoore
{
    //case insensitive, no whitespaces or any other characters beside those inside alphabet[]
    public class BoyerMoore
    {
        private string _haystack;
        private string _needle;
        private int[ , ] badCharLookupTable;
        private int[] goodSuffixRuleTable;
        private char[] alphabet = new char[] {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z' };


        public int numberOfComparisons = 0;
        public int IndexOf = -1;

        public BoyerMoore(string pattern, string text)
        {
            _haystack = text.ToLower();
            _needle = pattern.ToLower();
            PreprocessNeedle(_needle);
            CalculateIndexOf();
        }

        /// <summary>
        /// Runs the algorithm and performs comparisons
        /// </summary>
        private void CalculateIndexOf()
        {
            int indexOfCurrentPosition = 0;

            while (indexOfCurrentPosition <= (_haystack.Length - _needle.Length))
            {
                int j = _needle.Length - 1;

                while (j >= 0 && _needle[j] == _haystack[indexOfCurrentPosition + j])
                    j--;


                if (j < 0)
                {
                    // Match found
                    IndexOf = indexOfCurrentPosition;
                    return;
                }
                else
                {
                    string matchingSuffix = _needle.Substring(j);
                    int goodSuffixRuleSkips = (isPrefix(j)) ?  1 : matchingSuffix.Length;

                    indexOfCurrentPosition += Math.Max(LookupBadCharTable(_haystack[indexOfCurrentPosition + _needle.Length - 1], j) + 1, goodSuffixRuleSkips);

                    //indexOfCurrentPosition += Math.Max(LookupBadCharTable(_haystack[indexOfCurrentPosition + _needle.Length - 1], j) + 1, goodSuffixRuleTable[_needle.Length - 1 - j]);

                    numberOfComparisons++;
                }
            }
        }

      
        private void PreprocessNeedle(string needle)
        {   
            badCharLookupTable = new int[alphabet.Count(), needle.Count()];

            for(int i = 0; i < alphabet.Count(); i++)
            {
                for (int j = 0; j < needle.Count(); j++)
                {
                    if (j == 0 || alphabet[i] == needle[j])
                    {
                        badCharLookupTable[i, j] = 0;
                        continue;
                    }

                    int tempPointer = j;

                    do
                    {
                        tempPointer--;
                    }
                    while (tempPointer > -1 && alphabet[i] != needle[tempPointer]);

                    badCharLookupTable[i, j] = (tempPointer == -1) ? j : j-tempPointer-1;
                }
            }
            goodSuffixRuleTable = makeOffsetTable();
        }

        /// <summary>
        /// Performs a lookup on mismatched character table
        /// </summary>
        /// <param name="alphabetChar"></param>
        /// <param name="needlePosition"></param>
        /// <returns></returns>
        private int LookupBadCharTable(char alphabetChar, int needlePosition)
        {
            int test = alphabetChar - 97;
            return badCharLookupTable[alphabetChar-97, needlePosition];
        }

        /// <summary>
        /// Makes lookup table for good suffix rule
        /// </summary>
        private int[] makeOffsetTable()
        {
            int[] table = new int[_needle.Length];
            int lastPrefixPosition = _needle.Length;

            for (int i = _needle.Length - 1; i >= 0; --i)
            {
                if (isPrefix(i + 1))
                {
                    lastPrefixPosition = i + 1;
                }
                table[_needle.Length - 1 - i] = lastPrefixPosition - i + _needle.Length - 1;
            }
            for (int i = 0; i < _needle.Length - 1; ++i)
            {
                int suffixLen = suffixLength(i);
                table[suffixLen] = _needle.Length - 1 - i + suffixLen;
            }
            return table;
        }


        /// <summary>
        /// Returns true if needle[p] is prefix of needle
        /// </summary>
        /// <param name="p">Index of first character in matching string</param>
        /// <returns></returns>
        private bool isPrefix(int p)
        {
            for (int i = p, j = 0; i < _needle.Length; ++i, ++j)
            {
                if (_needle[i] != _needle[j])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Returns the maximum length of the substring ends at p and is a suffix.
        /// </summary>
        /// <param name="p"> the maximum length of the substring ends at p and is a suffix.</param>
        /// <returns></returns>
        private int suffixLength(int p)
        {
            int lenght = 0;

            for (int i = p, j = _needle.Length - 1; i >= 0 && _needle[i] == _needle[j]; --i, --j)
            {
                lenght += 1;
            }
            return lenght;
        }


    }
}
