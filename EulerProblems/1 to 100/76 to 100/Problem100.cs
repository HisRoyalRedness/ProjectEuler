using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
    Euler Problem 100

    Keith Fletcher
    Jul 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    #region Analysis
    [Analysis(@"
Hello
=====

1. some stuff
2. some more stuff
")]
    #endregion Analysis
    [Solution("12345")]
    [Summary(@"
Arranged probability
--------------------

If a box contains twenty-one coloured discs, composed of 
fifteen blue discs and six red discs, and two discs were 
taken at random, it can be seen that the probability of 
taking two blue discs, $P(BB) = \left(\frac{15}{21}\right)
\times\left(\frac{14}{20}\right) = \frac{1}{2}$.
   
The next such arrangement, for which there is exactly 50% 
chance of taking two blue discs at random, is a box 
containing eighty-five blue discs and thirty-five red 
discs.

By finding the first arrangement to contain over 
$10^12$ = 1,000,000,000,000 discs in total, determine the 
number of blue discs that the box would contain.
")]
    public class Problem100 : ProblemBase
    {
        protected override string InternalSolve()
        {
            //BigInteger j;
            // https://stackoverflow.com/questions/3432412/calculate-square-root-of-a-biginteger-system-numerics-biginteger

            // blues = (1 + SQRT(1 + 2 * Total * (Total - 1))) / 2

            ulong limit = 100000000;
            double limitD = (double)limit;
            ulong test = 0;
            double testD = 0;

            var sw1 = new Stopwatch();
            sw1.Start();
            for (ulong i = 0; i < limit; ++i)
                test = IsPerfectSquare(limit);
            sw1.Stop();
            Console.WriteLine(sw1.ElapsedMilliseconds);


            var sw2 = new Stopwatch();
            sw2.Start();
            for (ulong i = 0; i < limit; ++i)
                testD = Math.Sqrt(limitD);
            sw2.Stop();
            Console.WriteLine(sw2.ElapsedMilliseconds);


            //var x1 = ((ulong)100).IntSquareRoot();
            //var x2 = ((ulong)64).IntSquareRoot();
            //var x3 = ((ulong)10).IntSquareRoot();
            //var x4 = ((ulong)9).IntSquareRoot();
            //var x5 = ((ulong)8).IntSquareRoot();

            //var y1 = ((uint)100).IntSquareRoot();
            //var y2 = ((uint)64).IntSquareRoot();
            //var y3 = ((uint)10).IntSquareRoot();
            //var y4 = ((uint)9).IntSquareRoot();
            //var y5 = ((uint)8).IntSquareRoot();


            return "";
        }

        const ulong START_MASK = 1 << ((sizeof(ulong) * 8) - 2);
        static ulong IsPerfectSquare(ulong square)
        {
            ulong mask = START_MASK;
            ulong root = 0;
            ulong remainder = square;

            while (mask != 0)
            {
                if ((root + mask) <= remainder)
                {
                    remainder -= (root + mask);
                    root += (mask << 1);
                }
                root >>= 1;
                mask >>= 2;
            }

            return root;
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
