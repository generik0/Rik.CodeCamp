using Rik.Codecamp.Entities;
using Smooth.IoC.Repository.UnitOfWork;
using Smooth.IoC.UnitOfWork;

namespace Rik.CodeCamp.Repository
{
    public class WorldRepository : Repository<World, int>, IWorldRepository
    {
        public WorldRepository(IDbFactory factory) : base(factory)
        {
        }
    }
}
