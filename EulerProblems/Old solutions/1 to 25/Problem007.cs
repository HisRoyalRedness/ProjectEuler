using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace fletcher.org
{
    [Export(typeof(IProblem))]
    [Solution("104743")]
    class Problem007Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=7
        /// 
        /// By listing the first six prime numbers: 2, 3, 5, 7, 11, 
        /// and 13, we can see that the 6^(th) prime is 13.
        ///
        /// What is the 10001^(st) prime number?
        /// 
        /// Answer: 104743
        /// </summary>
        public Problem007Generator() { }

        protected override string InternalCalculateSolution()
        {
            return Prime.FromFile()
                .Take(10001)
                .Last().ToString();
        }
    }
}
