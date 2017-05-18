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
    [Solution("296962999629")]
    class Problem049Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=49
        /// 
        /// The arithmetic sequence, 1487, 4817, 8147, in which each of the
        /// terms increases by 3330, is unusual in two ways: (i) each of the
        /// three terms are prime, and, (ii) each of the 4-digit numbers are
        /// permutations of one another.
        /// 
        /// There are no arithmetic sequences made up of three 1-, 2-, or
        /// 3-digit primes, exhibiting this property, but there is one other
        /// 4-digit increasing sequence.
        /// 
        /// What 12-digit number do you form by concatenating the three
        /// terms in this sequence?
        /// 
        /// Answer: 296962999629
        /// </summary>
        public Problem049Generator() { }

        protected override string InternalCalculateSolution()
        {
            var dict = new Dictionary<ulong, List<ulong>>();

            // Sort the primes into bucket, with each bucket key
            // being the digits of the prime arranged from lowest
            // to highest. Ignore bucket 1478 (we already now this
            // one fits the pattern).
            foreach (var prime in Prime.FromFile()
                .TakeWhile(p => p < 10000)
                .Where(p => p > 1000))
            {
                var key = ReOrder(prime);
                if (key == 1478)
                    continue;

                if (dict.ContainsKey(key))
                    dict[key].Add(prime);
                else
                    dict.Add(key, new List<ulong>(new[] { prime }));
            }

            // Sort the buckets from the fullest to emptiest
            var sorted = dict
                .Select(kv => kv.Value)
                .Where(c => c.Count >= 3)
                .OrderByDescending(v => v.Count)
                .ToList();

            // step through each bucket
            foreach (var set in sorted)
                // Get all possible combinations of the elements in the bucket
                foreach (var index in GetIndexes(set.Count))
                    // Test the seperation between the elements
                    if (set[index.Item3] - set[index.Item2] == set[index.Item2] - set[index.Item1])
                        return string.Format("{0}{1}{2}", set[index.Item1], set[index.Item2], set[index.Item3]);

            return "";
        }

        static ulong ReOrder(ulong number)
        { return ulong.Parse(new string(number.ToString("0000").OrderBy(c => c).ToArray())); }

        static IEnumerable<Tuple<int, int, int>> GetIndexes(int limit)
        {
            for (int i1 = 0; i1 < limit; i1++)
                for (int i2 = i1 + 1; i2 < limit; i2++)
                    for (int i3 = i2 + 1; i3 < limit; i3++)
                        yield return new Tuple<int, int, int>(i1, i2, i3);
        }

    }
}
