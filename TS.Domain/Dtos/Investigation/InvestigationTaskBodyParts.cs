using System;
using System.Collections.Generic;
using System.Text;
using TS.Domain.Entities.Investigation;

namespace TS.Domain.Dtos.Investigation
{
    public class InvestigationTaskBodyParts
    {
        public InvestigationData InvestigationDataTask { get; set; }
        public List<InvestigationBodyPartEventEntity> BodyParts  { get; set; }
}
}
