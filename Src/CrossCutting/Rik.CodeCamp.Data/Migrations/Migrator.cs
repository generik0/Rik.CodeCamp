using System.Data;
using System.Reflection;
using Rik.CodeCamp.Data.Sessions;
using SimpleMigrations;
using SimpleMigrations.DatabaseProvider;
using Smooth.IoC.UnitOfWork;
using ILogger = Castle.Core.Logging.ILogger;

namespace Rik.CodeCamp.Data.Migrations
{
    public class Migrator : IMigrator
    {
        private readonly ILogger _logger;
        private readonly IDbFactory _dbFactory;

        public Migrator(ILogger logger, IDbFactory dbFactory)
        {
            _logger = logger;
            _dbFactory = dbFactory;
        }

        public void Start()
        {
            using (var uow = _dbFactory.Create<IUnitOfWork, IFooSession>(IsolationLevel.Serializable))
            {
                var databaseProvider = new SqliteDatabaseProvider(uow.Connection as System.Data.Common.DbConnection);
                var migrator = new SimpleMigrator(Assembly.GetCallingAssembly(), databaseProvider);

                migrator.Load();
                migrator.MigrateToLatest();
            }
        }
    }
}
