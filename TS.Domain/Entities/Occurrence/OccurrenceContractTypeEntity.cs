using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Domain.Entities.Occurrence
{
    public class OccurrenceContractTypeEntity
    {
        public int ContractTypeId { get; set; }

        public string ContractTypeName { get; set; }

        public int IsEnabled { get; set; }
    }
}
