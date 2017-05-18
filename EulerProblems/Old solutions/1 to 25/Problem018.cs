using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Numerics;
using System.Reflection;
using System.IO;

namespace fletcher.org
{    
    [Export(typeof(IProblem))]
    [Solution("1074")]
    class Problem018Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=18
        /// 
        /// By starting at the top of the triangle below and moving to 
        /// adjacent numbers on the row below, the maximum total from 
        /// top to bottom is 23.
        /// 
        ///          3
        ///         7 4
        ///        2 4 6
        ///       8 5 9 3
        /// 
        /// That is, 3 + 7 + 4 + 9 = 23.
        /// 
        /// Find the maximum total from top to bottom of the triangle below:
        /// 
        ///                                75
        ///                              95  64
        ///                            17  47  82
        ///                          18  35  87  10
        ///                        20  04  82  47  65
        ///                      19  01  23  75  03  34
        ///                    88  02  77  73  07  63  67
        ///                  99  65  04  28  06  16  70  92
        ///                41  41  26  56  83  40  80  70  33
        ///              41  48  72  33  47  32  37  16  94  29
        ///            53  71  44  65  25  43  91  52  97  51  14
        ///          70  11  33  28  77  73  17  78  39  68  17  57
        ///        91  71  52  38  17  14  91  43  58  50  27  29  48
        ///      63  66  04  68  89  53  67  30  73  16  69  87  40  31
        ///    04  62  98  27  23  09  70  98  73  93  38  53  60  04  23
        ///    
        /// NOTE: As there are only 16384 routes, it is possible to solve this 
        /// problem by trying every route. However, Problem 67, is the same challenge 
        /// with a triangle containing one-hundred rows; it cannot be solved by brute 
        /// force, and requires a clever method! ;o)
        /// 
        /// Answer: 1074
        /// </summary>
        public Problem018Generator() { }

        static readonly int[][] triangle = new int[][] 
            { 
                new int[] { 75 },
                new int[] { 95, 64 },
                new int[] { 17, 47, 82 },
                new int[] { 18, 35, 87, 10 },
                new int[] { 20, 04, 82, 47, 65 },
                new int[] { 19, 01, 23, 75, 03, 34 },
                new int[] { 88, 02, 77, 73, 07, 63, 67 },
                new int[] { 99, 65, 04, 28, 06, 16, 70, 92 },
                new int[] { 41, 41, 26, 56, 83, 40, 80, 70, 33 },
                new int[] { 41, 48, 72, 33, 47, 32, 37, 16, 94, 29 },
                new int[] { 53, 71, 44, 65, 25, 43, 91, 52, 97, 51, 14 },
                new int[] { 70, 11, 33, 28, 77, 73, 17, 78, 39, 68, 17, 57 },
                new int[] { 91, 71, 52, 38, 17, 14, 91, 43, 58, 50, 27, 29, 48 },
                new int[] { 63, 66, 04, 68, 89, 53, 67, 30, 73, 16, 69, 87, 40, 31 },
                new int[] { 04, 62, 98, 27, 23, 09, 70, 98, 73, 93, 38, 53, 60, 04, 23 },
            };

        protected override string InternalCalculateSolution()
        { return triangle.Reduce(); }
    }

    public static class Problem018Extensions
    {
        public static string Reduce(this int[][] triangle)
        {
            var levels = triangle.GetUpperBound(0);

            for (int level = levels; level > 0; level--)
            {
                var width = triangle[level].Count();
                var line = new int[width - 1];

                for (int i = 0; i < width - 1; i++)
                {
                    var left = triangle[level - 1][i] + triangle[level][i];
                    var right = triangle[level - 1][i] + triangle[level][i + 1];

                    triangle[level - 1][i] = left > right ? left : right;
                }
            }
            return triangle[0][0].ToString();
        }

        public static void Print(this int[][] triangle)
        {
            var levels = triangle.GetUpperBound(0);
            for (int i = 0; i <= triangle.GetUpperBound(0); i++)
            {
                Logging.Write(new string(' ', (levels - i) * 2));
                for (int j = 0; j <= i; j++)
                    Logging.Write("{0:00}  ", triangle[i][j]);
                Logging.WriteLine();
            }
        }

        public static int[][] LoadTriangle(this string filename)
        {
            var lines = new List<int[]>();

            using (var sr = new StreamReader(@"..\Resources\triangle.txt"))
            {
                string line;
                while (true)
                {
                    line = sr.ReadLine();
                    if (line == null)
                        break;

                    lines.Add(line
                        .Split(new[] { ' ' }, StringSplitOptions.None)
                        .Select(n => int.Parse(n))
                        .ToArray());
                }
            }

            return lines.ToArray();
        }
    }
}
