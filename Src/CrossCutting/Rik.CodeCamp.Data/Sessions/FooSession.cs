using System.Data.SQLite;
using Smooth.IoC.UnitOfWork;

namespace Rik.CodeCamp.Data.Sessions
{
    public class FooSession : Session<SQLiteConnection>, IFooSession
    {
        public FooSession(IDbFactory factory, IFooSessionSettings settings) : base(factory, settings.ConnectionString)
        {
        }
    }
}
