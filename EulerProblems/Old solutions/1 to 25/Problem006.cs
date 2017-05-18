using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace fletcher.org
{
    [Export(typeof(IProblem))]
    [Solution("25164150")]
    class Problem006Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=6
        /// 
        /// The sum of the squares of the first ten natural numbers is,
        /// 
        /// 1^(2) + 2^(2) + ... + 10^(2) = 385
        /// 
        /// The square of the sum of the first ten natural numbers is,
        /// 
        /// (1 + 2 + ... + 10)^(2) = 55^(2) = 3025
        /// 
        /// Hence the difference between the sum of the squares of the 
        /// first ten natural numbers and the square of the sum is 
        /// 3025 − 385 = 2640.
        /// 
        /// Find the difference between the sum of the squares of the 
        /// first one hundred natural numbers and the square of the sum.
        /// 
        /// Answer: 25164150
        /// </summary>
        public Problem006Generator() { }

        protected override string InternalCalculateSolution()
        {
            // http://en.wikipedia.org/wiki/Square_pyramidal_number

            var start = 1;
            var end = 100;

            var range = Enumerable.Range(start, end);
            var sqOfSums = range.Sum() * range.Sum();
            var sumOfSqs = range.Aggregate((acc, num) => acc + (num * num));
            return (sqOfSums - sumOfSqs).ToString();
        }
    }
}
