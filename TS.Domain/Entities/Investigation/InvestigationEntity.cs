using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TS.Domain.Entities.Investigation
{
    public class InvestigationEntity
    {
        public int InvestigationId { get; set; }
        public int EventId { get; set; }
        public string InvestigationDriverName { get; set; }
        public string InvestigationDriverLicense { get; set; }

        [JsonProperty("InvestigationLicenseDueDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? InvestigationLicenseDueDate { get; set; }
        public string InvestigationLicenseEmissionCountry { get; set; }
        public string InvestigationLicenseEmissionState { get; set; }

        [JsonProperty("InvestigationLastTrainingDefensiveDriving", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? InvestigationLastTrainingDefensiveDriving { get; set; }
        public int InvestigationIsCompanyVehicle { get; set; }
        public int VehicleTypeId { get; set; }
        public int InvestigationVehicleYear { get; set; }
        public int InvestigationDriverTrainned { get; set; }
        public int InvestigationVehiclePlaces { get; set; }
        public int InvestigationVehicleSafetyBelt { get; set; }

        [JsonProperty("InvestigationVehicleLastInspection", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? InvestigationVehicleLastInspection { get; set; }
        public int InvestigationVehicleNPassengers { get; set; }
        public int InvestigationDriverSafetyBelt { get; set; }
        public int InvestigationOccupantsSafetyBelt { get; set; }
        public string InvestigationOccupantsComments { get; set; }
        public int InvestigationIsDriverAllowed { get; set; }
        public int InvestigationVehicleMaintenance { get; set; }
        public int InvestigationUseDefensiveDriving { get; set; }
        public int InvestigationDriverDisturbed { get; set; }
        public int InvestigationDriverDistracted { get; set; }
        public int InvestigationDriverSleep { get; set; }
        public int InvestigationSpeedLimit { get; set; }
        public int InvestigationRealSpeed { get; set; }
        public float InvestigationLoadWeight { get; set; }
        public int ConditionsWeatherId { get; set; }
        public int ConditionsRoadId { get; set; }
        public bool InvestigationUseVehicle { get; set; }
    }
}
