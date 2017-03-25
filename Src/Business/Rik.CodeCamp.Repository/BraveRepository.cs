using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Rik.Codecamp.Entities;
using Smooth.IoC.Repository.UnitOfWork;
using Smooth.IoC.UnitOfWork;
using SqlDialect = Dapper.FastCrud.SqlDialect;

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

        public override async Task<IEnumerable<Brave>> GetAllAsync(ISession session)
        {
            var query = $@"SELECT fact.Id, fact.NewId, fact.WorldId, val.Id, val.Value, ts.Id, ts.datetime FROM 
                    {Sql.Table<Brave>(SqlDialect.SqLite)} as fact 
                    INNER JOIN {Sql.Table<New>(SqlDialect.SqLite)} as val ON val.Id = fact.NewId
                    INNER JOIN  {Sql.Table<World>(SqlDialect.SqLite)} as ts ON ts.Id =fact.WorldId  
			Order by ts.datetime asc";

            var actual = await session.QueryAsync<Brave, New, World, Brave>(query,
                    (factdata, val, ts) =>
                    {
                        factdata.New = val;
                        factdata.World = ts;
                        return factdata;
                    });
            return actual;
        }
    }
}
