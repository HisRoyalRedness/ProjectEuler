using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

/*
    Common interfaces used for composition and data transfer

    Keith Fletcher
    Mar 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    /// <summary>
    /// A base problem description
    /// </summary>
    public interface IProblem : IComparable<IProblem>
    {
        int ProblemNumber { get; }
        string Title { get; }
        string Solution { get; }
        string Analysis { get; }
        string Summary { get; }
    }

    /// <summary>
    /// Identifies classes that implement a solution to a problem
    /// </summary>    
    public interface IProblemSolver
    {
        string Solve();
    }

    /// <summary>
    /// A problem description and solution
    /// </summary>
    public interface ISolvableProblem : IProblem, IProblemSolver
    {
    }

    /// <summary>
    /// Used to identify problem descriptions for MEF
    /// </summary>
    [InheritedExport(typeof(IExportedProblem))]
    public interface IExportedProblem : ISolvableProblem
    {
    }

    public interface ISolutionResult
    {
        string Solution { get; }
        TimeSpan SolveTime { get; }
    }

    /// <summary>
    /// Provides control over the problem service
    /// </summary>
    [ServiceContract]
    public interface IProblemService : IDisposable
    {
        [OperationContract]
        void Ping();

        [OperationContract]
        Task<List<ProblemSummary>> GetProblemsAsync();

        [OperationContract]
        Task<SolutionResult> SolveProblem(int problemNumber);

        [OperationContract]
        Task ShutDownAsync();

        //[OperationContract]
        //void ShutDown();
    }


    public class ProblemComparer : IEqualityComparer<IProblem>, IComparer<IProblem>, IComparer
    {
        public int Compare(IProblem x, IProblem y)
            => (x?.ProblemNumber ?? -1).CompareTo(y?.ProblemNumber ?? -1);

        public int Compare(object x, object y)
            => Compare(x as IProblem, y as IProblem);

        public bool Equals(IProblem x, IProblem y)
            => x.ProblemNumber == y.ProblemNumber && string.Compare(x.Solution, y.Solution, true) == 0;

        public int GetHashCode(IProblem obj)
            => obj.GetHashCode();


        public static ProblemComparer Default { get; } = new ProblemComparer(); 
    }

}

/*
This is free and unencumbered software released into the public domain.

Anyone is free to copy, modify, publish, use, compile, sell, or
distribute this software, either in source code form or as a compiled
binary, for any purpose, commercial or non-commercial, and by any
means.

In jurisdictions that recognize copyright laws, the author or authors
of this software dedicate any and all copyright interest in the
software to the public domain. We make this dedication for the benefit
of the public at large and to the detriment of our heirs and
successors. We intend this dedication to be an overt act of
relinquishment in perpetuity of all present and future rights to this
software under copyright law.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.

For more information, please refer to <http://unlicense.org>
*/
