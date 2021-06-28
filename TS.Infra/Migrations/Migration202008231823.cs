using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Infra.Migrations
{
	[Migration(202008231823)]
	public class Migration202008231823 : Migration
	{
		public override void Down()
		{
			throw new NotImplementedException();
		}

		public override void Up()
		{
			Alter.Table("Why").AddColumn("RowNumber").AsInt32().Nullable();
		}
	}
}
