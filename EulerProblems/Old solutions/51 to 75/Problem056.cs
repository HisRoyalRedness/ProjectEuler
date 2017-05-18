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
    [Solution("972")]
    class Problem056Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=56
        /// 
        /// A googol (10¹⁰⁰) is a massive number: one followed by 
        /// one-hundred zeros; 100¹⁰⁰ is almost unimaginably large: 
        /// one followed by two-hundred zeros. Despite their size, 
        /// the sum of the digits in each number is only 1.
        /// 
        /// Considering natural numbers of the form, a^b, where 
        /// a, b < 100, what is the maximum digital sum?
        /// 
        /// Answer: 972
        /// </summary>
        public Problem056Generator() { }

        protected override string InternalCalculateSolution()
        {
            BigInteger maxI = 0;
            int maxJ = 0;
            int maxSum = 0;
            string maxAns = "";

            for (BigInteger i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {                    
                    var ans = BigInteger.Pow(i, j).ToString();
                    var sum = ans.Select(c => (int)c - 0x30).Sum();
                    if (sum > maxSum)
                    {
                        maxSum = sum;
                        maxI = i;
                        maxJ = j;
                        maxAns = ans;
                    }
                }
            }
            Console.WriteLine("{0}^{1} = {2}", maxI, maxJ, maxAns);
            return maxSum.ToString();
        }
    }
}
