using FluentMigrator;

namespace TS.Infra.Migrations
{
	[Migration(202003041045)]
	public class Migration202003041045 : Migration
	{
		public override void Up()
		{
			Create.Table("FormSubmitMessage")
					.WithColumn("FormSubmitMessageId").AsInt32().PrimaryKey().Identity()
					.WithColumn("FormIdFk").AsInt32().ForeignKey("Form", "FormId")
					.WithColumn("MessageType").AsInt32().NotNullable()
					.WithColumn("Message").AsString(1000).NotNullable()
					.WithColumn("IsActive").AsBoolean().WithDefaultValue(1);
		}
		public override void Down()
		{
			
		}

	}
}
