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
    [Solution("6531031914842725")]
    class Problem068Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=68
        ///
        /// Consider the following "magic" 3-gon ring, filled with the
        /// numbers 1 to 6, and each line adding to nine.
        ///         _
        ///        |4|
        ///           \ _
        ///            |3|
        ///         _ /   \ _       _
        ///        |1|-----|2|-----|5|
        ///      _ /
        ///     |6|
        ///
        /// Working clockwise, and starting from the group of three
        /// with the numerically lowest external node (4,3,2 in this
        /// example), each solution can be described uniquely. For
        /// example, the above solution can be described by the set:
        /// 4,3,2; 6,2,1; 5,1,3.
        ///
        /// It is possible to complete the ring with four different
        /// totals: 9, 10, 11, and 12. There are eight solutions in
        /// total.
        ///
        /// Total	Solution Set
        /// 9	4,2,3; 5,3,1; 6,1,2
        /// 9	4,3,2; 6,2,1; 5,1,3
        /// 10	2,3,5; 4,5,1; 6,1,3
        /// 10	2,5,3; 6,3,1; 4,1,5
        /// 11	1,4,6; 3,6,2; 5,2,4
        /// 11	1,6,4; 5,4,2; 3,2,6
        /// 12	1,5,6; 2,6,4; 3,4,5
        /// 12	1,6,5; 3,5,4; 2,4,6
        ///
        /// By concatenating each group it is possible to form 9-digit
        /// strings; the maximum string for a 3-gon ring is 432621513.
        ///
        /// Using the numbers 1 to 10, and depending on arrangements,
        /// it is possible to form 16- and 17-digit strings. What is
        /// the maximum 16-digit string for a "magic" 5-gon ring?
        ///
        /// Answer: 6531031914842725
        /// </summary>
        public Problem068Generator() { }

        protected override string InternalCalculateSolution()
        {
            // Get solution
            return GetValidCombos10()
                .Where(s => s.FlatStringLength() == 16)
                .Select(s => s.Arrange())
                .Select(s => s.ToFlatString())
                .Distinct()
                .Max();
        }

        public static IEnumerable<IEnumerable<IEnumerable<int>>> GetValidCombos10()
        {

            var elements = Enumerable.Range(1, 10).ToList();

            Action<List<int>, List<List<int>>, Action<List<List<int>>>> loop = (els, setsIn, action) =>
            {
                var exceptList = setsIn.SelectMany(s => new[] { s[0], s[1] }).ToList();
                var combo = Combinations.UniqueCombinations(els.Except(exceptList), 3);
                foreach (List<int> set in combo)
                {
                    if (setsIn.Count > 0)
                    {
                        var prevSet = setsIn.Last();
                        if (prevSet[2] != set[1] || prevSet.Sum() != set.Sum())
                            continue;

                    }
                    action(setsIn.Union(new List<List<int>>(new[] { set })).ToList());
                }
            };

            var answers = new List<List<List<int>>>();

            loop(elements, new List<List<int>>(), set1 =>
            {
                loop(elements, set1, set2 =>
                {
                    loop(elements, set2, set3 =>
                    {
                        loop(elements, set3, set4 =>
                        {
                            var set5 = new[] 
                                {
                                    elements.Except(set4.SelectMany(s => s)).First(),
                                    set4[3][2], set4[0][1]
                                }.ToList();

                            if (set4.Last().Sum() == set5.Sum())
                                answers.Add(set4.Union(new[] { set5 }.ToList()).ToList());
                        });
                    });
                });
            });

            return answers;
        }
    }

    internal static class Prob68Ext
    {

        public static string ToSetString(this IEnumerable<IEnumerable<int>> stuff)
        { return string.Join("; ", stuff.Select(i1 => string.Join(",", i1.Select(i2 => i2.ToString())))); }

        public static string ToFlatString(this IEnumerable<IEnumerable<int>> stuff)
        { return string.Join("", stuff.Select(i1 => string.Join("", i1.Select(i2 => i2.ToString())))); }

        public static int FlatStringLength(this IEnumerable<IEnumerable<int>> stuff)
        { return stuff.ToFlatString().Length; }

        public static IEnumerable<IEnumerable<int>> Arrange(this IEnumerable<IEnumerable<int>> items)
        {
            // Determine which set has the lowest digit
            var minFirst = items.Min(i => i.First());

            // Rearrange the list so that the sets appear in order,
            // starting with the smallest
            var queue = new Queue<IEnumerable<int>>(items);
            while (queue.Peek().First() != minFirst)
                queue.Enqueue(queue.Dequeue());

            return queue;
        }
    }
}
