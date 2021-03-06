﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    Euler Problem 6

    Keith Fletcher
    Jun 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    [Title("Sum square difference")]
    [Solution("25164150")]
    public class Problem6 : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=6
        /// 
        /// The sum of the squares of the first ten natural numbers is,
        /// 
        /// 1^(2) + 2^(2) + ... + 10^(2) = 385
        /// 
        /// The square of the sum of the first ten natural numbers is,
        /// 
        /// (1 + 2 + ... + 10)^(2) = 55^(2) = 3025
        /// 
        /// Hence the difference between the sum of the squares of the 
        /// first ten natural numbers and the square of the sum is 
        /// 3025 − 385 = 2640.
        /// 
        /// Find the difference between the sum of the squares of the 
        /// first one hundred natural numbers and the square of the sum.
        /// 
        /// Answer: 25164150
        /// </summary>

        protected override string InternalSolve()
        {
            // http://en.wikipedia.org/wiki/Square_pyramidal_number

            var start = 1;
            var end = 100;

            var range = Enumerable.Range(start, end).Select(i => (ulong)i);
            var sum = range.Sum();
            var sqOfSums = sum * sum;
            var sumOfSqs = range.Aggregate((acc, num) => acc + (num * num));
            return (sqOfSums - sumOfSqs).ToString();
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
