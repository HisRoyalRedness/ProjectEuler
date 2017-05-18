using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Numerics;
using System.Reflection;

namespace fletcher.org
{
    [Export(typeof(IProblem))]
    [Solution("872187")]
    class Problem036Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=36
        /// 
        /// The decimal number, 585 = 1001001001 (binary), 
        /// is palindromic in both bases.
        /// 
        /// Find the sum of all numbers, less than one million, which 
        /// are palindromic in base 10 and base 2.
        /// 
        /// (Please note that the palindromic number, in either base, 
        /// may not include leading zeros.)
        /// 
        /// Answer: 872187
        /// </summary>
        public Problem036Generator() { }

        protected override string InternalCalculateSolution()
        {   
            return Palindrome.Sequence.Where(n => n < 1000000)
                .Where(n => Convert.ToString((int)n, 2).IsPalindrome())
                .Sum().ToString();
        }
    }
}
