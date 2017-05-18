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
    [Solution("100")]
    class Problem033Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=33
        ///
        /// The fraction 49/98 is a curious fraction, as an inexperienced 
        /// mathematician in attempting to simplify it may incorrectly 
        /// believe that 49/98 = 4/8, which is correct, is obtained by 
        /// cancelling the 9s.
        /// 
        /// We shall consider fractions like, 30/50 = 3/5, to be trivial examples.
        /// 
        /// There are exactly four non-trivial examples of this type of 
        /// fraction, less than one in value, and containing two digits in 
        /// the numerator and denominator.
        /// 
        /// If the product of these four fractions is given in its lowest 
        /// common terms, find the value of the denominator.
        ///
        /// Answer: 100
        /// </summary>
        public Problem033Generator() { }

        [DebuggerDisplay("{Numerator}/{Denominator}")]
        struct Number
        {            
            public Number(int num, int den)
            {
                Numerator = num;
                Denominator = den;
            }

            public int Numerator;
            public int Denominator;
        }

        protected override string InternalCalculateSolution()
        {
            return DoubleDigits()
                .Aggregate(
                    new Number(1, 1),
                    (a, n) => { a.Numerator *= n.Numerator; a.Denominator *= n.Denominator; return a; },
                    a => new Tuple<ulong, ulong>((ulong)a.Numerator, (ulong)a.Denominator))
                .ReduceFraction()
                .Item2.ToString();
        }

        IEnumerable<Number> DoubleDigits()
        {
            int num2a, num2b, den2a, den2b;

            for (int i = 1; i < 10; i++)
            {
                for (int j = 1; j < 10; j++)
                {
                    num2a = i * 10 + j;
                    num2b = j * 10 + i;

                    for (int k = 1; k < 10; k++)
                    {
                        den2a = i * 10 + k;
                        den2b = k * 10 + i;

                        if (num2a < den2a && num2a * k == den2a * j)
                            yield return new Number(num2a, den2a);

                        if (i != j && num2b < den2a && num2b * k == den2a * j) 
                            yield return new Number(num2b, den2a);
                        if (num2a < den2b && num2a * k == den2b * j) 
                            yield return new Number(num2a, den2b);
                        if (i != j && i != k && num2b < den2b && num2b * k == den2b * j) 
                            yield return new Number(num2b, den2b);
                    }
                }
            }
        }
    }
}
