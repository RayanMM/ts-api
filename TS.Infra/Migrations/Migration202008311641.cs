using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Infra.Migrations
{
	[Migration(202008311641)]
	public class Migration202008311641 : Migration
	{
		public override void Down()
		{
			throw new NotImplementedException();
		}

		public override void Up()
		{
			Delete.FromTable("Action");

			Alter.Table("Action").AddColumn("RowNumber").AsInt32().Nullable();
		}
	}
}
