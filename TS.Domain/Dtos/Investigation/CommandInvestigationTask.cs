using System;
using System.Collections.Generic;
using System.Text;
using TS.Domain.Entities.Investigation;

namespace TS.Domain.Dtos.Investigation
{
    public class CommandInvestigationTask
    {
        public InvestigationEntity Investigation { get; set; }
        public InvestigationTaskEntity Task { get; set; }
        public List<InvestigationBodyPartEventEntity> BodyParts { get; set; }
}
}
