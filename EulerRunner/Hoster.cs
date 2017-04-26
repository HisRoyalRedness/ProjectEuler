using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using System.Threading.Tasks;

namespace HisRoyalRedness.com
{
    public class Hoster
    {
        public static void Host()
        {
            var retries = 0;
            const int startProcessRetryThreshold = 3;
            const int maxRetries = 10;

            IProblemService service = null;
            List<ProblemSummary> problems = null;
            var success = false;

            _logger.Debug("Starting up.");

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
                    problems = service.GetProblemsAsync().Result;
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

            service?.ShutDownAsync();

            if (problems != null)
                foreach (var problem in problems)
                    Console.WriteLine($"Problem: {problem.ProblemNumber}, Solution: {problem.Solution}");
        }

        static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    }

    // test
}