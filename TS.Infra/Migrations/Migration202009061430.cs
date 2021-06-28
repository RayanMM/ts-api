using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Infra.Migrations
{
	[Migration(202009061430)]
	public class Migration202009061430 : Migration
	{
		public override void Down()
		{
			throw new NotImplementedException();
		}

		public override void Up()
		{
			Insert.IntoTable("ActionSubject")
				.Row(new { ActionSubjectName = "Comment", IsEnabled = true })
				.Row(new { ActionSubjectName = "Status", IsEnabled = true });
		}
	}
}
