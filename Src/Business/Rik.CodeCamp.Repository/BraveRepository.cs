using System;
using System.Threading.Tasks;
using Rik.Codecamp.Entities;
using Smooth.IoC.Repository.UnitOfWork;
using Smooth.IoC.UnitOfWork;

namespace Rik.CodeCamp.Repository
{
    public class BraveRepository : Repository<Brave, int>, IBraveRepository
    {
        private readonly INewRepository _newRepository;
        private readonly IWorldRepository _worldRepository;

        public BraveRepository(IDbFactory factory, INewRepository newRepository, IWorldRepository worldRepository) : base(factory)
        {
            _newRepository = newRepository;
            _worldRepository = worldRepository;
        }
        public override async Task<int> SaveOrUpdateAsync(Brave entity, IUnitOfWork uow)
        {
            var newId= await _newRepository.SaveOrUpdateAsync(entity.New, uow);
            var worldId = await _worldRepository.SaveOrUpdateAsync(entity.World, uow);
            entity.NewId = newId;
            entity.WorldId = worldId;
            var actual = SaveOrUpdate(entity,uow);
            return actual;
        }
    }
}
