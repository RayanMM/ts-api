using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Infra.Migrations
{
	[Migration(202003281914)]
	public class Migration202003281914 : Migration
	{
		public override void Down()
		{

		}

		public override void Up()
		{
			Create.Table("ConfParameter")
				.WithColumn("ConfParameterId").AsInt32().PrimaryKey().Identity()
				.WithColumn("ConfParameterGrouper").AsInt32().NotNullable()
				.WithColumn("ConfParameterValueId").AsInt32().NotNullable()
				.WithColumn("ConfParameterValueLabel").AsString(255).NotNullable()
				.WithColumn("ConfParameterDesc").AsString(255).NotNullable()
				.WithColumn("IsActive").AsInt32().NotNullable().WithDefaultValue(1);
		}
	}
}
