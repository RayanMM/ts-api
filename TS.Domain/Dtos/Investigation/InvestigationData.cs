using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Domain.Dtos.Investigation
{
    public class InvestigationData
    {
        public int InvestigationId { get; set; }
        public int EventId { get; set; }
        public string InvestigationDriverName { get; set; }
        public string InvestigationDriverLicense { get; set; }
        public DateTime InvestigationLicenseDueDate { get; set; }
        public string InvestigationLicenseEmissionCountry { get; set; }
        public string InvestigationLicenseEmissionState { get; set; }
        public DateTime InvestigationLastTrainingDefensiveDriving { get; set; }
        public int InvestigationIsCompanyVehicle { get; set; }
        public int VehicleTypeId { get; set; }
        public int InvestigationVehicleYear { get; set; }
        public int InvestigationDriverTrainned { get; set; }
        public int InvestigationVehiclePlaces { get; set; }
        public int InvestigationVehicleSafetyBelt { get; set; }
        public DateTime InvestigationVehicleLastInspection { get; set; }
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

		/// Task
		public int TaskId { get; set; }        
        public string TaskExecuted { get; set; }
        public int TaskIsStandardized { get; set; }
        public int TaskTimeFunctionId { get; set; }
        public int TaskAble { get; set; }
        public DateTime TaskLastTraining { get; set; }
        public int TaskSimilarSituations { get; set; }
        public string TaskWichSimilarSituations { get; set; }
        public string TaskAction { get; set; }
        public string TaskImprovement { get; set; }
        public int TaskIsRightTool { get; set; }
        public int TaskEvidences { get; set; }
    }
}
