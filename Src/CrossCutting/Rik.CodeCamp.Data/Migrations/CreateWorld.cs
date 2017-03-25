﻿using SimpleMigrations;

namespace Rik.CodeCamp.Data.Migrations
{
    [Migration(20170325152200)]
    public class CreateWorld : Migration
    {
        public override void Up()
        {
            Execute(@"CREATE TABLE `new` (
                `Id`	INTEGER PRIMARY KEY AUTOINCREMENT,
                `Value`	NUMERIC
                 );");
        }

        public override void Down()
        {
            throw new System.NotImplementedException();
        }
    }
}