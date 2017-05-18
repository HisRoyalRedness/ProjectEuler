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
    [Solution("127035954683")]
    class Problem062Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=62
        /// 
        /// The cube, 41063625 (345^3), can be permuted to produce two 
        /// other cubes: 56623104 (384^3) and 66430125 (405^3). In fact, 
        /// 41063625 is the smallest cube which has exactly three 
        /// permutations of its digits which are also cube.
        /// 
        /// Find the smallest cube for which exactly five 
        /// permutations of its digits are cube
        /// 
        /// Answer: 127035954683
        /// </summary>
        public Problem062Generator() { }

        protected override string InternalCalculateSolution()
        {
            uint num = 1;
            var lookup = new Dictionary<string, List<uint>>();
            while (++num < uint.MaxValue - 1)
            {
                var cube = BigInteger.Pow((BigInteger)num, 3);
                var orderedCube = new string(cube.ToString().OrderBy(c => c).ToArray());
                if (lookup.ContainsKey(orderedCube))
                {
                    var list = lookup[orderedCube];
                    list.Add(num);
                    if (list.Count >= 5)
                        return BigInteger.Pow((BigInteger)(list.Min()), 3).ToString();
                }
                else
                    lookup.Add(orderedCube, new List<uint>(new[] { num }));
            }

            return "";
        }
    }
}
