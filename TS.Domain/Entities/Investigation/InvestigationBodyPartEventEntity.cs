using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Domain.Entities.Investigation
{
    public class InvestigationBodyPartEventEntity
    {
        public int EventId { get; set; }
        public int BodyPartId { get; set; }
        public bool IsActive { get; set; }
    }
}
