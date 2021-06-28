using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Domain.Entities.Form
{
	public class FormItemProperty
	{
		[Key]
		public int FormItemPropertyId { get; set; }
		public int FormItemIdFk { get; set; }
		public int FormPropertyIdFk { get; set; }
		public string FormItemValue { get; set; }
	}
}
