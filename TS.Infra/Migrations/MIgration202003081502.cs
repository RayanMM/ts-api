using System;
using FluentMigrator;

namespace TS.Infra.Migrations
{
    [Migration(202003081502)]
    public class Migration202003081502 : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Alter.Table("Form")
                .AddColumn("FormMainColumn").AsString();
        }
    }
}
