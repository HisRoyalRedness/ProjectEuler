﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
    Common sequence generators

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    #region Fibonacci
    public static class Fibonacci
    {
        static readonly double sqr5 = Math.Sqrt(5);
        static readonly double phi = (1 + sqr5) / 2;

        public static IEnumerable<ulong> Sequence
        {
            get
            {
                checked
                {
                    ulong n1 = 0;
                    ulong n2 = 1;
                    ulong n3 = 1;
                    yield return 0;
                    yield return 1;

                    while (n3 < ulong.MaxValue)
                    {
                        n3 = n1 + n2;
                        n1 = n2;
                        n2 = n3;
                        yield return n3;
                    }
                }
            }
        }

        public static IEnumerable<BigInteger> BigSequence
        {
            get
            {
                checked
                {
                    BigInteger n1 = 0;
                    BigInteger n2 = 1;
                    BigInteger n3 = 1;
                    yield return 0;
                    yield return 1;

                    while (true)
                    {
                        n3 = n1 + n2;
                        n1 = n2;
                        n2 = n3;
                        yield return n3;
                    }
                }
            }
        }

        public static ulong AtIndex(ulong index)
        {
            // http://en.wikipedia.org/wiki/Fibonacci_number#Relation_to_the_golden_ratio
            if (index < 1)
                throw new ArgumentOutOfRangeException(nameof(index), $"{nameof(index)} must be 1 or greater");
            checked
            {
                return (ulong)((Math.Pow(phi, (double)index) / sqr5) + .5D);
            }
        }
    }
    #endregion Fibonacci

    #region Figurate numbers

    // Triangle, square, pentagonal, hexagonal, heptagonal, and octagonal numbers 
    // are all figurate(polygonal) numbers and are generated by the following formulae:
    // 
    //     Triangle    P3,     n = n(n + 1) / 2   	0, 1, 3, 6, 10, 15, ... 
    //     Square      P4,     n = n2 	  	        0, 1, 4, 9, 16, 25, ... 
    //     Pentagonal  P5,     n = n(3n−1) / 2 	  	0, 1, 5, 12, 22, 35, ...
    //     Hexagonal   P6,     n = n(2n−1) 	  	    0, 1, 6, 15, 28, 45, ...
    //     Heptagonal  P7,     n = n(5n−3) / 2 	  	0, 1, 7, 18, 34, 55, ...
    //     Octagonal   P8,     n = n(3n−2) 	  	    0, 1, 8, 21, 40, 65, ...

    #region Triangle numbers
    /// <summary>
    /// A triangular number or triangle number counts the objects that can form an equilateral triangle.
    /// <see href="https://en.wikipedia.org/wiki/Triangular_number">Wikipedia</see>.
    /// </summary>
    public static class TriangleNumber
    {
        /// <summary>
        /// Get the triangle number that appears at the given index
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <returns>The triangle number at that index</returns>
        public static ulong AtIndex(int startIndex = 0)
        {
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            return (ulong)startIndex * ((ulong)startIndex + 1) / 2;
        }

        /// <summary>
        /// Generate a sequence of triangular numbers, of type <see cref="ulong"/>,
        /// starting at <paramref name="startIndex"/>.
        /// 0, 1, 3, 6, 10, 15, ... 
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <returns>A sequence of <see cref="ulong"/> triangle numbers</returns>
        public static IEnumerable<ulong> Sequence(int startIndex = 0)
        {
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            var index = (ulong)startIndex;
            var sum = AtIndex(startIndex);
            while (true)
            {
                yield return sum;
                sum += ++index;
            }
        }
    }
    #endregion Triangle numbers

    #region Square numbers
    /// <summary>
    /// A square number or perfect square is an integer that is the square of an integer.
    /// <see href="https://en.wikipedia.org/wiki/Square_number">Wikipedia</see>.
    /// </summary>
    public static class SquareNumber
    {
        /// <summary>
        /// Get the square number that appears at the given index
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <returns>The square number at that index</returns>
        public static ulong AtIndex(int startIndex = 0)
        {
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            return (ulong)startIndex * (ulong)startIndex;
        }

        /// <summary>
        /// Generate a sequence of square numbers, of type <see cref="ulong"/>,
        /// starting at <paramref name="startIndex"/>.
        /// 0, 1, 4, 9, 16, 25, ...
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <returns>A sequence of <see cref="ulong"/> square numbers</returns>
        public static IEnumerable<ulong> Sequence(int startIndex = 0)
        {
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            var index = (ulong)startIndex;
            var sum = AtIndex(startIndex);
            while (true)
            {
                yield return sum;
                sum += (++index * 2) - 1;
            }
        }
    }
    #endregion Square numbers
    #endregion Figurate numbers

    #region Fraction
    [DebuggerDisplay("{_numerator}/{_denominator}")]
    public struct Fraction<T>
    {
        // http://www.vcskicks.com/code-snippet/fraction.php

        public Fraction(T num, T denom)
        {
            _numerator = num;
            _denominator = denom;
        }

        public T Numerator => _numerator;
        public T Denominator => _denominator;

        readonly T _numerator;
        readonly T _denominator;
    }
    #endregion Fraction

    #region Continued fractions
    public static partial class ContinuedFraction
    {
        // Expression as an infinite continued fraction
        // https://en.wikipedia.org/wiki/Square_root_of_2#Continued_fraction_representation

        // Coefficient notation as [1, 2, 2, 2, ... ]
        //https://en.wikipedia.org/wiki/Continued_fraction#Notations_for_continued_fractions

        // Algorithm
        // https://en.wikipedia.org/wiki/Continued_fraction#Infinite_continued_fractions_and_convergents
    }
    #endregion Continued fractions
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
