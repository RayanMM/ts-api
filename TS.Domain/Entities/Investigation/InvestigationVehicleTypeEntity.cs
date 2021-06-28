using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Domain.Entities.Investigation
{
    public class InvestigationVehicleTypeEntity
    {
        public int VehicleTypeId { get; set; }
        public string VehicleTypeName { get; set; }
        public bool IsEnabled { get; set; }
    }
}
