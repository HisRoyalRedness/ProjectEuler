using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace HisRoyalRedness.com
{
    public class EulerDataContext : NotifyBase
    {
        public EulerDataContext()
        {
            _refreshProblems = new NotifyBase<object>.RelayCommand(RefreshProblems);
        }

        public ObservableCollection<IProblem> Problems { get; private set; } 
            = new ObservableCollection<IProblem>();


        public RelayCommand RefreshCommand => _refreshProblems;
        RelayCommand _refreshProblems;


        void RefreshProblems(object _)
        {
            var dispatcher = Dispatcher.CurrentDispatcher;

            Task.Run(async () =>
            {
                try
                {
                    var retries = 0;
                    const int startProcessRetryThreshold = 3;
                    const int maxRetries = 10;

                    IProblemService service = null;
                    List<ProblemSummary> problems = null;
                    var success = false;

                    _logger.Debug("Refreshing problem list...");

                    while (!success)
                    {
                        try
                        {
                            ChannelFactory<IProblemService> pipeFactory =
                                new ChannelFactory<IProblemService>(
                                    new NetNamedPipeBinding(),
                                    new EndpointAddress(
                                        string.Join("/", Constants.NamedPipeEndpoint, Constants.NamedPipeEndpointAddress)));

                            service = pipeFactory.CreateChannel();
                            problems = await service.GetProblemsAsync();
                            success = true;
                        }
                        catch (Exception ex)
                        {
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
                    await service?.ShutDownAsync();

                    if (problems != null)
                        await dispatcher.InvokeAsync(() =>
                        {
                            foreach (var extra in Problems.Except(problems, _problemComparer).ToList())
                                Problems.Remove(extra);

                            foreach (var problem in problems.Except(Problems, _problemComparer).ToList())
                                Problems.Add(problem);
                    });
                }
                catch(Exception ex)
                {
                    Console.WriteLine();
                }
            });
        }

        static ProblemComparer _problemComparer = new ProblemComparer();
        static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    }    
}
