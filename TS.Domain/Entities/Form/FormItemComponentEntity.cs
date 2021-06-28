using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Domain.Entities.Form
{
	public class FormItemComponentEntity
	{
		[Key]
		public int FormItemId { get; set; }
		public int FormItemIdFk { get; set; }
		public int FormComponentIdFk { get; set; }
		public int FormIdFk { get; set; }
		public int ComponentContext { get; set; }
		public float ComponentsubContext { get; set; }
		public string ComponentType { get; set; }
		public int AllowDrop { get; set; }
	}
}
