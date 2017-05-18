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
    [Solution("4782")]
    class Problem025Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=25
        /// 
        /// The Fibonacci sequence is defined by the recurrence relation:
        /// 
        /// F_(n) = F_(n−1) + F_(n−2), where F_(1) = 1 and F_(2) = 1.
        /// 
        /// Hence the first 12 terms will be:
        /// 
        /// F_(1) = 1
        /// F_(2) = 1
        /// F_(3) = 2
        /// F_(4) = 3
        /// F_(5) = 5
        /// F_(6) = 8
        /// F_(7) = 13
        /// F_(8) = 21
        /// F_(9) = 34
        /// F_(10) = 55
        /// F_(11) = 89
        /// F_(12) = 144
        /// 
        /// The 12th term, F_(12), is the first term to contain 
        /// three digits.
        /// 
        /// What is the first term in the Fibonacci sequence to 
        /// contain 1000 digits?
        /// 
        /// Answer: 4782
        /// </summary>
        public Problem025Generator() { }

        protected override string InternalCalculateSolution()
        {
            BigInteger limit = BigInteger.Pow(10,999);
            var fib = Fibonacci.BigSequence
                .Select((f, i) => new { Fib = f, Index = i })
                .SkipWhile(f => f.Fib < limit)
                .First();

            WriteLine("Index: {0}  Fib: {1}", fib.Index, fib.Fib);
            return fib.Index.ToString();
        }
    }
}
