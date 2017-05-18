using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Numerics;
using System.Reflection;
using System.Diagnostics;
using System.Threading;

namespace fletcher.org
{
    [Export(typeof(IProblem))]
    [Solution("985")]
    class Problem026Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=26
        ///
        /// A unit fraction contains 1 in the numerator. The decimal
        /// representation of the unit fractions with denominators
        /// 2 to 10 are given:
        ///
        ///   1/2	= 	0.5
        ///   1/3	= 	0.(3)
        ///   1/4	= 	0.25
        ///   1/5	= 	0.2
        ///   1/6	= 	0.1(6)
        ///   1/7	= 	0.(142857)
        ///   1/8	= 	0.125
        ///   1/9	= 	0.(1)
        ///   1/10	= 	0.1
        ///
        /// Where 0.1(6) means 0.166666..., and has a 1-digit recurring
        /// cycle. It can be seen that 1/7 has a 6-digit recurring
        /// cycle.
        ///
        /// Find the value of d < 1000 for which 1/d contains the
        /// longest recurring cycle in its decimal fraction part.
        ///
        /// Answer: 983
        /// </summary>
        public Problem026Generator() { }

        protected override string InternalCalculateSolution()
        {
            ulong dividend = 1;
            int maxRepeats = 0;
            int maxD = 0;

            return Enumerable.Range(2, 1000)
                .Select(d =>
                {
                    var count = dividend.GetRepeatingDecimals((ulong)d).Count();
                    if (count > maxRepeats)
                    {
                        maxRepeats = count;
                        maxD = d;
                    }
                    return new { D = d, Repeats = count };
                })
                .OrderByDescending(a => a.Repeats)
                .First().D
                .ToString();
        }

    }

    static class Problem026Extensions
    {
        public static IEnumerable<T> InfiniteSeries<T>(T item)
        {
            while (true)
                yield return item;
        }

        public static IEnumerable<int> GetRepeatingDecimals(this ulong dividend, ulong divisor)
        {
            if (divisor == 0)
                throw new DivideByZeroException();

            var nums = dividend.ToString()
                .ToNumericSequence()
                .Concat(InfiniteSeries<ulong>(0))
                .GetEnumerator();

            ulong num = 0;
            bool run = true;
            var dict = new Dictionary<ulong, int>();

            do
            {
                while (divisor > num && run)
                {
                    if (nums.MoveNext())
                        num = (num * 10) + nums.Current;
                    else
                    {
                        run = (num != 0);
                        num *= 10;
                    }
                }

                var a = (int)(num / divisor);
                if (dict.ContainsKey(num))
                    return dict.SkipWhile(x => x.Key != num).Select(x => x.Value);
                else
                    dict.Add(num, a);

                num %= divisor;
            }
            while (num != 0);

            return Enumerable.Empty<int>();
        }
    }
}
