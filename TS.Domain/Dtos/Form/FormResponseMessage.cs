using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Domain.Dtos.Form
{
	public class FormResponseMessage
	{
		public int Identifier { get; set; }
		public int MessageType { get; set; }
		public string Message { get; set; }
	}
}
