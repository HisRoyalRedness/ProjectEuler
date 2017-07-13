using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
    Euler Problem 90

    Keith Fletcher
    Jul 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    using Combo = Tuple<byte[], byte[]>;

    [Solution("")]
    public class Problem90 : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=90
        /// 
        /// Each of the six faces on a cube has a different digit(0 to 9) written 
        /// on it; the same is done to a second cube.By placing the two cubes 
        /// side-by-side in different positions we can form a variety of 2-digit 
        /// numbers.
        /// 
        /// For example, the square number 64 could be formed:
        /// 
        ///   [6] [4]
        ///
        /// In fact, by carefully choosing the digits on both cubes it is possible 
        /// to display all of the square numbers below one-hundred: 
        /// 01, 04, 09, 16, 25, 36, 49, 64, and 81.      
        /// 
        /// For example, one way this can be achieved is by placing 
        /// { 0, 5, 6, 7, 8, 9} on one cube and {1, 2, 3, 4, 8, 9} on the other cube.
        /// 
        /// However, for this problem we shall allow the 6 or 9 to be turned 
        /// upside-down so that an arrangement like { 0, 5, 6, 7, 8, 9} and 
        /// {1, 2, 3, 4, 6, 7} allows for all nine square numbers to be displayed; 
        /// otherwise it would be impossible to obtain 09. 
        /// 
        /// In determining a distinct arrangement we are interested in the digits 
        /// on each cube, not the order.
        /// 
        ///     {1, 2, 3, 4, 5, 6} is equivalent to {3, 6, 4, 1, 2, 5}
        ///     {1, 2, 3, 4, 5, 6} is distinct from {1, 2, 3, 4, 5, 9}
        ///     
        /// But because we are allowing 6 and 9 to be reversed, the two distinct sets 
        /// in the last example both represent the extended set {1, 2, 3, 4, 5, 6, 9} 
        /// for the purpose of forming 2-digit numbers.
        /// 
        /// How many distinct arrangements of the two cubes allow for all of the square 
        /// numbers to be displayed?
        /// 
        /// Answer: 
        /// </summary>

        protected override string InternalSolve()
        {
            // 01, 04, 09, 16, 25, 36, 49, 64, and 81
            // 01, 04, 06, 16, 25, 36, 46/64, and 81

            var cmb = Combinations.ToList();
            var validCmb = cmb.Where(c => c.IsValid()).ToList();
            var gg = cmb.Where(c => c.IsValid()).Where(c => c.N1.Equals(c.N2)).ToList();
            return validCmb.Count.ToString();
        }

        public static IEnumerable<ComboSet> Combinations
        {
            get
            {
                var cubes = Cubes.ToArray();
                var len = cubes.Length;
                for (var d1 = 0; d1 < len; ++d1)
                    for (var d2 = d1; d2 < len; ++d2)
                        yield return new ComboSet(cubes[d1], cubes[d2]);

            }
        }

        public static IEnumerable<NumberSet> Cubes
        {
            get
            {
                for (byte d1 = 0; d1 < 9; ++d1)
                    for (byte d2 = (byte)(d1 + 1); d2 < 9; ++d2)
                        for (byte d3 = (byte)(d2 + 1); d3 < 9; ++d3)
                            for (byte d4 = (byte)(d3 + 1); d4 < 9; ++d4)
                                for (byte d5 = (byte)(d4 + 1); d5 < 9; ++d5)
                                    for (byte d6 = (byte)(d5 + 1); d6 < 9; ++d6)
                                        yield return new NumberSet(new byte[] { d1, d2, d3, d4, d5, d6 });
            }
        }
    }

    [DebuggerDisplay("{_n1.ToDisplayString()}   {_n2.ToDisplayString()}")]
    public struct ComboSet
    {
        public ComboSet(NumberSet n1, NumberSet n2)
        {
            _n1 = n1;
            _n2 = n2;
        }

        public NumberSet N1 => _n1;
        public NumberSet N2 => _n2;

        readonly NumberSet _n1;
        readonly NumberSet _n2;
    }

    [DebuggerDisplay("{ToDisplayString()}")]
    public struct NumberSet : IEquatable<NumberSet>
    {
        public NumberSet(byte[] arr)
        {
            _array = arr;
        }

        public byte this[int index] => _array[index];
        public string ToDisplayString() => $"{_array[0]} {_array[1]} {_array[2]} {_array[3]} {_array[4]} {_array[5]}";

        public bool Equals(NumberSet other)
        {
            for (var i = 0; i < 6; ++i)
                if (this[i] != other[i])
                    return false;
            return true;
        }

        readonly byte[] _array;
    }

    internal static class Problem090Extensions
    {
        //var squareNumber = new[] { 1, 4, 9, 16, 25, 36, 49, 64, 81 };
        static Problem090Extensions()
        {
            _squares = new[]
            {
                new byte[]{ 0, 1 },
                new byte[]{ 0, 4 },
                new byte[]{ 0, 6 }, // 9
                new byte[]{ 1, 6 },
                new byte[]{ 2, 5 },
                new byte[]{ 3, 6 },
                new byte[]{ 4, 6 }, // 49
                //new byte[]{ 6, 4 },
                new byte[]{ 8, 1 },
            };
            _len = _squares.Length;
        }

        public static bool IsValid(this ComboSet combo)
        {
            var set = new HashSet<byte>();
            for (var i = 0; i < 6; ++i)
            {
                for (var j = 0; j < 6; ++j)
                {
                    for (var k = 0; k < _len; ++k)
                    {
                        if ((combo.N1[i] == _squares[k][0] && combo.N2[j] == _squares[k][1]) ||
                            (combo.N1[i] == _squares[k][1] && combo.N2[j] == _squares[k][0]))
                        {
                            set.Add((byte)k);
                            if (set.Count == _len)
                                return true;
                        }
                    }
                }
            }
            return false;
        }

        static byte[][] _squares;
        static int _len;
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
