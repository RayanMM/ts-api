using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Domain.Entities.Occurrence
{
    public class OccurrenceJobEntity
    {
        public int OccupationId { get; set; }
        public string OccupationName { get; set; }
        public int IsEnabled { get; set; }
    }
}
