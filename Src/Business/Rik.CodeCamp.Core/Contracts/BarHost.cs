using System;
using Castle.Core.Logging;
using Nancy.Hosting.Self;
using Rik.CodeCamp.Core.Installers;

namespace Rik.CodeCamp.Core.Contracts
{
    public class BarHost : IStartup
    {
        private readonly ILogger _logger;
        private readonly IWindsorNancyBootstrapperWrapper _bootstrapper;
        private NancyHost _host1;

        public BarHost(ILogger logger, IWindsorNancyBootstrapperWrapper bootstrapper)
        {
            _logger = logger;
            _bootstrapper = bootstrapper;
        }

        public bool Start()
        {
            try
            {
                var url1 = $"http://localhost/BarHost/";
                _host1 = new NancyHost(new Uri(url1), _bootstrapper);
                _host1.Start();
                _logger.Info($"Running on {url1}");
                return true;
            }
            catch (Exception exception)
            {
                _logger.FatalFormat(exception, "BarHost failed to start");
                throw;
            }
        }

        public void Stop()
        {
            try
            {
                _host1?.Dispose();
            }
            catch (Exception exception)
            {
                _logger.InfoFormat(exception, "ConfigurationService stop error");
            }
        }
    }
}
