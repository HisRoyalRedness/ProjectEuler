using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Threading;
using System.Xml.Serialization;

namespace HisRoyalRedness.com
{
    public class EulerDataContext : NotifyBase, IDisposable
    {
        public EulerDataContext()
        {
            _refreshProblems = new RelayCommand(RefreshProblems);
            _solve = new RelayCommand(Solve);
            _itemDoubleClick = new RelayCommand<ProblemDisplayModel>(ItemDoubleClick);

            LoadProblems();
            _refreshProblems.Execute(null);


            _sortedProblems = new ListCollectionView(Problems);
            _sortedProblems.LiveFilteringProperties.Add(nameof(ProblemDisplayModel.LoadedFromMEF));
            _sortedProblems.SortDescriptions.Add(new SortDescription(nameof(IProblem.ProblemNumber), ListSortDirection.Descending));
            _sortedProblems.Filter = o => (o as ProblemDisplayModel).LoadedFromMEF;
            _sortedProblems.CustomSort = _customSort;
        }


        public ObservableCollection<ProblemDisplayModel> Problems { get; private set; } 
            = new ObservableCollection<ProblemDisplayModel>();

        public ICollectionView SortedProblems => _sortedProblems;
        ListCollectionView _sortedProblems = null;

        public RelayCommand RefreshCommand => _refreshProblems;
        RelayCommand _refreshProblems;

        public RelayCommand SolveCommand => _solve;
        RelayCommand _solve;

        public RelayCommand<ProblemDisplayModel> ItemDoubleClickCommand => _itemDoubleClick;
        RelayCommand<ProblemDisplayModel> _itemDoubleClick;


        public IProblem CurrentProblem
        {
            get { return _currentProblem; }
            set { SetProperty(ref _currentProblem, value); }
        }
        IProblem _currentProblem = null;

        public bool KeepService
        {
            get { return _keepService; }
            set { SetProperty(ref _keepService, value); }
        }
        bool _keepService = false;

        void RefreshProblems()
        {
            var dispatcher = Dispatcher.CurrentDispatcher;
            Task.Run(async () =>
            {
                try
                {
                    var service = StartService();
                    if (service != null)
                    {
                        var mefProblems = await service.GetProblemsAsync();
                        if (!KeepService)
                            await service?.ShutDownAsync();

                        if (mefProblems != null)
                            await dispatcher.InvokeAsync(() =>
                            {
                                var currentProblemNumber = CurrentProblem?.ProblemNumber ?? -1;

                                // Update solutions loaded from MEF
                                foreach (var problem in mefProblems)
                                {
                                    var oldProblem = Problems.Where(p => p.ProblemNumber == problem.ProblemNumber).FirstOrDefault();
                                    if (oldProblem == null)
                                        Problems.Add(new ProblemDisplayModel(problem));
                                    else
                                        oldProblem.Solution = problem.Solution;
                                }

                                // Reset the LoadedFromMEF flag
                                foreach (var problem in Problems)
                                    problem.LoadedFromMEF = mefProblems.Any(p => p.ProblemNumber == problem.ProblemNumber);

                                SortedProblems.Refresh();

                                if (currentProblemNumber != -1)
                                    CurrentProblem = Problems.FirstOrDefault(p => p.ProblemNumber == currentProblemNumber);

                                SaveProblems();
                            });
                    }
                }
                catch(Exception ex)
                {
                    _logger.Info(ex, "Error refreshing problems");
                }
            });
        }

        void SaveProblems()
        {
            var serialProblems = Problems.ToArray();
            Task.Run(() =>
            {
                try
                {
                    var serializer = new XmlSerializer(serialProblems.GetType());
                    using (var writer = new StreamWriter(Constants.ProblemSummaryFile))
                        serializer.Serialize(writer, serialProblems);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Failed to save the problems to disk.");
                }
            });
        }

