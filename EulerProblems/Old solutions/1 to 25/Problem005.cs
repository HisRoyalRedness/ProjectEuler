using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.ComponentModel.Composition;

namespace fletcher.org
{
    [Export(typeof(IProblem))]
    [Solution("232792560")]
    class Problem005Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=5
        /// 
        /// 2520 is the smallest number that can be divided by each of 
        /// the numbers from 1 to 10 without any remainder.
        /// 
        /// What is the smallest positive number that is evenly divisible 
        /// by all of the numbers from 1 to 20?
        /// 
        /// Answer: 232792560
        /// </summary>
        public Problem005Generator() { }

        protected override string InternalCalculateSolution()
        {
            return Enumerable.Range(1, 20)
                .LowestCommonMultiple()
                .ToString();
        }
    }
}
