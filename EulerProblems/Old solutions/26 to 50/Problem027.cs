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
    [Solution("-59231")]
    class Problem027Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=27
        /// 
        /// Euler published the remarkable quadratic formula:
        /// n² + n + 41
        /// It turns out that the formula will produce 40 primes 
        /// for the consecutive values n = 0 to 39. However, when 
        /// n = 40, 402 + 40 + 41 = 40(40 + 1) + 41 is divisible 
        /// by 41, and certainly when n = 41, 41² + 41 + 41 is 
        /// clearly divisible by 41.
        /// 
        /// Using computers, the incredible formula  
        /// n² − 79n + 1601 was discovered, which produces 80 
        /// primes for the consecutive values n = 0 to 79. 
        /// The product of the coefficients, −79 and 1601, 
        /// is −126479.
        /// 
        /// Considering quadratics of the form:
        /// n² + an + b, where |a| < 1000 and |b| < 1000
        /// where |n| is the modulus/absolute value of n 
        /// e.g. |11| = 11 and |−4| = 4
        /// 
        /// Find the product of the coefficients, a and b, for the 
        /// quadratic expression that produces the maximum number 
        /// of primes for consecutive values of n, starting 
        /// with n = 0.
        /// 
        /// Answer: -59231
        /// </summary>
        public Problem027Generator() { }

        protected override string InternalCalculateSolution()
        {
            checked
            {
                var primes = new HashSet<uint>(Prime.FromFile().Take(100000).Select(p => (uint)p));
                var powers = Enumerable.Range(0, 1000).Select(n => n * n).ToArray();
                int max = 0;
                int aa = 0;
                int bb = 0;
                int limit = 1000;

                for (int a = -limit; a <= limit; a++)
                {
                    for (int b = -limit; b <= limit; b++)
                    {
                        //var count = PrimeCount(a, b, primes);

                        int n = 0;
                        while (true)
                        {

                            var x = (uint)Math.Abs((powers[n] + a * n + b));

                            if (primes.Contains(x))
                                n++;
                            else
                                break;
                        }


                        if (n > max)
                        {
                            max = n;
                            aa = a;
                            bb = b;
                        }
                        //WriteLine(count);
                    }

                    if (a % 100 == 0)
                        WriteLine(a);
                }

                WriteLine("{0} x {1} = {2}, {3} primes", aa, bb, aa * bb, max);
                return (aa * bb).ToString();
            }
        }        
    }
}
