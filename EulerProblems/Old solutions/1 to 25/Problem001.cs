using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Reactive.Linq;

namespace fletcher.org
{
    [Export(typeof(IProblem))]
    [Solution("233168")]
    public class Problem001Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=1
        /// 
        /// If we list all the natural numbers below 10 that are multiples of 3 or 5, 
        /// we get 3, 5, 6 and 9. The sum of these multiples is 23.
        /// 
        /// Find the sum of all the multiples of 3 or 5 below 1000.          
        /// 
        /// Answer: 233168
        /// </summary>
        public Problem001Generator() { }        

        protected override string InternalCalculateSolution()
        {
            const int start = 1;
            const int end = 1000;  // Not inclusive!

            return Observable.Range(start, end - start)
                .Where(i => (i % 3 == 0 || i % 5 == 0))
                .Sum().ToString();
        }
    }
}
