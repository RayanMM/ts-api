using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Domain.Entities.Occurrence
{
    public class OccurrenceFacilityEntity
    {
        public int FacilityId { get; set; }
        public string FacilityName { get; set; }
        public string FacilityCity { get; set; }
        public string FacilityState { get; set; }
        public string FacilityAdress { get; set; }
        public string FacilityAdressNumber { get; set; }
        public int IsEnabled { get; set; }
    }
}
