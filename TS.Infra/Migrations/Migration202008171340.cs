using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Infra.Migrations
{
	[Migration(202008171340)]
	public class Migration202008171340 : Migration
	{
		public override void Down()
		{
			throw new NotImplementedException();
		}

		public override void Up()
		{
			Create.Table("BodyPart")
				.WithColumn("BodyPartId").AsInt32().NotNullable()
				.WithColumn("BodyPartName").AsString().NotNullable();

			Insert.IntoTable("BodyPart")
				.Row(new { BodyPartId = 1,  BodyPartName = "Front left face's side" })
				.Row(new { BodyPartId = 2, BodyPartName = "Front right face's side" })
				.Row(new { BodyPartId = 3, BodyPartName = "Mouth" })
				.Row(new { BodyPartId = 4, BodyPartName = "Left eye" })
				.Row(new { BodyPartId = 5, BodyPartName = "Right eye" })
				.Row(new {BodyPartId = 6, BodyPartName = "Front left skull`s side" })
				.Row(new {BodyPartId = 7, BodyPartName = "Front right skull`s side" })
				.Row(new {BodyPartId = 8, BodyPartName = "Front right neck`s side" })
				.Row(new {BodyPartId = 9, BodyPartName = "Front left neck`s side" })
				.Row(new {BodyPartId = 10, BodyPartName = "Front left shoulder" })
				.Row(new {BodyPartId = 11, BodyPartName = "Front right shoulder" })
				.Row(new {BodyPartId = 12, BodyPartName = "Front right chest" })
				.Row(new {BodyPartId = 13, BodyPartName = "Front left chest" })
				.Row(new {BodyPartId = 14, BodyPartName = "Front left nipple" })
				.Row(new {BodyPartId = 15, BodyPartName = "Front right nipple" })
				.Row(new {BodyPartId = 16, BodyPartName = "Front left biceps" })
				.Row(new {BodyPartId = 17, BodyPartName = "Front right biceps" })
				.Row(new {BodyPartId = 18, BodyPartName = "Front left elbow" })
				.Row(new {BodyPartId = 19, BodyPartName = "Front right elbow" })
				.Row(new {BodyPartId = 20, BodyPartName = "Front left forearm" })
				.Row(new {BodyPartId = 21, BodyPartName = "Front right forearm" })
				.Row(new { BodyPartId = 22, BodyPartName = "Front left wrist" })
				.Row(new {BodyPartId = 23, BodyPartName = "Front right wrist" })
				.Row(new {BodyPartId = 24, BodyPartName = "Front left hand" })
				.Row(new {BodyPartId = 25, BodyPartName = "Front right hand" })
				.Row(new {BodyPartId = 26, BodyPartName = "Front left groin" })
				.Row(new {BodyPartId = 27, BodyPartName = "Front left belly" })
				.Row(new {BodyPartId = 28, BodyPartName = "Front right belly" })
				.Row(new {BodyPartId = 29, BodyPartName = "Navel" })
				.Row(new {BodyPartId = 30, BodyPartName = "Front right waist" })
				.Row(new {BodyPartId = 31, BodyPartName = "Front left waist" })
				.Row(new {BodyPartId = 32, BodyPartName = "Front right thigh" })
				.Row(new {BodyPartId = 33, BodyPartName = "Front left thigh" })
				.Row(new {BodyPartId = 34, BodyPartName = "Front right knee" })
				.Row(new {BodyPartId = 35, BodyPartName = "Front left knee" })
				.Row(new { BodyPartId = 37, BodyPartName = "Front left shin" })
				.Row(new {BodyPartId = 38, BodyPartName = "Front right shin" })
				.Row(new {BodyPartId = 39, BodyPartName = "Front left anckle" })
				.Row(new {BodyPartId = 40, BodyPartName = "Front right anckle" })
				.Row(new {BodyPartId = 41, BodyPartName = "Front right foot" })
				.Row(new {BodyPartId = 42, BodyPartName = "Front left foot" })
				.Row(new {BodyPartId = 43, BodyPartName = "Back left skull bellow" })
				.Row(new {BodyPartId = 44, BodyPartName = "Back right skull bellow" })
				.Row(new {BodyPartId = 45, BodyPartName = "Back right skull top" })
				.Row(new {BodyPartId = 46, BodyPartName = "Back left skull top" })
				.Row(new {BodyPartId = 47, BodyPartName = "Back left neck" })
				.Row(new {BodyPartId = 48, BodyPartName = "Back right neck" })
				.Row(new {BodyPartId = 49, BodyPartName = "Back left shoulder" })
				.Row(new {BodyPartId = 50, BodyPartName = "Back right shoulder" })
				.Row(new {BodyPartId = 51, BodyPartName = "Back left top" })
				.Row(new {BodyPartId = 52, BodyPartName = "Back right top" })
				.Row(new {BodyPartId = 53, BodyPartName = "Back left middle" })
				.Row(new { BodyPartId = 54, BodyPartName = "Back right middle" })
				.Row(new {BodyPartId = 55, BodyPartName = "Back left triceps" })
				.Row(new {BodyPartId = 56, BodyPartName = "Back right triceps" })
				.Row(new {BodyPartId = 57, BodyPartName = "Back left elbow" })
				.Row(new {BodyPartId = 58, BodyPartName = "Back right elbow" })
				.Row(new {BodyPartId = 59, BodyPartName = "Back left bottom" })
				.Row(new {BodyPartId = 60, BodyPartName = "Back right bottom" })
				.Row(new {BodyPartId = 61, BodyPartName = "Back left forearm" })
				.Row(new {BodyPartId = 62, BodyPartName = "Back right forearm" })
				.Row(new {BodyPartId = 63, BodyPartName = "Back left wrist" })
				.Row(new {BodyPartId = 64, BodyPartName = "Back right wrist" })
				.Row(new {BodyPartId = 65, BodyPartName = "Back left hand" })
				.Row(new {BodyPartId = 66, BodyPartName = "Back right hand" })
				.Row(new {BodyPartId = 67, BodyPartName = "Back left waist" })
				.Row(new {BodyPartId = 68, BodyPartName = "Back right waist" })
				.Row(new {BodyPartId = 69, BodyPartName = "Back left gluteus" })
				.Row(new {BodyPartId = 70, BodyPartName = "Back right gluteus" })
				.Row(new {BodyPartId = 71, BodyPartName = "Back left thigh" })
				.Row(new {BodyPartId = 72, BodyPartName = "Back right thigh" })
				.Row(new {BodyPartId = 73, BodyPartName = "Back left knee" })
				.Row(new {BodyPartId = 74, BodyPartName = "Back right knee" })
				.Row(new {BodyPartId = 75, BodyPartName = "Back left calf" })
				.Row(new {BodyPartId = 76, BodyPartName = "Back right calf" })
				.Row(new {BodyPartId = 77, BodyPartName = "Back left anckle" })
				.Row(new {BodyPartId = 78, BodyPartName = "Back right anckle" })
				.Row(new {BodyPartId = 79, BodyPartName = "Back left foot" })
				.Row(new {BodyPartId = 80, BodyPartName = "Back right foot" })
				;
		}
	}
}
