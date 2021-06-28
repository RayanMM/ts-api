using System;
using System.Collections.Generic;
using System.Text;
using TS.Domain.Entities.AnalysisCause;

namespace TS.Domain.Dtos.AnalysisCause
{
	public class AnalisysOfCauseRequestDto
	{
		public int EventId { get; set; }
		public List<AnalysisCauseWhyEntity> ListWhy { get; set; }
		public string LessonLearned { get; set; }
	}
}
