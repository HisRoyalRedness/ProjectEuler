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
    using Combo = Tuple<byte[], byte[]>;

    [Solution("")]
    public class Problem100 : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=100
        /// 
        /// If a box contains twenty-one coloured discs, composed of 
        /// fifteen blue discs and six red discs, and two discs were 
        /// taken at random, it can be seen that the probability of 
        /// taking two blue discs, P(BB) = (15/21)×(14/20) = 1/2.
        /// 
        /// The next such arrangement, for which there is exactly 50% 
        /// chance of taking two blue discs at random, is a box 
        /// containing eighty-five blue discs and thirty-five red 
        /// discs.
        /// 
        /// By finding the first arrangement to contain over 
        /// 10^12 = 1,000,000,000,000 discs in total, determine the 
        /// number of blue discs that the box would contain.
        /// 
        /// Answer: 
        /// </summary>

        protected override string InternalSolve()
        {
            BigInteger j;
            // https://stackoverflow.com/questions/3432412/calculate-square-root-of-a-biginteger-system-numerics-biginteger

            // blues = (1 + SQRT(1 + 2 * Total * (Total - 1))) / 2

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
