using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    Euler Problem 4

    Keith Fletcher
    May 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    [Title("Largest palindrome product")]
    [Solution("906609")]
    public class Problem4 : ProblemBase
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

        protected override string InternalSolve()
        {
            const int limit = 999;
            return GetPalindromes(limit)
                .Take(10)
                .OrderByDescending(p => p)
                .First()
                .Prod
                .ToString();
        }

        IEnumerable<Product> GetPalindromes(int limit)
        {
            for (var i = limit; i >= 0; --i)
            {
                for (var j = limit; j >= i; --j)
                {
                    var prod = new Product(i, j);
                    if (prod.IsPalindrome)
                        yield return prod;
                }
            }
        }

        struct Product : IComparable<Product>
        {
            public Product(int a, int b)
            {
                A = a;
                B = b;
                Prod = a * b;

                IsPalindrome = false;
                var s = Prod.ToString();
                var x = 0;
                var y = s.Length - 1;
                while (s[x++] == s[y--])
                {
                    if (x >= y)
                    {
                        IsPalindrome = true;
                        break;
                    }
                }
            }
            public int A;
            public int B;
            public int Prod;
            public bool IsPalindrome;
            public override string ToString() => $"{A}x{B}={Prod}";
            public int CompareTo(Product other) => Prod.CompareTo(other.Prod);
        }
    }
}

/*
This is free and unencumbered software released into the public domain.

Anyone is free to copy, modify, publish, use, compile, sell, or
distribute this software, either in source code form or as a compiled
binary, for any purpose, commercial or non-commercial, and by any
means.

In jurisdictions that recognize copyright laws, the author or authors
of this software dedicate any and all copyright interest in the
software to the public domain. We make this dedication for the benefit
of the public at large and to the detriment of our heirs and
successors. We intend this dedication to be an overt act of
relinquishment in perpetuity of all present and future rights to this
software under copyright law.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.

For more information, please refer to <http://unlicense.org>
*/
