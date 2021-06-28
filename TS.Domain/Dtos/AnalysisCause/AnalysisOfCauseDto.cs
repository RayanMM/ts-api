using System;
using System.Collections.Generic;
using System.Text;
using TS.Domain.Entities.AnalysisCause;

namespace TS.Domain.Dtos.AnalysisCause
{
	public class AnalysisOfCauseDto
	{
		public int EventId { get; set; }
		public IEnumerable<AnalysisCauseWhyEntity> ListWhy { get; set; }
		public string LessonLearned { get; set; }
	}
}
