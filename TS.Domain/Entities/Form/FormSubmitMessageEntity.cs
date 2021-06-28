using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Domain.Entities.Form
{
	public class FormSubmitMessageEntity
	{
		public int FormSubmitMessageId { get; set; }
		public int FormId { get; set; }
		public int MessageType { get; set; }
		public string Message { get; set; }
		public int IsActive { get; set; }
	}
}
