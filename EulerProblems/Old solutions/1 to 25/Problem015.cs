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
    [Solution("137846528820")]
    class Problem015Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=15
        /// 
        /// Starting in the top left corner of a 2×2 grid, there are 6 routes 
        /// (without backtracking) to the bottom right corner.
        /// 
        /// How many routes are there through a 20×20 grid?
        /// 
        /// Answer: 137846528820
        /// </summary>
        public Problem015Generator() { }

        protected override string InternalCalculateSolution()
        {
            var gridSize = new BigInteger(20);
            return CalculatorExtensions.Combination(gridSize * 2, gridSize).ToString();            
        }
    }
}
