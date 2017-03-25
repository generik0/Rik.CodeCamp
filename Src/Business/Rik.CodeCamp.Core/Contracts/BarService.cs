using System.Collections.Generic;
using System.Threading.Tasks;
using Rik.Codecamp.Entities;
using Smooth.IoC.Cqrs;

namespace Rik.CodeCamp.Core.Contracts
{
    public class BarService : IBarService
    {
        private readonly ICqrsDispatcher _dispatcher;

        public BarService(ICqrsDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async Task<IEnumerable<Brave>> GetAllBraves()
        {
            return await _dispatcher.QueryAsync<Brave>();
        }

        public async Task<int> SaveOrUpdateBrave(Brave brave)
        {
            return await _dispatcher.ExecuteAsync<Brave, int>(brave);
        }

        public async Task<Brave> GetBrave(int id)
        {
            return await _dispatcher.QuerySingleOrDefaultAsync<Brave,Brave>(new Brave{Id = id});
        }
        public bool IsConnected()
        {
            return true;
        }

        
    }
}
