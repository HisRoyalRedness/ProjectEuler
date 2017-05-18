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
    [Solution("9183")]
    class Problem029Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=29
        /// 
        /// Consider all integer combinations of a^b for 
        /// 
        ///     2 ≤ a ≤ 5 and 2 ≤ b ≤ 5:
        ///     2^2=4, 2^3=8, 2^4=16, 2^5=32
        ///     3^2=9, 3^3=27, 3^4=81, 3^5=243
        ///     4^2=16, 4^3=64, 4^4=256, 4^5=1024
        ///     5^2=25, 5^3=125, 5^4=625, 5^5=3125
        /// 
        /// If they are then placed in numerical order, 
        /// with any repeats removed, we get the following 
        /// sequence of 15 distinct terms:
        /// 
        /// 4, 8, 9, 16, 25, 27, 32, 64, 81, 125, 243, 256, 625, 1024, 3125
        /// 
        /// How many distinct terms are in the sequence generated 
        /// by a^b for 2 ≤ a ≤ 100 and 2 ≤ b ≤ 100?
        /// 
        /// Answer: 9183
        /// </summary>
        public Problem029Generator() { }


        protected override string InternalCalculateSolution()
        {
            int low = 2;
            int high = 99;

            return Enumerable.Range(low, high)
                .SelectMany(j =>
                {
                    BigInteger x = new BigInteger(j);
                    return Enumerable.Range(low, high)
                        .Select(i => x *= j);
                }).Distinct().Count().ToString();
        }
    }
}
