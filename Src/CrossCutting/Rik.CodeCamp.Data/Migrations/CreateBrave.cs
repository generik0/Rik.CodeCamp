using SimpleMigrations;

namespace Rik.CodeCamp.Data.Migrations
{
    [Migration(20170325152500)]
    public class CreateBrave : Migration
    {
        public override void Up()
        {
            Execute(@"CREATE TABLE `Braves` (
	            `Id`	INTEGER PRIMARY KEY AUTOINCREMENT,
	            `NewId`	INTEGER,
	            `WorldId`	INTEGER,
                FOREIGN KEY(NewId) REFERENCES New(Id)
                FOREIGN KEY(WorldId) REFERENCES World(Id)
                );");
        }

        public override void Down()
        {
            throw new System.NotImplementedException();
        }
    }
}
