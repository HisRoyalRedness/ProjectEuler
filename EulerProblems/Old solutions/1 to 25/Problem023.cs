using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Numerics;
using System.Reflection;
using System.IO;

namespace fletcher.org
{    
    [Export(typeof(IProblem))]
    [Solution("4179871")]
    class Problem023Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=23
        ///
        /// A perfect number is a number for which the sum of its proper
        /// divisors is exactly equal to the number. For example, the sum
        /// of the proper divisors of 28 would be 1 + 2 + 4 + 7 + 14 = 28,
        /// which means that 28 is a perfect number.
        ///
        /// A number n is called deficient if the sum of its proper
        /// divisors is less than n and it is called abundant if this
        /// sum exceeds n.
        ///
        /// As 12 is the smallest abundant number,
        /// 1 + 2 + 3 + 4 + 6 = 16, the smallest number that can be
        /// written as the sum of two abundant numbers is 24. By
        /// mathematical analysis, it can be shown that all integers
        /// greater than 28123 can be written as the sum of two abundant
        /// numbers. However, this upper limit cannot be reduced any
        /// further by analysis even though it is known that the greatest
        /// number that cannot be expressed as the sum of two abundant
        /// numbers is less than this limit.
        ///
        /// Find the sum of all the positive integers which cannot be
        /// written as the sum of two abundant numbers.
        ///
        /// Answer: 4179871
        /// </summary>
        public Problem023Generator() { }

        protected override string InternalCalculateSolution()
        {

            Logging.WriteLine("Getting abundant numbers ...");

            ulong limit = 30000;

            var abundantNumbersHash = AbundantNumbers.Sequence
                .TakeWhile(i => i < limit)
                .ToDictionary(i => i);

            var abundantNumbersList = abundantNumbersHash.Keys.OrderBy(i => i).ToList();

            Logging.WriteLine("Checking sums...");

            return Enumerable.Range(1, (int)limit)
                .Select(i => (ulong)i)
                .AsParallel()
                .Where(i =>
                    !abundantNumbersList
                        .TakeWhile(a => a <= i / 2)
                        .Any(a => abundantNumbersHash.ContainsKey(i - a)))
                .Sum().ToString();

        }
    }
}
