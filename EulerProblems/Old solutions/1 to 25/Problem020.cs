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
    [Solution("648")]
    class Problem020Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=20
        /// 
        /// n! means n × (n  − 1) × ... × 3 × 2 × 1
        /// 
        /// Find the sum of the digits in the number 100!
        /// 
        /// Answer: 648
        /// </summary>
        public Problem020Generator() { }

        protected override string InternalCalculateSolution()
        {
            return new BigInteger(100)
                .Factorial().ToString()
                .ToNumericSequence()
                .Sum().ToString();
        }
    }
}
