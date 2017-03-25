using System.Collections.Generic;
using System.Threading.Tasks;
using Smooth.IoC.Cqrs;

namespace Rik.CodeCamp.Core.Contracts
{
    public class BarContract : IBarContract
    {
        private readonly ICqrsDispatcher _dispatcher;

        public BarContract(ICqrsDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async Task<IEnumerable<BraveService>> GetAllBraves()
        {
            return await _dispatcher.QueryAsync<BraveService>();
        }

        public async Task<int> SaveOrUpdateBrave(BraveService brave)
        {
            return await _dispatcher.ExecuteAsync<BraveService, int>(brave);
        }

        public async Task<BraveService> GetBrave(int id)
        {
            return await _dispatcher.QuerySingleOrDefaultAsync<BraveService,BraveService>(new BraveService{Id = id});
        }
        public bool IsConnected()
        {
            return true;
        }

        
    }
}
