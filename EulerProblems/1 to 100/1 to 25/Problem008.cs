using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    Euler Problem 8

    Keith Fletcher
    Jun 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    [Solution("23514624000")]
    public class Problem8 : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=8
        /// 
        /// The four adjacent digits in the 1000-digit number that have 
        /// the greatest product are 9 × 9 × 8 × 9 = 5832.
        /// 
        ///  73167176531330624919225119674426574742355349194934
        ///  96983520312774506326239578318016984801869478851843
        ///  85861560789112949495459501737958331952853208805511
        ///  12540698747158523863050715693290963295227443043557
        ///  66896648950445244523161731856403098711121722383113
        ///  62229893423380308135336276614282806444486645238749
        ///  30358907296290491560440772390713810515859307960866
        ///  70172427121883998797908792274921901699720888093776
        ///  65727333001053367881220235421809751254540594752243
        ///  52584907711670556013604839586446706324415722155397
        ///  53697817977846174064955149290862569321978468622482
        ///  83972241375657056057490261407972968652414535100474
        ///  82166370484403199890008895243450658541227588666881
        ///  16427171479924442928230863465674813919123162824586
        ///  17866458359124566529476545682848912883142607690042
        ///  24219022671055626321111109370544217506941658960408
        ///  07198403850962455444362981230987879927244284909188
        ///  84580156166097919133875499200524063689912560717606
        ///  05886116467109405077541002256983155200055935729725
        ///  71636269561882670428252483600823257530420752963450
        /// 
        /// Find the thirteen adjacent digits in the 1000-digit number 
        /// that have the greatest product. What is the value of this 
        /// product?
        /// 
        /// Answer: 23514624000
        /// </summary>

        const string number =
            "73167176531330624919225119674426574742355349194934" +
            "96983520312774506326239578318016984801869478851843" +
            "85861560789112949495459501737958331952853208805511" +
            "12540698747158523863050715693290963295227443043557" +
            "66896648950445244523161731856403098711121722383113" +
            "62229893423380308135336276614282806444486645238749" +
            "30358907296290491560440772390713810515859307960866" +
            "70172427121883998797908792274921901699720888093776" +
            "65727333001053367881220235421809751254540594752243" +
            "52584907711670556013604839586446706324415722155397" +
            "53697817977846174064955149290862569321978468622482" +
            "83972241375657056057490261407972968652414535100474" +
            "82166370484403199890008895243450658541227588666881" +
            "16427171479924442928230863465674813919123162824586" +
            "17866458359124566529476545682848912883142607690042" +
            "24219022671055626321111109370544217506941658960408" +
            "07198403850962455444362981230987879927244284909188" +
            "84580156166097919133875499200524063689912560717606" +
            "05886116467109405077541002256983155200055935729725" +
            "71636269561882670428252483600823257530420752963450";

        protected override string InternalSolve()
            => Products(13).Max().ToString();

        static IEnumerable<ulong> Products(int numLen)
        {
            var index = 0;
            var zeroCount = 0;
            var product = (ulong)1;
            var array = number.ToNumericSequence().Select(i => (ulong)i).ToArray();
            var limit = number.Length;
            var q = new Queue<ulong>();

            while (index < limit)
            {
                if (array[index] == 0)
                    ++zeroCount;
                else
                    product *= array[index];

                if (index >= numLen)
                {
                    if (array[index - numLen] == 0)
                        --zeroCount;
                    else
                        product /= array[index - numLen];
                }

                if (zeroCount == 0)
                    yield return product;
                ++index;
            }
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
