﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    Euler Problem 3

    Keith Fletcher
    Mar 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    [Title("Largest prime factor")]
    [Solution("6857")]
    public class Problem3 : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=3
        /// 
        /// The prime factors of 13195 are 5, 7, 13 and 29.
        /// 
        /// What is the largest prime factor of the number 600851475143 ?
        /// 
        /// Answer: 6857
        /// </summary>

        protected override string InternalSolve()
        {
            long limit = 600851475143;
            long max = 0;
            var stop = limit;

            foreach (var prime in Primes.Sequence(1, (UInt64)limit).Select(p => (long)p))
            {
                long result;
                stop = Math.DivRem(limit, prime, out result);
                if (result == 0)
                    max = prime;

                if (prime > stop)
                    break;
            }
            
            return max.ToString();
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
