using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ArraysAndStrings
{
    public static class InterviewQuestions
    {
        /// <summary>
        /// Verify if in the given string it contains only unique characters, it skip special characters
        /// </summary>
        /// <param name="st">string to very</param>
        /// <returns></returns>
        public static bool IsUnique(string st)
        {
            // Edge 1: null or empty string => in theory "all" is unique
            if (string.IsNullOrEmpty(st)) return true;

            var charContainer = new System.Collections.BitArray(26);
            foreach (var c in st)
            {                
                var position = -1;

                if (c >= 'A' && c <= 'Z')
                    position = c - 'A';

                if (c >= 'a' && c <= 'z')
                    position = c - 'a';

                // Edge case 2: skip characters not contained in the US alphabet 
                if (position == -1) continue;                

                if (charContainer[position])
                    return false;

                charContainer[position] = true;
            }
            return true;
        }
        

        /// <summary>
        /// Given 2 strings determine if one is permutation of the other
        /// </summary>
        /// <param name="st1"></param>
        /// <param name="st2"></param>
        /// <returns></returns>
        public static bool CheckPermutation(string st1, string st2)
        {
            /* not optimal but clean:
             this option checks for the length of the string, if are equals then both
             strings are sorted to verify that the content of both are equals.
             OrderBy => quick sort O(n log n)
             */
            return st1.Length == st2.Length && st1.OrderBy(x => x).Equals(st2.OrderBy(x => x));

            /* 
             this option counts each character of the string, then remove each element 
             of the second string. The array should end with all 0's in count    
             */
            if (st1.Length != st2.Length) return false;

            var charCountContainer = new int[128];
            foreach (var c in st1)
            {
                charCountContainer[c]++;
            }

            foreach (var c in st2)
            {
                if (charCountContainer[c] == 0)
                    return false;

                charCountContainer[c]--;                
            }

            return true;
        }

        /// <summary>
        /// Replace every " " (space) of the string with %20, should be 'in-place'
        /// Complexity: O(2n)
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        public static string URLify(char[] st)
        {
            var endPos = st.Length -1;
            var startPos = -1;

            for (var i = st.Length-1; i >= 0; i--)
            {
                if (st[i] != ' ')
                {
                    startPos = i;
                    break;
                }
            }
            
            for (var i = startPos; i >= 0 && i < endPos; i--)
            {
                if (st[i] == ' ')
                {
                    st[endPos] = '0';
                    endPos--;
                    st[endPos] = '2';
                    endPos--;
                    st[endPos] = '%';
                }
                else
                {
                    st[endPos] = st[i];
                }               
                endPos--;
            }
            
            return new string(st);
        }

        /// <summary>
        /// Verify if the provided string is a permutation of a palindrome
        /// tip: with all characters can you create a palindrome? 
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        public static bool IsPalindromePermutation(string st)
        {
            // edge case: null or empty
            if (string.IsNullOrEmpty(st)) return true;

            var charCountContainer = new int[128];
            foreach (var c in st) charCountContainer[c]++;
            
            // at most only one character can be odd count
            var oddCount = charCountContainer.Count(x => x % 2 > 0);
            return oddCount > 1;
        }


        /// <summary>
        /// Verify if the st2 and st1 are one step away from each other
        /// possible steps: add character, remove character, replace character
        /// </summary>
        /// <param name="st1"></param>
        /// <param name="st2"></param>
        /// <returns></returns>
        public static bool OneAway(string st1, string st2)
        {
            // store the change implemented in the second string, if both are true it means that
            // the change was a character replacement
            var addedChar = false;
            var removedChar = false;

            // edge cases: no valid strings (null or empty), both strings are equal
            if (string.IsNullOrEmpty(st1) || string.IsNullOrEmpty(st2) || st1.Equals(st2)) return false;

            var charCountContainer = new int[128];
            foreach (var c in st1) charCountContainer[c]++;

            // remove characters from the second string to compare differences
            foreach (var c in st2) charCountContainer[c]--;

            for (var c = 0; c < charCountContainer.Length; c++)
            {
                if (c > 0)
                {
                    if (c > 1 || addedChar)
                        return false; // more than one step away (added 2 or more characters)

                    addedChar = true;
                }

                if (c < 0)
                {
                    if (c < -1 || removedChar)
                        return false; // more than one step away (removed 2 or more characters)

                    removedChar = true;
                }
            }

            return true;
        }

    }
}
