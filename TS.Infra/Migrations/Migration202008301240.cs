using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Infra.Migrations
{
    [Migration(202008301240)]
    public class Migration202008301240 : Migration
    {
        public override void Down()
        {

            throw new NotImplementedException();
        }

        public override void Up()
        {
            Alter.Table("Investigation")
                .AddColumn("InvestigationUseVehicle")
                .AsBoolean()
                .WithDefaultValue(false);
        }
    }
}
