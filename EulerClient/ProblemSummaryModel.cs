using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HisRoyalRedness.com
{
    public class ProblemResult : NotifyBase, IProblem
    {
        public int ProblemNumber
        {
            get { return _problemNumber; }
            set { SetProperty(ref _problemNumber, value); }
        }
        int _problemNumber;

        public string Solution
        {
            get { return _embeddedSolution; }
            set { SetProperty(ref _embeddedSolution, value); }
        }
        string _embeddedSolution;

        public string CalculatedSolution
        {
            get { return _calculatedSolution; }
            set { SetProperty(ref _calculatedSolution, value); }
        }
        string _calculatedSolution;


        public DateTime? LastCalculated
        {
            get { return _lastCalculated; }
            set { SetProperty(ref _lastCalculated, value); }
        }
        DateTime? _lastCalculated = null;

        public int CompareTo(IProblem other)
        {
            throw new NotImplementedException();
        }
    }
}
