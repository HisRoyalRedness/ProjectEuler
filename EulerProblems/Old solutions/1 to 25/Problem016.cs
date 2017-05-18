using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Numerics;
using System.Reflection;
using System.Reactive.Linq;

namespace fletcher.org
{
    [Export(typeof(IProblem))]
    [Solution("1366")]
    class Problem016Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=16
        /// 
        /// 2^(15) = 32768 and the sum of its digits is 3 + 2 + 7 + 6 + 8 = 26.
        /// 
        /// What is the sum of the digits of the number 2^(1000)?
        /// 
        /// Answer: 1366
        /// </summary>
        public Problem016Generator() { }

        static IObservable<int> Generate()
        {
            return Observable.Return(0);
        }

        protected override string InternalCalculateSolution()
        {            
            return BigInteger.Pow(2, 1000).ToString()
                .ToNumericSequence<uint>()
                .Sum().ToString();            
        }
    }
}
