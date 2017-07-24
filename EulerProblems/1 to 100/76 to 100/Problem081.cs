using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
    Euler Problem 81

    Keith Fletcher
    Jul 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    using Cell = Tuple<int, int>;

    [Solution("427337")]
    public class Problem81 : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=81
        /// 
        /// In the 5 by 5 matrix below, the minimal path sum from the 
        /// top left to the bottom right, by only moving to the right 
        /// and down, is indicated in bold red and is equal to 2427.
        /// 
        ///     ₁₃₁ 673 234 103  18
        ///     ₂₀₁  ₉₆ ₃₄₂ 965 150
        ///     630 803 ₇₄₆ ₄₂₂ 111
        ///     537 699 497 ₁₂₁ 956
        ///     805 732 524  ₃₇ ₃₃₁
        ///     
        /// Find the minimal path sum, in matrix.txt (right click and 
        /// "Save Link/Target As..."), a 31K text file containing a 
        /// 80 by 80 matrix, from the top left to the bottom right by 
        /// only moving right and down.
        /// 
        /// Answer: 427337
        /// </summary>

        protected override string InternalSolve()
        {
            var grid = File
                .ReadAllLines(@"Resources\p081_matrix.txt")
                .Select(l => l.Split(',').Select(c => ulong.Parse(c)).ToArray())
                .ToArray();

            var rowLimit = grid.Length - 1;
            var colLimit = grid[0].Length - 1;

            foreach (var diag in Diagonals(rowLimit, colLimit).Skip(1))
            {
                foreach(var cell in diag)
                {
                    var up = cell.Item1 - 1 >= 0 ? grid[cell.Item1 - 1][cell.Item2] : int.MaxValue;
                    var left = cell.Item2 - 1 >= 0 ? grid[cell.Item1][cell.Item2 - 1] : int.MaxValue;
                    grid[cell.Item1][cell.Item2] += up < left ? up : left;
                }
            }

            return grid[rowLimit][colLimit].ToString();
        }

        static IEnumerable<Cell[]> Diagonals(int rowLimit, int colLimit)
        {
            foreach(var start in Diagonal_start(rowLimit, colLimit))
            {
                var r = start.Item1;
                var c = start.Item2;

                var list = new List<Cell>();
                while (c >= 0 && r <= rowLimit)
                    list.Add(new Cell( r++, c--));
                yield return list.ToArray();
            }
        }

        static IEnumerable<Cell> Diagonal_start(int rowLimit, int colLimit)
        {
            var row = 0;
            var col = 0;

            while (row < rowLimit || col < colLimit)
            {
                yield return new Cell(row, col);
                if (col < colLimit)
                    ++col;
                else
                    ++row;
            }
            yield return new Cell(row, col);
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
