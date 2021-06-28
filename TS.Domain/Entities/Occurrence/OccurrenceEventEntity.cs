using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace TS.Domain.Entities.Occurrence
{
    public class OccurrenceEventEntity
    {
        public int? EventId { get; set; }
        public int? OccurrenceClassificationId { get; set; }
        public int? OccurrenceTypeId { get; set; }
        public string EventIdentification { get; set; }
        public string EventInjuredPersonName { get; set; }
        public int? OccupationId { get; set; }
        public int? FacilityId { get; set; }
        public int? DepartamentId { get; set; }
        public string EventSupervisorName { get; set; }
        public int? UserId { get; set; }
        public int? ContractTypeId { get; set; }
        public int? OutSourcedCompaniesId { get; set; }
        public int? HappenedGroupId { get; set; }
        public int? HappenedId { get; set; }
        public string EventDescription { get; set; }
        public string EventActions { get; set; }
        public string EventSIFStatus { get; set; }
        public string EventSIFCriticality { get; set; }

        [JsonProperty("DateTimeStamp", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? DateTimeStamp { get; set; }

        [JsonProperty("AbsenceDateTimeIni", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? AbsenceDateTimeIni { get; set; }

        [JsonProperty("AbsenceDateTimeEnd", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? AbsenceDateTimeEnd { get; set; }
        public string IsVehicleEvent { get; set; }
        public string IsClosed { get; set; }
        public int? UserDepartamentId { get; set; }

    }
}
