using System.Collections.Generic;
using System.Threading.Tasks;
using Rik.Codecamp.Entities;
using Rik.CodeCamp.Data.Sessions;
using Rik.CodeCamp.Repository;
using Smooth.IoC.Cqrs;
using Smooth.IoC.Cqrs.Query;
using Smooth.IoC.UnitOfWork;

namespace Rik.CodeCamp.Core.Queries
{
    public class BraveHandler : Handler, IQueryHandler<Brave>
    {
        private readonly IDbFactory _dbFactory;
        private readonly IBraveRepository _braveRepository;

        public BraveHandler(IHandlerFactory handlerFactory, IDbFactory dbFactory, IBraveRepository braveRepository) : base(handlerFactory)
        {
            _dbFactory = dbFactory;
            _braveRepository = braveRepository;
        }

        public async Task<IEnumerable<Brave>> QueryAsync()
        {
            using (var session = _dbFactory.Create<IFooSession>())
            {
                return await _braveRepository.GetAllAsync(session);
            }
        }

        public IEnumerable<Brave> Query()
        {
            throw new System.NotImplementedException();
        }
    }
}