        void LoadProblems()
        {
            var serialProblems = new ProblemDisplayModel[] { };
            try
            {
                var serializer = new XmlSerializer(serialProblems.GetType());
                using (var reader = new StreamReader(Constants.ProblemSummaryFile))
                {
                    serialProblems = serializer.Deserialize(reader) as ProblemDisplayModel[];
                    foreach (var problem in serialProblems)
                    {
                        problem.LoadedFromMEF = false;
                        Problems.Add(problem);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to save the problems to disk.");
            }
        }

        void Solve() => Solve(CurrentProblem?.ProblemNumber ?? -1);

        void Solve(int problemNumber)
        {
            if (problemNumber == -1)
                return;

            var dispatcher = Dispatcher.CurrentDispatcher;
            Task.Run(async () =>
            {
                try
                {
                    var service = StartService();
                    if (service != null)
                    {
                        var solution = await service.SolveProblem(problemNumber);
                        if (!KeepService)
                            await service.ShutDownAsync();
                        _logger.Info($"Solution to problem {problemNumber} is {solution.Solution}. Solved in {solution.SolveTime.TotalMilliseconds} ms.");
                        await dispatcher.InvokeAsync(() =>
                        {
                            Problems
                                .Where(p => p.ProblemNumber == problemNumber)
                                .FirstOrDefault()
                                ?.SetSolution(solution);
                            SaveProblems();
                        });                        
                    }
                }
                catch (Exception ex)
                {
                    _logger.Info(ex, $"Error solving problem {problemNumber}.");
                }
            });
        }

        void ItemDoubleClick(ProblemDisplayModel model) => Solve(model?.ProblemNumber ?? -1);

        public void Sort(string header, ListSortDirection direction)
        {
            switch(header as string)
            {
                case Constants.ProblemNumberHeader:         _customSort.SortField = SortField.ProblemNumber; break;
                case Constants.EmbeddedSolutionHeader:      _customSort.SortField = SortField.EmbeddedSolution; break;
                case Constants.CalculatedSolutionHeader:    _customSort.SortField = SortField.CalculatedSolution; break;
                case Constants.SolveTimeHeader:             _customSort.SortField = SortField.SolveTime; break;
                case Constants.LastSolvedHeader:            _customSort.SortField = SortField.LastSolved; break;
            }

            _customSort.Direction = direction;
            SortedProblems.Refresh();
        }

        IProblemService StartService()
        {
            var retries = 0;
            const int startProcessRetryThreshold = 3;
            const int maxRetries = 10;

            var success = false;
            IProblemService service = KeepService ? _service : null;

            while (!success)
            {
                try
                {
                    if (service == null)
                    {
                        ChannelFactory<IProblemService> pipeFactory =
                            new ChannelFactory<IProblemService>(
                                new NetNamedPipeBinding(),
                                new EndpointAddress(
                                    string.Join("/", Constants.NamedPipeEndpoint, Constants.NamedPipeEndpointAddress)));

                        service = pipeFactory.CreateChannel();
                    }
                    service.Ping();
                    success = true;
                }
                catch (Exception ex)
                {
                    service = null;
                    _logger.Trace(ex);
                    if (++retries > maxRetries)
                        throw;
                    _logger.Debug($"Retry attempt {retries}...");
                    if (retries == startProcessRetryThreshold)
                    {
                        _logger.Debug($"Try start {Constants.ProblemServerProcess}.");
                        try
                        {
                            Process.Start(Constants.ProblemServerProcess);
                        }
                        catch (Exception pex)
                        {
                            _logger.Error(pex);
                            throw;
                        }
                    }
                    Thread.Sleep(250);
                }
            }

            _service = KeepService ? service : null;
            return service;
        }

        readonly ProblemDisplayModelSort _customSort = new ProblemDisplayModelSort();
        IProblemService _service = null;
        static ProblemComparer _problemComparer = new ProblemComparer();
        static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_service != null)
                    {
                        try { _service.ShutDownAsync(); }
                        catch (Exception) { }
                        _service = null;
                    }                    
                }
                _disposed = true;
            }
        }
        bool _disposed = false;

        public void Dispose() => Dispose(true);
        #endregion
    }    
}
