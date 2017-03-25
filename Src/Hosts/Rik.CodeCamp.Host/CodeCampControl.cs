using System;
using GAIT.Utilities.Logging;
using NLog;
using Rik.CodeCamp.Core;
using Topshelf;
using static GAIT.Utilities.GeneralBootstrapper;

namespace Rik.CodeCamp.Host
{
    internal class CodeCampControl : ServiceControl
    {
        private Bootstrapper _bootstrapper;
        private readonly ILogger _logger;

        public CodeCampControl()
        {
            _logger = LoggingFactory.Create(GetType());
        }

        public bool Start(HostControl hostControl)
        {
            try
            {
                _bootstrapper = new Bootstrapper();
                var worker = _bootstrapper.Resolve<IWorker>();
                return worker.Start();
            }
            catch (Exception exception)
            {
                _logger.Fatal(exception, "Start failed!!! ");
            }
            return false;
        }

        public bool Stop(HostControl hostControl)
        {
            try
            {
                _bootstrapper?.Dispose();
                CancelAll.Cancel();
            }
            catch (Exception exception)
            {
                _logger.Fatal(exception, "Stop failed!!! ");
            }
            return false;
            
            
        }
    }
}