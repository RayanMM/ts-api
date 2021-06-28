using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Domain.Entities.Form
{
	public class FormGroupSubMenuEntity
	{
		public int FormGroupSubMenuId { get; set; }
		public int FormGroupIdFk { get; set; }
		public int SubMenusIdFk { get; set; }
	}
}
