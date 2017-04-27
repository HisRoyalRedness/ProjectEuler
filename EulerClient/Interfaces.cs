using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HisRoyalRedness.com
{
    public interface IProblemDisplay : IProblem
    {
        string CalculatedSolution { get; }
        DateTime? LastSolved { get; }
        TimeSpan? LastSolveTime { get; }
    }
}
