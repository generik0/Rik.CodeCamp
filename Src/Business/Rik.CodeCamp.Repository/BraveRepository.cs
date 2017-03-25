using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Rik.Codecamp.Entities;
using Smooth.IoC.Repository.UnitOfWork;
using Smooth.IoC.Repository.UnitOfWork.Extensions;
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

        //This is using Smooth.IoC.Repo
        public override async Task<int> SaveOrUpdateAsync(Brave entity, IUnitOfWork uow)
        {
            var newId = entity.NewId !=0 ? entity.NewId : await GetOrCreateValue(entity.New, entity.New.Value, uow, nameof(New.Value), _newRepository);
            var worldId = entity.WorldId !=0 ? entity.WorldId : await GetOrCreateValue(entity.World, entity.World.DateTime, uow, nameof(World.DateTime),_worldRepository);

            entity.NewId = newId;
            entity.WorldId = worldId;
            var actual = SaveOrUpdate(entity,uow);
            return actual;
        }

       private static async Task<int> GetOrCreateValue<T, TValue>(T entity, TValue value, IUnitOfWork uow, string column, IRepository<T,int> repository) where T : class, IEntity<int>
        {
            return await Task.Run(() =>
            {
                var actual = uow.Find<T>(statement =>
                {
                    statement.Where($"{column:C} = @p1")
                        .WithParameters(new { p1 = value });
                }).FirstOrDefault();
                return actual?.Id ?? repository.SaveOrUpdate(entity, uow);
            });
        }

        //This is using dapper (with some name resolving ny FastCrud)
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

        //This is using Dapper.FastCrud
        public override async Task<Brave> GetAsync(Brave entity, ISession session)
        {
            var actual = await Task.Run(() =>
            {
                return session.Get(entity, statement =>
                {
                    statement.Include<New>(join => join.InnerJoin())
                        .Include<World>(join => join.InnerJoin());
                });
            });
            return actual;
        }
    }
}
