using Rik.Codecamp.Entities;
using Smooth.IoC.Repository.UnitOfWork;
using Smooth.IoC.UnitOfWork;

namespace Rik.CodeCamp.Repository
{
    public class BraveRepository : Repository<Brave, int>, IBraveRepository
    {
        public BraveRepository(IDbFactory factory) : base(factory)
        {
        }
    }
}
