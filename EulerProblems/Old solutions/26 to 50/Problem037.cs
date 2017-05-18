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
    [Solution("748317")]
    class Problem037Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=37
        /// 
        /// The number 3797 has an interesting property. Being prime itself, 
        /// it is possible to continuously remove digits from left to right, 
        /// and remain prime at each stage: 3797, 797, 97, and 7. Similarly 
        /// we can work from right to left: 3797, 379, 37, and 3.
        /// 
        /// Find the sum of the only eleven primes that are both truncatable 
        /// from left to right and right to left.
        /// 
        /// NOTE: 2, 3, 5, and 7 are not considered to be truncatable primes.
        /// 
        /// Answer: 748317
        /// </summary>
        public Problem037Generator() { }

        protected override string InternalCalculateSolution()
        {
            var hash = new HashSet<ulong>(new ulong[] { 2, 3, 5, 7 });
            return Prime.FromFile()
                .Skip(4)
                .Where(prime => IsTruncatable(prime, hash))
                .Take(11)
                .Sum().ToString();            
        }

        bool IsTruncatable(ulong prime, HashSet<ulong> hash)
        {
            hash.Add(prime);
            var p = prime.ToString();
            for (int i = 1; i < p.Length; i++)
            {
                var s1 = ulong.Parse(p.Substring(0, i));
                var s2 = ulong.Parse(p.Substring(i));
                if (!hash.Contains(s1) || !hash.Contains(s2))
                    return false;
            }
            return true;
        }
        
    }
}
