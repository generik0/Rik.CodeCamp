using Rik.CodeCamp.Data.Migrations;

namespace Rik.CodeCamp.Core
{
    public class Worker : IWorker
    {
        private readonly IMigrator _migrator;

        public Worker(IMigrator migrator)
        {
            _migrator = migrator;
        }

        public bool Start()
        {
            _migrator.Start();

            return true;
        }
    }
}
