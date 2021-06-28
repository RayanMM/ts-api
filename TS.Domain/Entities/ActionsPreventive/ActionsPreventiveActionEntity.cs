using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Domain.Entities.ActionsPreventive
{
    public class ActionsPreventiveActionEntity
    {
        public int EventId { get; set; }
        public int ActionSubjectId { get; set; }
        public string ActionName { get; set; }
		public int RowNumber { get; set; }
	}
}
