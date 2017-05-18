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
    [Solution("210")]
    class Problem040Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=40
        /// 
        /// An irrational decimal fraction is created by concatenating 
        /// the positive integers:
        /// 
        /// 0.123456789101112131415161718192021...
        ///             /|\
        ///              |
        /// 
        /// It can be seen that the 12th digit of the fractional part is 1.
        /// If d_n represents the nth digit of the fractional part, find the 
        /// value of the following expression.
        /// 
        /// d_1 × d_10 × d_100 × d_1000 × d_10000 × d_100000 × d_1000000
        /// 
        /// Answer: 210
        /// </summary>
        public Problem040Generator() { }

        protected override string InternalCalculateSolution()
        {
            var tt = Enumerable.Range(1, 1000000)
                .SelectMany(i => i.ToString().ToArray())
                .ToList();

            return (int.Parse(tt[1 - 1].ToString()) *
                int.Parse(tt[10 - 1].ToString()) *
                int.Parse(tt[100 - 1].ToString()) *
                int.Parse(tt[1000 - 1].ToString()) *
                int.Parse(tt[10000 - 1].ToString()) *
                int.Parse(tt[100000 - 1].ToString()) *
                int.Parse(tt[1000000 - 1].ToString())).ToString();
        }
    }
}
