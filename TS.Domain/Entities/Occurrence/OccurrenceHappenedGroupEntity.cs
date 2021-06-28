using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Domain.Entities.Occurrence
{
    public class OccurrenceHappenedGroupEntity
    {
        public int HappenedGroupId { get; set; }
        public string HappenedGroupName { get; set; }
        public int IsEnabled { get; set; }        
    }
}
