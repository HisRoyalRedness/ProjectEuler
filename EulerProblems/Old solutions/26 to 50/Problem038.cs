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
    [Solution("932718654")]
    class Problem038Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=38
        /// 
        /// Take the number 192 and multiply it by each of 1, 2, and 3:
        /// 
        ///   192 × 1 = 192
        ///   192 × 2 = 384
        ///   192 × 3 = 576
        ///   
        /// By concatenating each product we get the 1 to 9 pandigital, 
        /// 192384576. We will call 192384576 the concatenated product 
        /// of 192 and (1,2,3)
        /// 
        /// The same can be achieved by starting with 9 and multiplying 
        /// by 1, 2, 3, 4, and 5, giving the pandigital, 918273645, which 
        /// is the concatenated product of 9 and (1,2,3,4,5).
        /// 
        /// What is the largest 1 to 9 pandigital 9-digit number that 
        /// can be formed as the concatenated product of an integer 
        /// with (1,2, ... , n) where n > 1?
        /// 
        /// Answer: 932718654
        /// </summary>
        public Problem038Generator() { }

        char[] carr = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        // CSP!!!!!!!

        protected override string InternalCalculateSolution()
        {
            uint max = 0;
            uint rollOver = 10;
            for (uint t0 = 1; t0 < 100000; t0++)
            {
                if (t0 == rollOver)
                {
                    rollOver *= 10;
                    if (max > 0)
                    {
                        t0 = uint.Parse(max.ToString().Substring(0, t0.ToString().Length));
                    }
                }
                
                uint t1 = t0;
                var sb = new StringBuilder();
                for (uint i = 1; sb.Length < 9; i++)
                {
                    t1 *= i;
                    sb.Append(t1);
                }

                if (sb.Length == 9)
                {
                    if (IsPandigital(sb.ToString()))
                    {
                        var num = uint.Parse(sb.ToString());
                        if (num > max)
                        {
                            max = num;
                            Console.WriteLine(max);
                        }
                    }
                }
            }

            return max.ToString();
            
        }

        bool IsPandigital(string num)
        { return carr.All(c => num.Contains(c)); }

    }

    static class Prob38Ext
    {

        public static uint Generate(this int start)
        {
            if (start % 100000 == 0)
                Console.WriteLine((double)start / 5000000d);
            uint t0 = (uint)start;
            uint t1 = t0;
            for (uint i = 1; true; i++)
            {
                t1 *= i;
                if (t1 > 1000000000)                
                    return t0;
                
                t0 = t1;
            }            
        }
    }
}
