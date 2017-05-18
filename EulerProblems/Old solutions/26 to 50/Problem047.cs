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
    [Solution("134043")]
    class Problem047Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=47
        /// 
        /// The first two consecutive numbers to have two distinct
        /// prime factors are:
        /// 
        /// 14 = 2 × 7
        /// 15 = 3 × 5
        /// 
        /// The first three consecutive numbers to have three
        /// distinct prime factors are:
        /// 
        /// 644 = 2² × 7 × 23
        /// 645 = 3 × 5 × 43
        /// 646 = 2 × 17 × 19.
        /// 
        /// Find the first four consecutive integers to have
        /// four distinct primes factors. What is the first of
        /// these numbers?
        /// 
        /// Answer: 134043
        /// </summary>
        public Problem047Generator() { }

        protected override string InternalCalculateSolution()
        {
            var limit = 200000;
            var primes = new HashSet<ulong>(Prime.FromFile().Take(limit));

            var primeFactorCounts = Enumerable.Range(1, limit)
                .Select(n => new KeyValuePair<int, int>(n, ((ulong)n).ProperFactors().Where(f => primes.Contains(f)).Count()))
                .Where(n => n.Value == 4);


            var count = 0;
            var lastNum = 0;
            foreach (var factor in primeFactorCounts)
            {
                count = (factor.Key == lastNum + 1) ? count + 1 : 0;
                lastNum = factor.Key;

                if (count == 3)
                {
                    Console.WriteLine("{0}, {1}, {2}, {3}", lastNum - 3, lastNum - 2, lastNum - 1, lastNum);
                    return (lastNum - 3).ToString();
                }
            }

            return "";
        }
    }
}
