﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
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
    public static partial class Calculators
    {
        // Public methods in Calculator_Template
        static string Reverse(this string number) => new string(Enumerable.Reverse(number.ToString()).ToArray());

        // Public methods in Calculator_Template
        static bool IsPalindrome(this string number)
        {
            var left = 0;
            var right = number.Length - 1;
            while (left < right)
            {
                if (number[left++] != number[right--])
                    return false;
            }
            return true;
        }

        #region GCD and LCM
        [DllImport("EulerNative.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "lcm")]
        public static extern ulong LCM(ulong u, ulong v);
        [DllImport("EulerNative.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "lcm_multi")]
        private static extern ulong LCMPrivate(
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U8, SizeParamIndex = 1)]
            ulong[] u,
            int size);
        [DllImport("EulerNative.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "gcd")]
        public static extern ulong GCD(ulong u, ulong v);
        [DllImport("EulerNative.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "gcd_multi")]
        private static extern ulong GCDPrivate(
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U8, SizeParamIndex = 1)]
            ulong[] u,
            int size);

        public static ulong LCM(params ulong[] u) => LCMPrivate(u, u.Length);
        public static ulong LCM(this IEnumerable<ulong> series)
        {
            var arr = series.ToArray();
            return LCM(arr);
        }

        public static ulong GCD(params ulong[] u) => GCDPrivate(u, u.Length);
        public static ulong GCD(this IEnumerable<ulong> series)
        {
            var arr = series.ToArray();
            return GCD(arr);
        }

        #region GCD BigInteger
        public static BigInteger GCD(params BigInteger[] series)
        {
            if (series.Any(s => s > ulong.MaxValue))
                return GCD_bigint(series);
            else
                return (BigInteger)GCD(ToUlongArray(series));
        }

        public static BigInteger GCD(this IEnumerable<BigInteger> series) => GCD(series.ToArray());

        static BigInteger GCD_bigint(BigInteger u, BigInteger v)
        {
            // https://en.wikipedia.org/wiki/Binary_GCD_algorithm

            int shift;

            /* GCD(0,v) == v; GCD(u,0) == u, GCD(0,0) == 0 */
            if (u == 0) return v;
            if (v == 0) return u;

            /* Let shift := lg K, where K is the greatest power of 2
            dividing both u and v. */
            for (shift = 0; IsEven(u | v); ++shift)
            {
                u >>= 1;
                v >>= 1;
            }

            while (IsEven(u))
                u >>= 1;

            /* From here on, u is always odd. */
            do
            {
                /* remove all factors of 2 in v -- they are not common */
                /*   note: v is not zero, so while will terminate */
                while (IsEven(v))  /* Loop X */
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

        static BigInteger GCD_bigint(BigInteger[] bigints)
        {
            var acc = bigints[0];
            for(var i = 1; i < bigints.Length; ++i)
                acc = GCD_bigint(acc, i);
            return acc;
        }
        #endregion GCD BigInteger

        #region LCM BigInteger
        public static BigInteger LCM(params BigInteger[] series)
        {
            if (series.Any(s => s > ulong.MaxValue))
                return LCM_bigint(series);
            else
                return (BigInteger)LCM(ToUlongArray(series));
        }

        public static BigInteger LCM(this IEnumerable<BigInteger> series) => LCM(series.ToArray());

        static BigInteger LCM_bigint(BigInteger u, BigInteger v) => u / GCD_bigint(u, v) * v;

        static BigInteger LCM_bigint(BigInteger[] bigints)
        {
            var acc = bigints[0];
            for (var i = 1; i < bigints.Length; ++i)
                acc = LCM_bigint(acc, bigints[i]);
            return acc;
        }
        #endregion LCM BigInteger

        #endregion GCD and LCM

        #region Root finding
        /// <summary>
        /// Estimate a root of a number using Newton-Raphson
        /// </summary>
        /// <param name="number">The number to find a root of</param>
        /// <param name="n">The order of the root</param>
        /// <param name="initialEstimate">The initial estimate</param>
        /// <param name="iterations">The number of iterations. Defaults to 10. A value of -1 will iterate 
        /// until the error reaches epsilon.</param>
        /// <returns>The estimated root</returns>
        public static double NthRoot(this double number, int n, double initialEstimate, int iterations = 5)
        {
            // https://en.wikipedia.org/wiki/Newton%27s_method

            var func = new Func<double, double>(x => Math.Pow(x, n) - number);
            var deriv = new Func<double, double>(x => n * Math.Pow(x, n - 1));
            var guess = (double)(n + 1);
            for(var i = 0; i < iterations; ++i)
                guess = guess - (func(guess) / deriv(guess));
            return guess;
        }

        /// <summary>
        /// Estimate a root of a number using Newton-Raphson
        /// </summary>
        /// <param name="number">The number to find a root of</param>
        /// <param name="n">The order of the root</param>
        /// <param name="initialEstimate">The initial estimate</param>
        /// <param name="error">The algorithm will iterate until the difference between two 
        /// successive estimates is equal to or less than <paramref name="error"/></param>
        /// <returns>The estimated root</returns>
        public static double NthRoot(this double number, int n, double initialEstimate, double error)
        {
            // https://en.wikipedia.org/wiki/Newton%27s_method

            var func = new Func<double, double>(x => Math.Pow(x, n) - number);
            var deriv = new Func<double, double>(x => n * Math.Pow(x, n - 1));
            var guess = (double)(n + 1);

            var thisError = 0.0;
            do
            {
                var newGuess = guess - (func(guess) / deriv(guess));
                thisError = Math.Abs(newGuess - guess);
                guess = newGuess;
            } while (thisError > error);

            return guess;
        }


        #endregion Root finding
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
