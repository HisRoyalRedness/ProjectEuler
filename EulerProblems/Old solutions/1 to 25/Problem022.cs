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
    [Solution("871198282")]
    class Problem022Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=22
        /// 
        /// Using names.txt, a 46K text file containing over five-thousand 
        /// first names, begin by sorting it into alphabetical order. Then 
        /// working out the alphabetical value for each name, multiply this 
        /// value by its alphabetical position in the list to obtain a name 
        /// score.
        /// 
        /// For example, when the list is sorted into alphabetical order, 
        /// COLIN, which is worth 3 + 15 + 12 + 9 + 14 = 53, is the 938th 
        /// name in the list. So, COLIN would obtain a score of 
        /// 938 × 53 = 49714.
        /// 
        /// What is the total of all the name scores in the file?
        /// 
        /// Answer: 871198282
        /// </summary>
        public Problem022Generator() { }

        protected override string InternalCalculateSolution()
        {
            return File.ReadAllText(@"..\Resources\names.txt")
                .SplitFromCSV()
                .Select(name => name.Substring(1, name.Length - 2))
                .Select(name => new { Name = name, Worth = name.CalcWorth() })
                .OrderBy(comb => comb.Name)
                .Select((comb, index) => comb.Worth * (index + 1))
                .Sum().ToString();            
        }
    }

    static class Problem022Ext
    {
        public static int CalcWorth(this string name)
        {
            return name
                .ToUpper()
                .Select(c => (int)c - 0x40)
                .Sum();
        }
    }
}
