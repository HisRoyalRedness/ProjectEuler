﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    Euler Problem 11

    Keith Fletcher
    Jun 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    [Solution("70600674")]
    public class Problem12 : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=12
        ///
        /// The sequence of triangle numbers is generated by adding the
        /// natural numbers. So the 7^(th) triangle number would be
        /// 1 + 2 + 3 + 4 + 5 + 6 + 7 = 28. The first ten terms would be:
        ///
        /// 1, 3, 6, 10, 15, 21, 28, 36, 45, 55, ...
        ///
        /// Let us list the factors of the first seven triangle numbers:
        ///
        ///    1: 1
        ///    3: 1,3
        ///    6: 1,2,3,6
        ///   10: 1,2,5,10
        ///   15: 1,3,5,15
        ///   21: 1,3,7,21
        ///   28: 1,2,4,7,14,28
        ///
        /// We can see that 28 is the first triangle number to have
        /// over five divisors.
        ///
        /// What is the value of the first triangle number to have
        /// over five hundred divisors?
        ///
        /// Answer: 76576500
        /// </summary>

        protected override string InternalSolve()
        {
            var t = TriangleNumber.Sequence().Take(10).ToList();
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
