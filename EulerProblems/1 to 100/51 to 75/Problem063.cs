using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
    Euler Problem 63

    Keith Fletcher
    Jul 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    [Solution("49")]
    public class Problem63 : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=63
        /// 
        /// The 5-digit number, 16807=7^5, is also a fifth power.
        /// Similarly, the 9-digit number, 134217728=8^9, is a ninth power.
        /// 
        /// How many n-digit positive integers exist which are also an nth power?
        /// 
        /// Answer: 49
        /// </summary>

        protected override string InternalSolve()
        {
            var numDigits = 1;
            var sum = 0;


            while(true)
            {
                var n = NumbersPerDigitCount(numDigits++);
                if (n == 0)
                    return sum.ToString();
                else
                    sum += n;
            }
        }        


        int NumbersPerDigitCount(int digitCount)
        {
            var num = double.Parse("1" + new string('0', digitCount - 1));
            var root = NthRoot(num, digitCount, 100);

            return 10 - (int)Math.Ceiling(root);
        }


        double NthRoot(double number, int n, int iterations = 5)
        {
            var func = new Func<double, double>(x => Math.Pow(x, n) - number);
            var deriv = new Func<double, double>(x => n * Math.Pow(x, n - 1));
            var guess = (double)(n + 1);
            for(var i = 0; i < iterations; ++i)
                guess = guess - (func(guess) / deriv(guess));
            return guess;
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
