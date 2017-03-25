using Rik.Codecamp.Entities;
using Smooth.IoC.Repository.UnitOfWork;
using Smooth.IoC.UnitOfWork;

namespace Rik.CodeCamp.Repository
{
    public class NewRepository : Repository<New, int>, INewRepository
    {
        public NewRepository(IDbFactory factory) : base(factory)
        {
        }
    }
}
