using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Numerics;
using System.Reflection;
using System.Diagnostics;

namespace fletcher.org
{
    [Export(typeof(IProblem))]
    [Solution("16695334890")]
    class Problem043Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=43
        /// 
        /// The number, 1406357289, is a 0 to 9 pandigital number 
        /// because it is made up of each of the digits 0 to 9 in 
        /// some order, but it also has a rather interesting 
        /// sub-string divisibility property.
        /// 
        /// Let d_1 be the 1st digit, d_2 be the 2nd digit, and 
        /// so on. In this way, we note the following:
        /// 
        /// d_2d_3d_4=406 is divisible by 2
        /// d_3d_4d_5=063 is divisible by 3
        /// d_4d_5d_6=635 is divisible by 5
        /// d_5d_6d_7=357 is divisible by 7
        /// d_6d_7d_8=572 is divisible by 11
        /// d_7d_8d_9=728 is divisible by 13
        /// d_8d_9d_10=289 is divisible by 17
        /// 
        /// Find the sum of all 0 to 9 pandigital numbers with 
        /// this property.
        /// 
        /// Answer: 16695334890
        /// </summary>
        public Problem043Generator() { }

        static int[] _array = new[] { 2, 3, 5, 7, 11, 13, 17 };

        protected override string InternalCalculateSolution()
        {
            return Pandigital.GetNumbers(Enumerable.Range(0, 10), 10)
                .AsParallel()
                .Where(p => HasProperty(p))
                .Sum()
                .ToString();
        }

        bool HasProperty(ulong number)
        {
            var numS = number.ToString("0000000000");
            for (int i = 1; i < 8; i++)
            {
                if (int.Parse(numS.Substring(i, 3)) % _array[i - 1] != 0)
                    return false;
            }
            return true;
        }
    }
}
