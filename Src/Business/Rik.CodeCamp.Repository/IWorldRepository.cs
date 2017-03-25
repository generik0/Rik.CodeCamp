using Rik.Codecamp.Entities;
using Smooth.IoC.Repository.UnitOfWork;

namespace Rik.CodeCamp.Repository
{
    public interface IWorldRepository : IRepository<World, int>
    {
    }
}