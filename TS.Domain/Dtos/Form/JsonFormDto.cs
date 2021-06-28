using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Domain.Dtos.Form
{
	public class JsonFormDto
	{
		public Form_Config Form_config { get; set; }
		public Form_Fields Form_fields { get; set; }
	}

	public class Form_Config
	{
		public int Form_id { get; set; }
		public string Form_name { get; set; }
	}

	public class Form_Fields
	{
		public List<Form_Items> Items { get; set; }
	}

	public class Form_Items
	{
		public int ItemId { get; set; }
		public int ItemParentId { get; set; }
		public int? ItemContext { get; set; }
		public float? ItemSubContext { get; set; }
		public string ItemType { get; set; }
		public int ItemComponentId { get; set; }
		public string ItemClassName { get; set; }
        public string ItemPlaceholder { get; set; }
        public string ItemDefaultValue { get; set; }
		public string ItemLabel { get; set; }
		public string ItemRequired { get; set; }
		public string ItemMaxLength { get; set; }
		public string ItemName { get; set; }
		public string ItemReadOnly { get; set; }
		public string ItemChecked { get; set; }
		public string ItemRowNumber { get; set; }
		public string ItemTableField { get; set; }
		public List<Form_Items> ItemChildren { get; set; }

	}
}
