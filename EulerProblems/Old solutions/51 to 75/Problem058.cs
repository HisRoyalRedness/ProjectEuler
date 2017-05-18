using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Numerics;
using System.Reflection;

namespace fletcher.org
{
    [Export(typeof(IProblem))]
    [Solution("26241")]
    class Problem058Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=58
        /// 
        /// Starting with 1 and spiralling anticlockwise in the following way, 
        /// a square spiral with side length 7 is formed.
        /// 
        ///                 37 36 35 34 33 32 31
        ///                 38 17 16 15 14 13 30
        ///                 39 18  5  4  3 12 29
        ///                 40 19  6  1  2 11 28
        ///                 41 20  7  8  9 10 27
        ///                 42 21 22 23 24 25 26
        ///                 43 44 45 46 47 48 49
        /// 
        /// It is interesting to note that the odd squares lie along the bottom 
        /// right diagonal, but what is more interesting is that 8 out of the 13 
        /// numbers lying along both diagonals are prime; that is, a ratio 
        /// of 8/13 ≈ 62%.
        /// 
        /// If one complete new layer is wrapped around the spiral above, a 
        /// square spiral with side length 9 will be formed. If this process is 
        /// continued, what is the side length of the square spiral for which the 
        /// ratio of primes along both diagonals first falls below 10%?
        /// 
        /// Answer: 26241
        /// </summary>
        public Problem058Generator() { }

        static IEnumerator<ulong> primeEnum = Prime.FromFile().GetEnumerator();

        protected override string InternalCalculateSolution()
        {
            //var spiral = Spiral(15);
            //PrintSpiral(spiral);
            

            int level = 0;
            int numCount = 0;
            int primeCount = 0;
            int sideLength = 0;
            while(true)
            {
                ++level;
                var nums = Diagonals(level).OrderBy(n => n).ToList();
                numCount += nums.Count;
                primeCount += nums.Where(n => Prime.IsPrimeIncremental((ulong)n)).Count();
                //perc = (double)primeCount / (double)numCount * 100.0;
                sideLength = (level * 2 - 1);
                double perc = (double)primeCount / (double)numCount * 100.0;
                if (level > 2 && primeCount * 10 < numCount)
                    return sideLength.ToString();
            }

            return "";
        }

        IEnumerable<ulong> Diagonals(int level)
        {
            if (level == 1)
                yield return 1;
            else
            {
                ulong top = ((ulong)level - 1) * 2;
                ulong bottom = (ulong)level * 2 - 1;
                yield return top * top + 1;                 //(($AG7-1)*2)*(($AG7-1)*2)+1
                yield return top * top - top + 1;           //(($AG7-1)*2)*(($AG7-1)*2)-(($AG7-1)*2)+1
                yield return bottom * bottom - bottom + 1;  //($AG9*2-1)*($AG9*2-1)-$AG9*2+2
                yield return bottom * bottom;               //($AG9*2-1)*($AG9*2-1)
            }
        }

        void PrintSpiral(int[,] spiral)
        {
            for (int y = spiral.GetLength(0) - 1; y >= 0; y--)
            {
                for (int x = 0; x < spiral.GetLength(0); x++)
                {
                    Console.Write(spiral[x, y].ToString().PadLeft(4));
                }
                Console.WriteLine();
            }
        }

        int[,] Spiral(int size)
        {
            Verify.ArgIs(size, "size", s => s > 1, "size must be greater than 1");
            Verify.ArgIs(size, "size", s => s % 2 == 1, "size must be odd.");

            var matrix = new int[size, size];

            int i = 0;
            int maxE = size / 2;
            int minW = size / 2;
            int maxN = size / 2;
            int minS = size / 2;
            int x = maxE;
            int y = maxE;
            char direction = 'e';
            int limit = size * size;

            while (++i <= limit)
            {
                matrix[x, y] = i;
                switch (direction)
                {
                    case 'e':
                        if (++x > maxE)
                        {
                            direction = 'n';
                            ++maxE;
                        }
                        break;
                    case 'n':
                        if (++y > maxN)
                        {
                            direction = 'w';
                            ++maxN;
                        }
                        break;
                    case 'w':
                        if (--x < minW)
                        {
                            direction = 's';
                            --minW;
                        }
                        break;
                    case 's':
                        if (--y < minS)
                        {
                            direction = 'e';
                            --minS;
                        }
                        break;
                }
            }
            return matrix;
        }
    }
}
