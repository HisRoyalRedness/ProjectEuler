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
    [Solution("21124")]
    class Problem017Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=17
        /// 
        /// If the numbers 1 to 5 are written out in words: one, two, 
        /// three, four, five, then there are 3 + 3 + 5 + 4 + 4 = 19 
        /// letters used in total.
        /// 
        /// If all the numbers from 1 to 1000 (one thousand) inclusive 
        /// were written out in words, how many letters would be used?
        /// 
        /// NOTE: Do not count spaces or hyphens. For example, 342 
        /// (three hundred and forty-two) contains 23 letters and 115 
        /// (one hundred and fifteen) contains 20 letters. The use of 
        /// "and" when writing out numbers is in compliance with 
        /// British usage.
        /// 
        /// Answer: 21124
        /// </summary>
        public Problem017Generator() { }

        static IObservable<int> Generate()
        {
            return Observable.Return(0);
        }

        protected override string InternalCalculateSolution()
        {
            return Enumerable.Range(1, 1000)
                .Select(num => num.ToWordNumber())
                .Select(num => num.Replace(" ", "").Replace("-", "").Length)
                .Sum().ToString();
        }
    }    
}
