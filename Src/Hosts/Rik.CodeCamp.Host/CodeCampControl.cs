using System;
using System.IO;
using System.Threading.Tasks;
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
        private IWorker _worker;

        public CodeCampControl()
        {
            _logger = LoggingFactory.Create(GetType());
        }

        public bool Start(HostControl hostControl)
        {
            try
            {
                _bootstrapper = new Bootstrapper();
                _worker = _bootstrapper.Resolve<IWorker>();
                return _worker.Start();
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
                Task.Run(() =>
                {
                    if (File.Exists("C:\\RikCodeCampDb.db"))
                    {
                        File.Delete("C:\\RikCodeCampDb.db");
                    }
                });
                _worker.Stop();
                _bootstrapper?.Dispose();
                CancelAll.Cancel();
                return true;
            }
            catch (Exception exception)
            {
                _logger.Fatal(exception, "Stop failed!!! ");
            }
            return false;
            
            
        }
    }
}