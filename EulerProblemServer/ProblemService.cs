using System;
using System.Linq;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading;
using System.Windows.Threading;
using NLog;
using System.Threading.Tasks;
using System.Diagnostics;

/*
    Server for problem descriptions (i.e. anything implementing IProblem)

    Keith Fletcher
    Mar 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ProblemService : IProblemService, IDisposable
    {
        public ProblemService()
        {
            _factory = Task.Factory;
            Touch();
        }

        public List<ProblemSummary> GetProblems()
        {
            _logger.Debug(nameof(GetProblems));
            Touch();
            var loader = new ProblemLoader();
            var summaries = loader
                .Problems.Select(kv =>
                        new ProblemSummary(
                            kv.Key,
                            kv.Value.Solution)
                        .Tap(ps => _logger.Debug($"Problem: {ps.ProblemNumber}, Solution: {ps.Solution}")))
                .ToList();

            _logger.Debug($"Found {summaries.Count} problems.");
            return summaries;
        }

        public Task<List<ProblemSummary>> GetProblemsAsync()
            => _factory.StartNew(() => GetProblems());

        public void ShutDown()
        {
            _logger.Debug(nameof(ShutDown));
            CloseAction?.Invoke();
        }

        public Task ShutDownAsync()
            => _factory.StartNew(() => CloseAction?.Invoke());

        public void Dispose()
        {
            _logger.Debug(nameof(Dispose));
            CloseAction?.Invoke();
            CloseAction = null;
        }

        void Touch()
        {
            _logger.Trace("Touch");
            UpdateLastTouch?.Invoke();
        }

        public Task<SolutionResult> SolveProblem(int problemNumber)
        {
            return _factory.StartNew(() =>
            {
                _logger.Debug(nameof(GetProblems));
                Touch();
                var loader = new ProblemLoader();
                var problem = loader.Problems.ContainsKey(problemNumber)
                    ? loader.Problems[problemNumber]
                    : null;

                if (problem == null)
                    return new SolutionResult(null, TimeSpan.Zero);

                var sw = new Stopwatch();
                sw.Start();
                var solution = problem.Solve();
                sw.Stop();
                return new SolutionResult(solution, sw.Elapsed);
            });
        }

        public void Ping()
        {
            _logger.Trace("Ping");
            Touch();
        }

        internal Action CloseAction { get; set; }
        internal Action UpdateLastTouch { get; set; }

        readonly TaskFactory _factory;

        static readonly Logger _logger = LogManager.GetCurrentClassLogger();
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
