using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Domain.Entities.Occurrence
{
    public class OccurrenceHappenedEntity
    {
        public int HappenedId { get; set; }
        public int HappenedGroupId { get; set; }
        public string HappenedName { get; set; }
        public int IsEnabled { get; set; }
    }
}
