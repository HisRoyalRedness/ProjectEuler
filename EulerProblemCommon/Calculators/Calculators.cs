using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
    Common calculators

    Keith Fletcher
    May 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    public static class Calculators
    {
        public static bool IsOdd(this ulong number) => (number & 1) == 1;
        public static bool IsEven(this ulong number) => (number & 1) == 0;

        #region GCD and LCM
        public static ulong LCM(this ulong u, ulong v)
            => (u * v) / GCD(u,v);

        public static ulong LCM(params ulong[] numbers)
            => numbers.Product() / GCD(numbers);


        /// <summary>
        /// Determine the greatest common denominator with an arbitrary list
        /// on numbers. It works by recursively calling the binary GCD algorithm.
        /// GCD(a,b,c,d, ...) = GCD(a,GCD(b,GCD(c, GCD(d, ...))))
        /// </summary>
        /// <remarks>https://en.wikipedia.org/wiki/Binary_GCD_algorithm</remarks>
        public static ulong GCD(params ulong[] numbers)
        {
            if ((numbers?.Length ?? 0) == 0)
                return 0;
            if (numbers.Length == 1)
                return numbers[0];

            var i = numbers.Length - 1;
            while(i > 0)
                numbers[i - 1] = GCD(numbers[i - 1], numbers[i--]);
            return numbers[0];
        }


        /// <summary>
        /// Determine the greatest common denominator
        /// </summary>
        /// <remarks>https://en.wikipedia.org/wiki/Binary_GCD_algorithm</remarks>
        public static ulong GCD(this ulong u, ulong v)
        {
            int shift;

            /* GCD(0,v) == v; GCD(u,0) == u, GCD(0,0) == 0 */
            if (u == 0) return v;
            if (v == 0) return u;

            /* Let shift := lg K, where K is the greatest power of 2
                  dividing both u and v. */
            for (shift = 0; ((u | v) & 1) == 0; ++shift)
            {
                u >>= 1;
                v >>= 1;
            }

            while ((u & 1) == 0)
                u >>= 1;

            /* From here on, u is always odd. */
            do
            {
                /* remove all factors of 2 in v -- they are not common */
                /*   note: v is not zero, so while will terminate */
                while ((v & 1) == 0)  /* Loop X */
                    v >>= 1;

                /* Now u and v are both odd. Swap if necessary so u <= v,
                   then set v = v - u (which is even). For bignums, the
                   swapping is just pointer movement, and the subtraction
                   can be done in-place. */
                if (u > v)
                {
                    var t = v; v = u; u = t;
                }  // Swap u and v.
                v = v - u;                       // Here v >= u.
            } while (v != 0);

            /* restore common factors of 2 */
            return u << shift;
        }
        #endregion GCD and LCM
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
