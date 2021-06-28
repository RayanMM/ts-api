using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Domain.Entities.AnalysisCause
{
    public class AnalysisCauseWhyEntity
    {        
        public int EventId { get; set; }
        public int WhySubjectId { get; set; }
        public string WhyAnswer { get; set; }
		public int RowNumber { get; set; }
	}
}
