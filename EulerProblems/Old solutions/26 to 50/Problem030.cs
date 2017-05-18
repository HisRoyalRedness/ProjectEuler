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
    [Solution("443839")]
    class Problem030Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=30
        /// 
        /// Surprisingly there are only three numbers that can 
        /// be written as the sum of fourth powers of their digits:
        /// 
        ///     1634 = 1^4 + 6^4 + 3^4 + 4^4
        ///     8208 = 8^4 + 2^4 + 0^4 + 8^4
        ///     9474 = 9^4 + 4^4 + 7^4 + 4^4
        /// 
        /// As 1 = 14 is not a sum it is not included.
        /// The sum of these numbers is 1634 + 8208 + 9474 = 19316.
        /// Find the sum of all the numbers that can be written as 
        /// the sum of fifth powers of their digits.
        /// 
        /// Answer: 443839
        /// </summary>
        public Problem030Generator() { }


        protected override string InternalCalculateSolution()
        {
            return Enumerable.Range(2, 1000000)
                .AsParallel()
                .Select(i => new
                            {
                                Num = (ulong)i,
                                Sum = i.ToString()
                                    .Select(ii => (int)ii - 48)
                                    .Select(ii => Math.Pow(ii, 5))
                                    .Sum()
                            })
                .Where(i => i.Num == i.Sum)
                .Select(i => i.Num)
                .Sum().ToString();
        }
    }
}
