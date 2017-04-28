using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HisRoyalRedness.com
{
    [DebuggerDisplay("{DebugString}")]
    public class ProblemDisplayModel : NotifyBase, IProblemDisplay
    {
        public ProblemDisplayModel()
        { }

        public ProblemDisplayModel(IProblem problem)
        {
            ProblemNumber = problem.ProblemNumber;
            Solution = problem.Solution;
            _loadedFromMef = true;
        }

        public string CalculatedSolution
        {
            get { return _calculatedSolution ?? string.Empty; }
            set { SetProperty(ref _calculatedSolution, value); }
        }
        string _calculatedSolution = null;

        public DateTime? LastSolved
        {
            get { return _lastSolved; }
            set { SetProperty(ref _lastSolved, value); }
        }
        DateTime? _lastSolved = null;

        [XmlIgnoreAttribute]
        public TimeSpan? LastSolveTime
        {
            get { return _lastSolveTime; }
            set { SetProperty(ref _lastSolveTime, value); }
        }
        TimeSpan? _lastSolveTime = null;

        // Pretend property for serialization
        [XmlElement("LastSolveTime")]
        public long LastSolveTimeTicks
        {
            get { return _lastSolveTime.HasValue ? _lastSolveTime.Value.Ticks : 0; }
            set { _lastSolveTime = value == 0 ? (TimeSpan?)null : new TimeSpan(value); }
        }

        public int ProblemNumber
        {
            get { return _problemNumber; }
            set { SetProperty(ref _problemNumber, value); }
        }
        int _problemNumber;

        public string Solution
        {
            get { return _solution; }
            set { SetProperty(ref _solution, value); }
        }
        string _solution = null;

        [XmlIgnoreAttribute]
        public bool LoadedFromMEF
        {
            get { return _loadedFromMef; }
            set { SetProperty(ref _loadedFromMef, value); }
        }
        bool _loadedFromMef = false;

        public void SetSolution(ISolutionResult result)
        {
            CalculatedSolution = result.Solution;
            LastSolveTime = result.SolveTime;
            LastSolved = DateTime.Now;
        }

        public int CompareTo(IProblem other)
            => ProblemComparer.Default.Compare(this, other);

        string DebugString => $"No: {ProblemNumber:#}, Sol: {Solution}, Calc: {CalculatedSolution}";
    }

    public enum SortField
    {
        ProblemNumber,
        EmbeddedSolution,
        CalculatedSolution,
        SolveTime,
        LastSolved
    }

    public class ProblemDisplayModelSort : NotifyBase, IComparer, IComparer<ProblemDisplayModel>
    {
        public int Compare(object x, object y)
            => Compare(x as ProblemDisplayModel, y as ProblemDisplayModel);

        public int Compare(ProblemDisplayModel x, ProblemDisplayModel y)
        {
            int sort = 0;
            switch (SortField)
            {
                case SortField.EmbeddedSolution:
                    sort = (x?.Solution ?? "").CompareTo(y?.Solution ?? "");
                    break;
                case SortField.CalculatedSolution:
                    sort = (x?.CalculatedSolution ?? "").CompareTo(y?.CalculatedSolution ?? "");
                    break;
                case SortField.SolveTime:
                    sort = (x?.LastSolveTime ?? TimeSpan.Zero).CompareTo(y?.LastSolveTime ?? TimeSpan.Zero);
                    break;
                case SortField.LastSolved:
                    sort = (x?.LastSolved ?? DateTime.MinValue).CompareTo(y?.LastSolved ?? DateTime.MinValue);
                    break;
                default:
                    sort = (x?.ProblemNumber ?? -1).CompareTo(y?.ProblemNumber ?? -1);
                    break;
            }
            return Direction == ListSortDirection.Ascending ? sort : sort * -1;
        }

        public SortField SortField
        {
            get { return _sortField; }
            set { SetProperty(ref _sortField, value); }
        }
        SortField _sortField = SortField.ProblemNumber;

        public ListSortDirection Direction
        {
            get { return _direction; }
            set { SetProperty(ref _direction, value); }
        }
        ListSortDirection _direction = ListSortDirection.Ascending;
    }
}
