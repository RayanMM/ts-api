using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Domain.Entities.Form
{
	public class FormGroupMenuEntity
	{
		[Key]
		public int FormGroupMenuId { get; set; }
		public int FormGroupIdFk { get; set; }
		public int MenusIdFk { get; set; }
	}
}
