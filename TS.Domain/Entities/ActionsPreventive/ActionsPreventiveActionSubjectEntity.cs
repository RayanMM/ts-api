using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Domain.Entities.ActionsPreventive
{
    public class ActionsPreventiveActionSubjectEntity
    {
        public int ActionSubjectId { get; set; }
        public string ActionSubjectName { get; set; }
        public bool IsEnabled { get; set; }
    }
}
