using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Domain.Entities.Occurrence
{
	public class OccurrenceClassificationEntity
	{
		public int OccurrenceClassificationId { get; set; }
		public string OccurrenceClassificationName { get; set; }
		public int IsEnabled { get; set; }
	}
}
