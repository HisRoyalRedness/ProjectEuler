using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Numerics;
using System.Reflection;

namespace fletcher.org.Solutions
{
    [Export(typeof(IProblem))]
    [Solution("55")]
    class Problem035Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=35
        /// 
        /// The number, 197, is called a circular prime because all 
        /// rotations of the digits: 197, 971, and 719, are themselves prime.
        /// 
        /// There are thirteen such primes below 100: 
        /// 2, 3, 5, 7, 11, 13, 17, 31, 37, 71, 73, 79, and 97.
        /// 
        /// How many circular primes are there below one million?
        /// 
        /// Answer: 55
        /// </summary>
        public Problem035Generator() { }

        protected override string InternalCalculateSolution()
        {
            ulong limit = 1000000;
            var primes = new HashSet<ulong>(Prime.FromFile().TakeWhile(p => p < limit).Select(p => (ulong)p));

            return primes.Where(p => p.IsCircularPrime()).Count().ToString();
        }
    }
}
