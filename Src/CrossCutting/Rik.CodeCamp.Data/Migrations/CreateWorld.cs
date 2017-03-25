using SimpleMigrations;

namespace Rik.CodeCamp.Data.Migrations
{
    [Migration(20170325152200)]
    public class CreateWorld : Migration
    {
        public override void Up()
        {
            Execute(@"CREATE TABLE `Worlds` (
	            `Id`	INTEGER PRIMARY KEY AUTOINCREMENT,
	            `datetime`	datetime not null
                );");
        }

        public override void Down()
        {
            throw new System.NotImplementedException();
        }
    }
}
