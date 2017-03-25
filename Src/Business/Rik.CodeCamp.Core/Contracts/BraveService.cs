using System.Threading.Tasks;
using Castle.Core.Logging;
using Nancy;
using Nancy.ModelBinding;
using Rik.Codecamp.Entities;
using Smooth.IoC.Cqrs;

namespace Rik.CodeCamp.Core.Contracts
{
    public class BraveService : NancyModule
    {
        private readonly ILogger _logger;
        private readonly ICqrsDispatcher _dispatcher;

        public BraveService(ILogger logger, ICqrsDispatcher dispatcher)
        {
            _logger = logger;
            _dispatcher = dispatcher;
            Get[$"{GetType().Name}/IsConnected"] = parameters => Response.AsJson(true);
            Get[$"{GetType().Name}/GetAll", true] = async (x, ct) => await GetAll();
            Get[$"{GetType().Name}/Get/{{id}}", true] = async (x, ct) => await GetId(x.id);
            Put[$"{GetType().Name}/SaveOrUpdate",true] = async (x, ct) => await Task.Run(()=>
            {
                var model = this.Bind<Brave>();
                return SaveOrUpdate(model);
            });
        }

        private async Task<Response> SaveOrUpdate(Brave model)
        {
            var actual = await _dispatcher.ExecuteAsync<Brave, int>(model);
            return Response.AsJson(actual);
        }

        private async Task<Response> GetId(int id)
        {
            var actual = await _dispatcher.QuerySingleOrDefaultAsync<Brave, Brave>(new Brave { Id = id });
            return Response.AsJson(actual);
        }

        private async Task<Response> GetAll()
        {
            var actual = await _dispatcher.QueryAsync<Brave>();
            return Response.AsJson(actual);
        }
    }

    
}
