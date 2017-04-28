using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HisRoyalRedness.com
{
    class Program
    {
        static void Main(string[] args)
        {
            //Hoster.Host();

            const int problemNumber = 1;

            var loader = new ProblemLoader();
            var problem = loader.Problems.ContainsKey(problemNumber)
                ? loader.Problems[problemNumber]
                : null;

            problem?.Solve();
        }
    }
}
