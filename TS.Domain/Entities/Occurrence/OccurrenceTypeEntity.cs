using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Domain.Entities.Occurrence
{
    public class OccurrenceTypeEntity
    {
        public int OccurrenceTypeId { get; set; }
        public string OccurrenceTypeName { get; set; }
        public int IsEnabled { get; set; }
    }
}
