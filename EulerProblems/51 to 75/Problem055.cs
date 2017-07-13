using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
    Euler Problem 55

    Keith Fletcher
    Mar 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    [Solution("249")]
    public class Problem55 : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=55
        /// 
        /// If we take 47, reverse and add, 47 + 74 = 121, which is palindromic.
        /// 
        /// Not all numbers produce palindromes so quickly.For example,
        ///         349 + 943 = 1292,
        ///         1292 + 2921 = 4213
        ///         4213 + 3124 = 7337
        /// 
        /// That is, 349 took three iterations to arrive at a palindrome.
        /// Although no one has proved it yet, it is thought that some numbers, 
        /// like 196, never produce a palindrome. A number that never forms a 
        /// palindrome through the reverse and add process is called a Lychrel 
        /// number. Due to the theoretical nature of these numbers, and for the 
        /// purpose of this problem, we shall assume that a number is Lychrel 
        /// until proven otherwise. In addition you are given that for every 
        /// number below ten-thousand, it will either (i) become a palindrome 
        /// in less than fifty iterations, or, (ii) no one, with all the 
        /// computing power that exists, has managed so far to map it to a 
        /// palindrome. In fact, 10677 is the first number to be shown to require 
        /// over fifty iterations before producing a palindrome: 
        ///     4668731596684224866951378664 (53 iterations, 28-digits).
        ///     
        /// Surprisingly, there are palindromic numbers that are themselves 
        /// Lychrel numbers; the first example is 4994.
        /// 
        /// How many Lychrel numbers are there below ten-thousand?
        /// 
        /// NOTE: Wording was modified slightly on 24 April 2007 to emphasise the theoretical nature of Lychrel numbers.
        /// 
        /// Answer: 249
        /// </summary>

        protected override string InternalSolve()
        {
            var limit = 10000;

            return Enumerable.Range(1, (int)limit - 1).Select(n => new BigInteger(n))
                .AsParallel()
                .Where(n => n.IsLychrel())
                .Count()
                .ToString();
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
