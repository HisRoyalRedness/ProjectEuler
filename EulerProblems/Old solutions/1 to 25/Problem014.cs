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
    [Solution("837799")]
    class Problem014Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=14
        ///
        /// The following iterative sequence is defined for the set of positive integers:
        ///
        /// n → n/2 (n is even)
        /// n → 3n + 1 (n is odd)
        ///
        /// Using the rule above and starting with 13, we generate the following sequence:
        ///
        /// 13 → 40 → 20 → 10 → 5 → 16 → 8 → 4 → 2 → 1
        ///
        /// It can be seen that this sequence (starting at 13 and finishing at 1) contains
        /// 10 terms. Although it has not been proved yet (Collatz Problem), it is thought
        /// that all starting numbers finish at 1.
        ///
        /// Which starting number, under one million, produces the longest chain?
        /// NOTE: Once the chain starts the terms are allowed to go above one million.
        ///
        /// Answer: 837799
        /// </summary>
        public Problem014Generator() { }

        const int top = 100000000;

        protected override string InternalCalculateSolution()
        {
            return Enumerable.Range(1, 1000000)
                .Select(num => (ulong)num)
                .AsParallel()
                .Select(num => new { Num = num, Count = Algorithm(num).Count() })
                .MaxBy(num => num.Count)
                .Select(num => num.Num)
                .First()
                .ToString();
        }

        static IEnumerable<ulong> Algorithm(ulong start)
        {
            var number = start;
            while (number != 1)
            {
                if (number % 2 == 0)
                {
                    number /= 2;
                    yield return number;
                }
                else
                {
                    number = (number * 3) + 1;
                    yield return number;
                }
            }
        }
    }
}
