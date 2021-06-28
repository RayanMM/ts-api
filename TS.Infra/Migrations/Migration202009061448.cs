using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Infra.Migrations
{
	[Migration(202009061448)]
	public class Migration202009061448 : Migration
	{
		public override void Down()
		{
			throw new NotImplementedException();
		}

		public override void Up()
		{
			Delete.Column("ActionStatus").Column("ActionComment").FromTable("Action");
		}
	}
}
