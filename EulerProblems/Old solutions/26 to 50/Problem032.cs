using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Numerics;
using System.Reflection;
using System.Diagnostics;

namespace fletcher.org.Solutions
{
    [Export(typeof(IProblem))]
    [Solution("45228")]
    class Problem032Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=32
        ///
        /// We shall say that an n-digit number is pandigital if it makes 
        /// use of all the digits 1 to n exactly once; for example, the 
        /// 5-digit number, 15234, is 1 through 5 pandigital.
        /// 
        /// The product 7254 is unusual, as the identity, 
        ///     39 × 186 = 7254, 
        ///     
        /// containing multiplicand, multiplier, and product is 1 
        /// through 9 pandigital.
        /// 
        /// Find the sum of all products whose multiplicand/multiplier/product 
        /// identity can be written as a 1 through 9 pandigital.
        /// HINT: Some products can be obtained in more than one way so be 
        /// sure to only include it once in your sum.
        ///
        /// Answer: 45228
        /// </summary>
        public Problem032Generator() { }

        static int totalLen = 9;
        static char[] carr = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        bool[] _marker = Enumerable.Repeat(false, totalLen).ToArray();

        protected override string InternalCalculateSolution()
        {
            return GetPandigitals().Select(t => t.Item3).Distinct().Sum().ToString();
        }

        IEnumerable<Tuple<int, int, int>> GetPandigitals()
        {
            /*
             * A one digit number times a four digit number should give
             * an answer of 9-10 digits. We're only interested in nine
             * digit answers. A two digit number times a three digit 
             * gives answers of 9-10 digits.
             * 
             * Therefore, we only need to test one and two digit numbers
             * against four and three digit numbers respectively, in order 
             * to avoid counting duplicates
             */


            var range = Enumerable.Range(1, 9).ToList();

            var oneAndTwoDigit = Pandigital.GetNumbers(range, 9, false).Where(p => p < 100);
            var threeDigit = Pandigital.GetNumbers(range, 9, false).Where(p => p < 1000).ToList();
            var fourDigit = Pandigital.GetNumbers(range, 9, false).Where(p => p < 10000).ToList();


            foreach (var num1 in oneAndTwoDigit)
            {
                var num1s = num1.ToString();
                var list = num1s.Length == 1 ? fourDigit : threeDigit;    
                foreach (var num2 in list)
                {
                    var ans = num1 * num2;
                    var fullNum = num1s + num2.ToString() + ans.ToString();
                    if (fullNum.Length == 9 && IsPandigital(fullNum))
                        yield return new Tuple<int, int, int>((int)num1, (int)num2, (int)ans);
                }
            }
        }

        static bool IsPandigital(uint num)
        {
            var arr = num.ToString().ToArray();
            return carr.All(c => arr.Contains(c));
        }

        static bool IsPandigital(string num)
        {
            var arr = num.ToArray();
            return carr.All(c => arr.Contains(c));
        }
    }
}
