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
    [Solution("5777")]
    class Problem046Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=46
        ///
        /// It was proposed by Christian Goldbach that every odd composite
        /// number can be written as the sum of a prime and twice a square.
        ///
        /// 9 = 7 + 2×1²
        /// 15 = 7 + 2×2²
        /// 21 = 3 + 2×3²
        /// 25 = 7 + 2×3²
        /// 27 = 19 + 2×2²
        /// 33 = 31 + 2×1²
        ///
        /// It turns out that the conjecture was false.
        /// What is the smallest odd composite that cannot be written as
        /// the sum of a prime and twice a square?
        ///
        /// Answer:
        /// </summary>
        public Problem046Generator() { }

        protected override string InternalCalculateSolution()
        {
            var primesEnum = Prime.FromFile().Take(1000000000).GetEnumerator();
            var primeList = Prime.FromFile().Take(10000).ToList();

            foreach(var oddNo in OddNumbers.Sequence.Skip(1).Where(o => !primeList.Contains(o)))
            {
                var flag = false;
                foreach(var prime in primeList.Where(p => p < oddNo))
                {
                    if ((oddNo - prime) % 2 == 0)
                    {
                        var square = (oddNo - prime) / 2;
                        var sqrInt = (ulong)Math.Sqrt(square);
                        if (square == (sqrInt * sqrInt))
                        {
                            flag = true;
                            continue;
                        }
                    }
                }

                if (!flag)
                    return oddNo.ToString();
            }

            return "-1";

        }
    }
}
