using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Infra.Migrations
{
    [Migration(202008221600)]
    public class Migration202008221600 : Migration
    {
        public override void Down()
        {

            throw new NotImplementedException();
        }

        public override void Up()
        {            
            Create.PrimaryKey("PK_BodyPart_Id").OnTable("BodyPart").Column("BodyPartId");         
            
            Create.Table("BodyPartEvent")
                .WithColumn("BodyPartEventId").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("EventId").AsInt32().ForeignKey("Event", "EventId")
                .WithColumn("BodyPartId").AsInt32().ForeignKey("BodyPart", "BodyPartId")
                .WithColumn("IsActive").AsBoolean().WithDefaultValue(1);
        }        
    }
}
