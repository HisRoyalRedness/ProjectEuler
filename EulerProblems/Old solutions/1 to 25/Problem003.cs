using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.ComponentModel.Composition;
using System.Diagnostics;

namespace fletcher.org
{
    [Export(typeof(IProblem))]
    [Solution("6857")]
    class Problem003Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=3
        /// 
        /// The prime factors of 13195 are 5, 7, 13 and 29.
        /// 
        /// What is the largest prime factor of the number 600851475143 ?   
        /// 
        /// Answer: 6857
        /// </summary>
        public Problem003Generator() { }

        protected override string InternalCalculateSolution()
        {
            ulong number = 600851475143;
            ulong limit = (ulong)Math.Sqrt(number);

            return 
                Prime.FromFile()
                .TakeWhile(p => p < limit)
                .Where(prime => number % prime == 0)
                .Last().ToString();                                    
        }
    }
}
