using System;
using Castle.Facilities.Logging;
using Castle.Windsor;
using GAIT.Utilities.DI.Attributes;

namespace GAIT.Utilities.Logging
{
    public interface IGeneralNLogFactory
    {
        void AddNLogFactory(IWindsorContainer container);
    }
    [NoIoC]
    public class GeneralNLogFactory : IGeneralNLogFactory
    {
        public void AddNLogFactory(IWindsorContainer container)
        {
            try
            {
                container.AddFacility<LoggingFacility>(f => f.LogUsing(LoggerImplementation.NLog).WithConfig("NLog.config"));
            }
            catch (Exception exception)
            {
                LoggingFactory.Create(GetType()).Fatal(exception, "Initialization failed");
            }
        }
    }
}
