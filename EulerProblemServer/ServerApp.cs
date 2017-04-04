using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

/*
    A Windows application host for the ProblemService

    Keith Fletcher
    Mar 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    public class ServerApp : Application
    {
        [STAThread]
        static void Main(string[] args) => new ServerApp().Run();

        public ServerApp()
        {
            _logger.Info("Starting up the Problem Server.");
            var timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += (o, e) =>
            {
                if (DateTime.Now - _lastTouch > TimeSpan.FromSeconds(5))
                {
                    _logger.Trace("Service timed out.");
                    ShutdownServer();
                }
            };
            timer.IsEnabled = true;
        }

        protected override void OnStartup(StartupEventArgs sea)
        {
            _service = new ProblemService();
            _host = new ServiceHost(_service, new Uri(Constants.NamedPipeEndpoint));

            _service.UpdateLastTouch = () => _lastTouch = DateTime.Now;
            _service.CloseAction = () => 
                Task.Delay(TimeSpan.FromSeconds(1)).ContinueWith(_ => Dispatcher.Invoke(ShutdownServer));

            AddEndpoints();
            IncludeExceptionsInFaults();

            _host.Open();
        }

        void ShutdownServer()
        {
            _host?.Close();
            _host = null;
            if (!_shuttingDown)
            {
                _logger.Debug("Shutting down the Problem Server.");
                _shuttingDown = true;
                Shutdown();
            }
        }

        protected override void OnExit(ExitEventArgs e) => ShutdownServer();

        void AddEndpoints()
        {
            _host.AddServiceEndpoint(
                typeof(IProblemService),
                new NetNamedPipeBinding(),
                Constants.NamedPipeEndpointAddress);

            foreach (var ep in _host.Description.Endpoints)
                _logger.Debug($"Listening on {ep.ListenUri}");
        }

        void IncludeExceptionsInFaults()
        {
            var debug = _host.Description.Behaviors.Find<ServiceDebugBehavior>();
            if (debug == null)
                _host.Description.Behaviors.Add(
                     new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
            else
                if (!debug.IncludeExceptionDetailInFaults)
                    debug.IncludeExceptionDetailInFaults = true;
            _logger.Trace("IncludeExceptionDetailInFaults enabled.");
        }

        bool _shuttingDown = false;
        ProblemService _service = null;
        ServiceHost _host = null;
        DateTime _lastTouch = DateTime.Now;
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
