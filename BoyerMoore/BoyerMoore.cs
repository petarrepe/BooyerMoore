using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoyerMoore
{
    //case insensitive
    public class BoyerMoore
    {
        private string _haystack;
        private string _needle;
        private int[ , ] lookupTable;
        private char[] alphabet = new char[] {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','x','y','z' };

        public int IndexOf = -1;

        public BoyerMoore(string pattern, string text)
        {
            _haystack = text;
            _needle = pattern.ToLower();
            PreprocessNeedle(_needle);
            CalculateIndexOf();
        }

        private void CalculateIndexOf()
        {
            int indexOfCurrentPosition = 0;

            while (indexOfCurrentPosition <= (_haystack.Length - _needle.Length))
            {
                //// Look if we have a match at this position
                int j = _needle.Length - 1;

                while (j >= 0 && _needle[j] == _haystack[indexOfCurrentPosition + j])
                    j--;


                if (j < 0)
                {
                    // Match found
                    IndexOf = indexOfCurrentPosition;
                    return;
                }

                // Advance to next comparision
                indexOfCurrentPosition += Math.Max(Lookup(_haystack[indexOfCurrentPosition], j), GoodSuffixRuleValue());
            }
        }


        private void PreprocessNeedle(string needle)
        {   
            lookupTable = new int[alphabet.Count(), needle.Count()];

            for(int i = 0; i < alphabet.Count(); i++)
            {
                for (int j = 0; j < needle.Count(); j++)
                {
                    if (j == 0 || alphabet[i] == needle[j])
                    {
                        lookupTable[i, j] = 0;
                        continue;
                    }

                    int tempPointer = j;

                    do
                    {
                        tempPointer--;
                    }
                    while (tempPointer > -1 && alphabet[i] != needle[tempPointer]);

                    lookupTable[i, j] = (tempPointer == -1) ? j: j-tempPointer-1;
                }
            }
        }

        private int Lookup(char alphabetChar, int needlePosition)
        {

            return lookupTable[alphabetChar-97, needlePosition];
        }

        private int GoodSuffixRuleValue()
        {
            return 0;
        }

    }
}
