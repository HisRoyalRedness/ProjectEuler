using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
    Euler Problem 51

    Keith Fletcher
    Jul 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    [Solution("121313")]
    public class Problem51 : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=51
        /// 
        /// By replacing the 1st digit of the 2-digit number *3, it turns 
        /// out that six of the nine possible values: 13, 23, 43, 53, 73, 
        /// and 83, are all prime.
        /// 
        /// By replacing the 3rd and 4th digits of 56**3 with the same 
        /// digit, this 5-digit number is the first example having seven 
        /// primes among the ten generated numbers, yielding the family: 
        /// 56003, 56113, 56333, 56443, 56663, 56773, and 56993. 
        /// Consequently 56003, being the first member of this family, 
        /// is the smallest prime with this property.
        /// 
        /// Find the smallest prime which, by replacing part of the 
        /// number (not necessarily adjacent digits) with the same digit, 
        /// is part of an eight prime value family.
        /// 
        /// Answer: 121313
        /// </summary>

        protected override string InternalSolve()
        {
            var sixDigitPrimes = Primes
                .Sequence(100000)
                .TakeWhile(p => p < 1000000)
                .Select(p => p.ToString())
                .ToList();

            var min = int.Parse(sixDigitPrimes.Last());
            
            for(var i = 0; i < 6; ++i)
            {
                for(var j = i; j < 6; ++j)
                {
                    for (var k = i; k < 6; ++k)
                    {
                        var primes = sixDigitPrimes
                            .Where(p => IsValid(p, i, j, k))
                            .GroupBy(p => Blank(p, i, j, k))
                            .Where(g => g.Count() == 8)
                            .Select(g => int.Parse(g.First()));

                        foreach (var p in primes)
                            if (p < min)
                                min = p;
                    }
                }
            }

            return min.ToString(); ;
        } 

        static string Blank(string input, int i, int j, int k)
        {
            var q = input.ToArray();
            q[i] = '_';
            q[j] = '_';
            q[k] = '_';
            return new string(q);
        }

        static bool IsValid(string input, int i, int j, int k)
            => input[i] == input[j] && input[j] == input[k];
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
