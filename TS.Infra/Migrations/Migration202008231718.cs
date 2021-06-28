using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Infra.Migrations
{
	[Migration(202008231718)]
	public class Migration202008231718 : Migration
	{
		public override void Down()
		{
			throw new NotImplementedException();
		}

		public override void Up()
		{
			Update.Table("WhySubject").Set(new
			{
				WhySubjectName = "Labour/People"
			}).Where(new { WhySubjectId = 1 });

			Update.Table("WhySubject").Set(new
			{
				WhySubjectName = "Measure"
			}).Where(new { WhySubjectId = 2 });

			Update.Table("WhySubject").Set(new
			{
				WhySubjectName = "Machines and equipaments"
			}).Where(new { WhySubjectId = 3 });

			Update.Table("WhySubject").Set(new
			{
				WhySubjectName = "Materials"
			}).Where(new { WhySubjectId = 4 });

			Update.Table("WhySubject").Set(new
			{
				WhySubjectName = "Environment"
			}).Where(new { WhySubjectId = 5 });

			Update.Table("WhySubject").Set(new
			{
				WhySubjectName = "Method"
			}).Where(new { WhySubjectId = 6 });


		}
	}
}
