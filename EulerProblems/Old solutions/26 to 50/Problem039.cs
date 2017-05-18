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
    [Solution("840")]
    class Problem039Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=39
        /// 
        /// If p is the perimeter of a right angle triangle with integral 
        /// length sides, {a,b,c}, there are exactly three solutions 
        /// for p = 120.
        /// 
        /// {20,48,52}, {24,45,51}, {30,40,50}
        /// 
        /// For which value of p ≤ 1000, is the number of solutions 
        /// maximised?
        /// 
        /// Answer: 840
        /// </summary>
        public Problem039Generator() { }

        protected override string InternalCalculateSolution()
        {
            var maxSols = 0;
            var maxP = 0;
            Enumerable.Range(1, 1000)
                .Select(p =>
                {
                    var sols = SolutionsForPerimeter((ulong)p).Count();
                    if (sols > maxSols)
                    {
                        maxSols = sols;
                        maxP = p;
                    }
                    return p;
                }).ToList();            
            return maxP.ToString();
        }

        IEnumerable<Tuple<ulong, ulong, ulong>> SolutionsForPerimeter(ulong perimeter)
        {
            for (ulong i = 1; i < perimeter / 2; i++)
            {
                var i2 = i*i;
                for (ulong j = i; j < perimeter / 2; j++)
                {
                    var r = perimeter - i - j;
                    if (i2 + j * j == r * r)
                        yield return new Tuple<ulong, ulong, ulong>(i, j, r);
                }
            }
        }
    }
}
