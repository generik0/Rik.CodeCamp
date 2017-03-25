using System.Data;
using System.Threading.Tasks;
using Rik.Codecamp.Entities;
using Rik.CodeCamp.Data.Sessions;
using Rik.CodeCamp.Repository;
using Smooth.IoC.Cqrs;
using Smooth.IoC.Cqrs.Requests;
using Smooth.IoC.UnitOfWork;

namespace Rik.CodeCamp.Core.Requests
{
    public class BraveHandler : Handler, IRequestHandler<Brave,int>
    {
        private readonly IDbFactory _dbFactory;
        private readonly IBraveRepository _braveRepository;


        public BraveHandler(IHandlerFactory handlerFactory, IDbFactory dbFactory, IBraveRepository braveRepository) : base(handlerFactory)
        {
            _dbFactory = dbFactory;
            _braveRepository = braveRepository;
        }

        public async Task<int> ExecuteAsync(Brave request)
        {
            using (var uow = _dbFactory.Create<IUnitOfWork, IFooSession>(IsolationLevel.Serializable))
            {
                return await _braveRepository.SaveOrUpdateAsync(request,uow);
            }
        }

        public int Execute(Brave request)
        {
            throw new System.NotImplementedException();
        }
    }
}
