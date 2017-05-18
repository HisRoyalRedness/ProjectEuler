using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Numerics;

namespace fletcher.org
{
    [Export(typeof(IProblem))]
    [Solution("142913828922")]
    class Problem010Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=10
        /// 
        /// The sum of the primes below 10 is 2 + 3 + 5 + 7 = 17.
        /// 
        /// Find the sum of all the primes below two million.
        /// 
        /// Answer: 142913828922
        /// </summary>
        public Problem010Generator() { }


        protected override string InternalCalculateSolution()
        {
            return Prime.FromFile().TakeWhile(p => p < 2000000)                
                .Sum().ToString();
        }
    }
}
