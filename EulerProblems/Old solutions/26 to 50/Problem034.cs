using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Numerics;
using System.Reflection;
using System.Diagnostics;

namespace fletcher.org.Solutions
{
    [Export(typeof(IProblem))]
    [Solution("40730")]
    class Problem034Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=34
        ///
        /// 145 is a curious number, as 1! + 4! + 5! = 1 + 24 + 120 = 145.
        /// 
        /// Find the sum of all numbers which are equal to the sum of the 
        /// factorial of their digits.
        /// 
        /// Note: as 1! = 1 and 2! = 2 are not sums they are not included.
        ///
        /// Answer: 40730
        /// </summary>
        public Problem034Generator() { }

        protected override string InternalCalculateSolution()
        {

            var curiousNums = Enumerable.Range(3, 1000000)
                .AsParallel()
                .Select(i => (ulong)i)
                .Where(i => i == i.ToNumericSequence().Select(k => k.Factorial()).Sum())                
                .Sum();

            return curiousNums.ToString();
            
        }        
    }
}
