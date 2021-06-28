using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Infra.Migrations
{
	[Migration(202009061715)]
	public class Migration202009061715 : Migration
	{
		public override void Down()
		{
			throw new NotImplementedException();
		}

		public override void Up()
		{
			Alter.Table("FormGroup").AddColumn("FormGroupHeader").AsString().Nullable();
		}
	}
}
