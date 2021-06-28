using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Domain.Entities.AnalysisCause
{
	public class LessonLearnedEntity
	{
		public int LessonLearnedId { get; set; }
		public int EventId { get; set; }
		public string LessonLearnedDescription { get; set; }
	}
}
