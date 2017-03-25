using SimpleMigrations;

namespace Rik.CodeCamp.Data.Migrations
{
    [Migration(20170325151800)]
    public class CreateNew : Migration
    {
        public override void Up()
        {
            Execute(@"CREATE TABLE `Worlds` (
	            `Id`	INTEGER PRIMARY KEY AUTOINCREMENT,
	            `datetime`	NUMERIC
                );");
        }

        public override void Down()
        {
            throw new System.NotImplementedException();
        }
    }
}
