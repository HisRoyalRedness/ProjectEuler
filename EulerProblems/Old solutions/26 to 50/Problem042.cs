using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Numerics;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;

namespace fletcher.org
{
    [Export(typeof(IProblem))]
    [Solution("162")]
    class Problem042Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=42
        /// 
        /// The nth term of the sequence of triangle numbers is given by, 
        /// t_n = n(n+1)/2; so the first ten triangle numbers are:
        /// 
        /// 1, 3, 6, 10, 15, 21, 28, 36, 45, 55, ...
        /// 
        /// By converting each letter in a word to a number corresponding 
        /// to its alphabetical position and adding these values we form 
        /// a word value. For example, the word value for SKY is 
        /// 19 + 11 + 25 = 55 = t10. If the word value is a triangle number 
        /// then we shall call the word a triangle word.
        /// 
        /// Using words.txt, a 16K text file containing nearly two-thousand 
        /// common English words, how many are triangle words?
        /// 
        /// Answer: 162
        /// </summary>
        public Problem042Generator() { }

        protected override string InternalCalculateSolution()
        {
            var tri = new HashSet<int>(TriangleNumbers.Sequence.TakeWhile(t => t < 200).Select(t => (int)t));
            return FileUtils.ReadCSV(@"..\Resources\words.txt")
                .Select(word => WordSum(word))
                .Where(sum => tri.Contains(sum))
                .Count().ToString();
        }

        int WordSum(string word)
        { return word.ToUpper().Select(c => (int)c - 0x40).Sum(); }
    }
}
