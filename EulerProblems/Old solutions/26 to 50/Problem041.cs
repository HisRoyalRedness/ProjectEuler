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
    [Solution("7652413")]
    class Problem041Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=41
        /// 
        /// We shall say that an n-digit number is pandigital if it 
        /// makes use of all the digits 1 to n exactly once. 
        /// For example, 2143 is a 4-digit pandigital and is also prime.
        /// 
        /// What is the largest n-digit pandigital prime that exists?
        /// 
        /// Answer: 7652413
        /// </summary>
        public Problem041Generator() { }

        protected override string InternalCalculateSolution()
        {
            for (int i = 9; i > 1; i--)
            {
                var prime = Pandigital.GetNumbers(Enumerable.Range(1, i), i)
                    .OrderByDescending(p => p)
                    .FirstOrDefault(p => ((ulong)p).IsPrime());
                if (prime > 0)
                    return prime.ToString();
            }
            return "";
        }
    }
}
