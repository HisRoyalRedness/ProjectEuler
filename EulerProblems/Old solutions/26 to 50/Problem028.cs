using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Numerics;
using System.Reflection;

namespace fletcher.org.Solutions
{
    [Export(typeof(IProblem))]
    [Solution("669171001")]
    class Problem028Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=28
        /// 
        /// Starting with the number 1 and moving to the right in 
        /// a clockwise direction a 5 by 5 spiral is formed as follows:
        /// 
        ///     21 22 23 24 25
        ///     20  7  8  9 10
        ///     19  6  1  2 11
        ///     18  5  4  3 12
        ///     17 16 15 14 13
        /// 
        /// It can be verified that the sum of the numbers on the 
        /// diagonals is 101. What is the sum of the numbers on the 
        /// diagonals in a 1001 by 1001 spiral formed in the same way?
        /// 
        /// Answer: 669171001
        /// </summary>
        public Problem028Generator() { }

        struct Pos
        {
            public int X;
            public int Y;
            public int Number;
        }

        enum Direction
        {
            Right,
            Down,
            Left, 
            Up
        }

        protected override string InternalCalculateSolution()
        {
            const int size = 1001 * 1001;
            var matrix = new List<Pos>(size);

            var number = 1;
            var step = 1;
            var direction = Direction.Right;
            var x = 0;
            var y = 0;

            while (true)
            {
                for (int i = 0; i < step; i++)
                {
                    matrix.Add(new Pos() { X = x, Y = y, Number = number++ });
                    switch (direction)
                    {
                        case Direction.Right: x++; break;
                        case Direction.Down: y++; break;
                        case Direction.Left: x--; break;
                        case Direction.Up: y--; break;
                    }   
                }

                if (number >= size)
                    break;

                switch (direction)
                {
                    case Direction.Right: direction = Direction.Down; break;
                    case Direction.Down: direction = Direction.Left; step++; break;
                    case Direction.Left: direction = Direction.Up; break;
                    case Direction.Up: direction = Direction.Right; step++; break;
                }
            }

            var answer = matrix.Where(p => p.Y == p.X).Select(p => p.Number)
                .Union(matrix.Where(p => p.Y == p.X * -1).Select(p => p.Number));

            return answer.Sum().ToString();
        }
    }
}
