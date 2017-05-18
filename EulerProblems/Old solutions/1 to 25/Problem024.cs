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
    [Solution("2783915460")]
    class Problem024Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=21
        /// 
        /// A permutation is an ordered arrangement of objects. For 
        /// example, 3124 is one possible permutation of the digits 
        /// 1, 2, 3 and 4. If all of the permutations are listed 
        /// numerically or alphabetically, we call it lexicographic 
        /// order. The lexicographic permutations of 0, 1 and 2 are:
        /// 
        /// 012   021   102   120   201   210
        /// 
        /// What is the millionth lexicographic permutation of the 
        /// digits 0, 1, 2, 3, 4, 5, 6, 7, 8 and 9?
        /// 
        /// Answer: 2783915460
        /// </summary>
        public Problem024Generator() { }

        

        protected override string InternalCalculateSolution()
        {
            return GetPermutationAtPosition(10, 1000000-1);
        }

        /// <summary>
        /// Given 'numDigits' number of digits, calculate the 'position'th permutation.
        /// 'position' is zero-based
        /// </summary>
        string GetPermutationAtPosition(long numDigits, long position)
        {
            long fact = numDigits.Factorial();

            if (position > fact)
                throw new ArgumentOutOfRangeException("position");

            var list = Enumerable.Range(0, (int)numDigits).Select(d => (uint)d).ToList();

            var sb = new StringBuilder();
            long divSize;
            long remainder = 0;
            long toDiv = position + 1;
            long digit;

            for (long num = numDigits; num > 1; num--)
            {
                fact = num.Factorial();
                divSize = fact / num;

                if (toDiv == 0)
                {
                    digit = divSize;
                    remainder = 0;
                }
                else
                {
                    
                    digit = Math.DivRem((long)toDiv, (long)divSize, out remainder);
                    if (remainder == 0)
                        digit--;
                }

                sb.Append(list[(int)digit]);
                list.RemoveAt((int)digit);
                toDiv = remainder;
            }
            sb.Append(list.First());
            return sb.ToString();
        }
    }
}
