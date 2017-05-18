using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.ComponentModel.Composition;

namespace fletcher.org
{
    [Export(typeof(IProblem))]
    [Solution("906609")]
    class Problem004Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=4
        ///
        /// A palindromic number reads the same both ways. The largest palindrome
        /// made from the product of two 2-digit numbers is 9009 = 91 × 99.
        ///
        /// Find the largest palindrome made from the product of two 3-digit numbers.
        ///
        /// Answer: 906609
        /// </summary>
        public Problem004Generator() { }

        static IEnumerable<Tuple<int, int>> GetNumbers()
        {
            for (int i = 999; i >= 100; i--)
                for (int j = 999; j >= i; j--)
                    yield return new Tuple<int, int>(i, j);
        }

        protected override string InternalCalculateSolution()
        {
            var answer =
                (from num in GetNumbers()
                 let prod = num.Item1 * num.Item2
                 where prod.IsPalindrome()
                 select new { Product = prod, Num1 = num.Item1, Num2 = num.Item2 })
                .MaxBy(item => item.Product)
                .First();
            
            WriteLine("{0} x {1} = {2}", answer.Num1, answer.Num2, answer.Product);
            return answer.Product.ToString();

        }
    }
}
