using System.Collections.Generic;
using System.Linq;
using Rik.CodeCamp.Data.Migrations;

namespace Rik.CodeCamp.Core
{
    public class Worker : IWorker
    {
        private readonly IMigrator _migrator;
        private readonly IEnumerable<IStartup> _startups;

        public Worker(IMigrator migrator, IEnumerable<IStartup> startups )
        {
            _migrator = migrator;
            _startups = startups;
        }

        public bool Start()
        {
            _migrator.Start();
            return _startups.All(x=>x.Start());
        }

        public void Stop()
        {
            _startups.ToList().ForEach(x=>x.Stop());
        }
    }
}
