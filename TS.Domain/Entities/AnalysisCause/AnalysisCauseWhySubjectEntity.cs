using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Domain.Entities.AnalysisCause
{
    public class AnalysisCauseWhySubjectEntity
    {
        public int WhySubjectId { get; set; }
        public string WhySubjectName { get; set; }
        public bool IsEnabled { get; set; }
    }
}
