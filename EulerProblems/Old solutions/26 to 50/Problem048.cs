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
    [Solution("9110846700")]
    class Problem048Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=48
        /// 
        /// The series, 1¹ + 2² + 3³ + ... + 10¹⁰ = 10405071317.
        /// Find the last ten digits of the series,
        /// 1¹ + 2² + 3³ + ... + 1000^¹⁰⁰⁰.
        /// 
        /// Answer: 9110846700
        /// </summary>
        public Problem048Generator() { }

        protected override string InternalCalculateSolution()
        {
            Func<int, BigInteger> power = num =>
            {
                BigInteger temp = 1;
                for (int i = 0; i < num; i++)
                    temp *= num;
                return temp;
            };


            var answer = Enumerable.Range(1, 1000)
                .Select(s => power(s))
                .Sum()
                .ToString();

            return answer.Substring(answer.Length - 10);
        }
    }
}
