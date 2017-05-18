using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Numerics;
using System.Reflection;

namespace fletcher.org.Solutions._51_to_75
{
    [Export(typeof(IProblem))]
    [Solution("4075")]
    class Problem053Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=?
        /// 
        /// ⁰¹²³⁴⁵⁶⁷⁸⁹₀₁₂₃₄₅₆₇₈₉
        /// 
        /// There are exactly ten ways of selecting three from five, 12345:
        /// 
        /// 123, 124, 125, 134, 135, 145, 234, 235, 245, and 345
        /// 
        /// In combinatorics, we use the notation, ⁵C₃ = 10.
        /// 
        /// In general, n_C_r = 	n! / r!(n−r)!
        /// 
        /// ,where r ≤ n, n! = n×(n−1)×...×3×2×1, and 0! = 1.
        /// 
        /// It is not until n = 23, that a value exceeds one-million: 
        /// ²³C₁₀ = 1144066.
        /// 
        /// How many, not necessarily distinct, values of  n_C_r, 
        /// for 1 ≤ n ≤ 100, are greater than one-million?
        /// 
        /// Answer: 4075
        /// </summary>
        public Problem053Generator() { }

        static List<BigInteger> _factorials = new List<BigInteger>();

        protected override string InternalCalculateSolution()
        {
            for (int i = 0; i <= 100; i++)
                _factorials.Add(((BigInteger)i).Factorial());

            int sum = 0;
            for (int n = 1; n <= 100; n++)
            {
                for (int r = 1; r <= n; r++)
                {
                    if (C(n, r) > 1000000)
                        ++sum;
                }
            }
            
            return sum.ToString();
        }

        static BigInteger C(int n, int r)
        { return _factorials[n] / _factorials[r] / _factorials[n - r]; }
    }
}
