using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Domain.Entities.Form
{
	public class FormEntity
	{
		[Key]
		public int FormId { get; set; }
		public string FormName { get; set; }
		public int FormActive { get; set; }
        public string FormReportQuery { get; set; }
    }
}
