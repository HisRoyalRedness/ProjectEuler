using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
    Euler Problem 92

    Keith Fletcher
    Jul 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    [Solution("8581146")]
    public class Problem92 : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=92
        /// 
        /// A number chain is created by continuously adding the square of the 
        /// digits in a number to form a new number until it has been seen before.
        /// 
        /// For example,
        ///     44 → 32 → 13 → 10 → 1 → 1
        ///     85 → 89 → 145 → 42 → 20 → 4 → 16 → 37 → 58 → 89
        ///     
        /// Therefore any chain that arrives at 1 or 89 will become stuck in an 
        /// endless loop.What is most amazing is that EVERY starting number will 
        /// eventually arrive at 1 or 89.
        /// 
        /// How many starting numbers below ten million will arrive at 89?
        /// 
        /// Answer: 8581146
        /// </summary>

        protected override string InternalSolve()
        {
            var limit = 10000000;
            return Enumerable.Range(1, limit - 1)
                .Where(i => CalcChain(i) == 89)
                .Count()
                .ToString();
        }
        
        int CalcChain(int number)
        {
            var num = number;
            var nums = new List<int>();
            while(num != 89 && num != 1)
            {
                if (_chainResult.ContainsKey(num))
                    num = _chainResult[num];
                else
                {
                    nums.Add(num);
                    num = SumOfDigitSquares(num);
                }
            }
            foreach (var n in nums)
                _chainResult[n] = num;

            return num;
        }

        int SumOfDigitSquares(int num) => num.ToString().Select(d => (int)d - 0x30).Select(d => d * d).Sum();


        Dictionary<int, int> _chainResult = new Dictionary<int, int>();
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
