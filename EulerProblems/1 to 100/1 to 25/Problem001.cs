﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    Euler Problem 1

    Keith Fletcher
    Mar 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    [Title("Multiples of 3 and 5")]
    [Solution("233168")]
    public class Problem1 : ProblemBase
    {
        /// http://projecteuler.net/index.php?section=problems&id=1
        /// 
        /// If we list all the natural numbers below 10 that are multiples of 3 or 5, 
        /// we get 3, 5, 6 and 9. The sum of these multiples is 23.
        /// 
        /// Find the sum of all the multiples of 3 or 5 below 1000.          
        /// 
        /// Answer: 233168
        /// </summary>
        /// 
        protected override string InternalSolve()
        {
            int m3 = 3;
            int m5 = 5;
            int sum = 3 + 5;
            const int max = 1000;

            while (true)
            {
                if (m3 < m5)
                {
                    m3 += 3;
                    if (m3 < max)
                        if (m3 != m5)
                            sum += m3;
                }
                else
                {
                    m5 += 5;
                    if (m5 < max)
                        sum += m5;
                    else if (m3 >= max)
                            break;
                }
            }
            return sum.ToString();
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
