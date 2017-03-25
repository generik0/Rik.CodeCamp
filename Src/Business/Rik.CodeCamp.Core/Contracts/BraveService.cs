using Castle.Core.Logging;
using Nancy;
using Smooth.IoC.Cqrs;

namespace Rik.CodeCamp.Core.Contracts
{
    public class BraveService : NancyModule
    {
        private readonly ICqrsDispatcher _dispatcher;

        public BraveService(ILogger logger, ICqrsDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            Get[$"{GetType().Name}/IsConnectec"] = parameters => Response.AsJson(true);


        }    
    }

    
}
