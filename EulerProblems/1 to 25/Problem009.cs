using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    Euler Problem 9

    Keith Fletcher
    Jun 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    [Solution("31875000")]
    public class Problem9 : ProblemBase
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

        protected override string InternalSolve()
        {

            var squares = Enumerable.Range(1, 1000000)
                .Select(i => new { Num = i, Sqr = i * i })
                .Where(a => a.Num < 1000)
                .ToDictionary(a => a.Num, a => a.Sqr);
            var roots = squares
                .ToDictionary(kv => kv.Value, kv => kv.Key);

            var first = squares.First().Key;
            var last = squares.Last().Key;
            for(var a = first; a < last; ++a)
            {
                for (var b = a + 1; b < last; ++b)
                {
                    var sqrSum = squares[a] + squares[b];
                    if (roots.ContainsKey(sqrSum))
                    {
                        var c = roots[sqrSum];
                        if (a + b + c == 1000)
                            return (a * b * c).ToString();
                    }
                }
            }
            return "";
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
