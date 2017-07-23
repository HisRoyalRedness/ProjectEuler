using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
    Euler Problem 65

    Keith Fletcher
    Jul 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    [Solution("272")]
    public class Problem65 : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=65
        /// 
        /// The square root of 2 can be written as an infinite continued fraction.
        /// √2 = 1 + 1 / (2 + 1 / ( 2 + 1 / (2 + ...)))
        /// 
        /// The infinite continued fraction can be written, 
        /// √2 = [1; (2)], (2) indicates that 2 repeats ad infinitum. 
        /// In a similar way, √23 = [4;(1,3,1,8)].
        /// 
        /// It turns out that the sequence of partial values of continued fractions 
        /// for square roots provide the best rational approximations.Let us consider 
        /// the convergents for √2.
        /// 
        /// 1 + (1/2) = 3/2
        /// 1 + (1 / (2 + 1/2)) = 7/5
        /// 1 + (1 / (2 + 1 / (2 + 1/2))) = 17/12
        /// /// 1 + (1 / (2 + 1 / (2 + 1 / (2 + 1/2)))) = 41/29
        /// 
        /// Hence the sequence of the first ten convergents for √2 are:
        /// 1, 3/2, 7/5, 17/12, 41/29, 99/70, 239/169, 577/408, 1393/985, 3363/2378, ...
        /// 
        /// What is most surprising is that the important mathematical constant,
        /// e = [2; 1,2,1, 1,4,1, 1,6,1 , ... , 1,2k,1, ...]. 
        /// 
        /// The first ten terms in the sequence of convergents for e are:
        /// 2, 3, 8/3, 11/4, 19/7, 87/32, 106/39, 193/71, 1264/465, 1457/536, ...
        /// 
        /// The sum of digits in the numerator of the 10th convergent is 1+4+5+7=17.
        /// 
        /// Find the sum of digits in the numerator of the 100th convergent of the 
        /// continued fraction for e.
        /// 
        /// Answer: 272
        /// </summary>

        protected override string InternalSolve()
        {
            return ContinuedFraction
                .Sequence(eContinuedFractionCoefficients().Select(n => (BigInteger)n))
                .Skip(99).First()
                .Numerator
                .ToString()
                .Select(c => c - 0x30)
                .Sum()
                .ToString();
        }

        static IEnumerable<ulong> eContinuedFractionCoefficients()
        {
            yield return 2;
            ulong i = 2;
            while(true)
            {
                yield return 1;
                yield return i;
                yield return 1;
                i += 2;
            }
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
