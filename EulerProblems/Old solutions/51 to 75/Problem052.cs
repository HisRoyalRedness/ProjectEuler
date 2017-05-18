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
    [Solution("142857")]
    class Problem052Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=52
        /// 
        /// It can be seen that the number, 125874, and its double, 
        /// 251748, contain exactly the same digits, but in a 
        /// different order.
        /// 
        /// Find the smallest positive integer, x, such that 2x, 3x, 
        /// 4x, 5x, and 6x, contain the same digits.
        /// 
        /// Answer: 142857
        /// </summary>
        public Problem052Generator() { }

        protected override string InternalCalculateSolution()
        {
            ulong i = 10;
            ulong limit = ulong.MaxValue - 1;
            while (++i < limit)
            {
                var ii = Arrange(i);
                if (Arrange(i * 2) != ii)
                    continue;
                if (Arrange(i * 3) != ii)
                    continue;
                if (Arrange(i * 4) != ii)
                    continue;
                if (Arrange(i * 5) != ii)
                    continue;
                if (Arrange(i * 6) != ii)
                    continue;
                return i.ToString();
            }
            return "";
        }

        static ulong Arrange(ulong number)
        { return ulong.Parse(new string(number.ToString().OrderBy(c => c).ToArray())); }
    }
}
