using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace fletcher.org
{
    [Export(typeof(IProblem))]
    [Solution("31875000")]
    class Problem009Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=9
        /// 
        /// A Pythagorean triplet is a set of three natural numbers, a  < b  < c, for which,
        /// a^(2) + b^(2) = c^(2)
        /// For example, 3^(2) + 4^(2) = 9 + 16 = 25 = 5^(2).
        /// 
        /// There exists exactly one Pythagorean triplet for which a + b + c = 1000.
        /// 
        /// Find the product abc.
        /// 
        /// Answer: 31875000
        /// </summary>
        public Problem009Generator() { }

        protected override string InternalCalculateSolution()
        {
            for (int c = 1000; c >= 334; c--)
            {
                for (int b = c - 1; b >= 2; b--)
                {
                    for (int a = b - 1; a >= 1; a--)
                    {
                        if (Validate(a, b, c))
                        {
                            WriteLine("a={0}, b={1}, c={2}, abc={3}", a, b, c, a * b * c);
                            return (a * b * c).ToString();
                        }
                    }
                }
            }
            return "";
        }

        bool Validate(int a, int b, int c)
        {
            return (a + b + c == 1000) &&
                   (a * a + b * b == c * c);
        }
    }
}
