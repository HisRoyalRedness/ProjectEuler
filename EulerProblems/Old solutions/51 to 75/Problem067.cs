using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Numerics;
using System.Reflection;
using System.IO;

namespace fletcher.org.Solutions
{
    [Export(typeof(IProblem))]
    [Solution("7273")]
    class Problem067Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=67
        /// 
        /// By starting at the top of the triangle below and moving to 
        /// adjacent numbers on the row below, the maximum total from 
        /// top to bottom is 23.
        /// 
        ///    3
        ///   7 4
        ///  2 4 6
        /// 8 5 9 3
        /// 
        /// That is, 3 + 7 + 4 + 9 = 23.
        /// 
        /// Find the maximum total from top to bottom in triangle.txt,
        /// a 15K text file containing a triangle with one-hundred rows.
        /// 
        /// NOTE: This is a much more difficult version of Problem 18. 
        /// It is not possible to try every route to solve this problem, 
        /// as there are 299 altogether! If you could check one trillion 
        /// (10^12) routes every second it would take over twenty billion 
        /// years to check them all. There is an efficient algorithm 
        /// to solve it. ;o)
        /// 
        /// Answer: 7273
        /// </summary>
        public Problem067Generator() { }        

        protected override string InternalCalculateSolution()
        { return @"..\Resources\triangle.txt".LoadTriangle().Reduce(); }        
    }
}
