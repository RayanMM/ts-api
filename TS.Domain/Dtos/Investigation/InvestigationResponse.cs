using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Domain.Dtos.Investigation
{
    public class InvestigationResponse
    {
        public bool Success { get; set; }
        public int? InvestigationId { get; set; }
        public int? TaskId { get; set; }
        public string Message { get; set; }
    }
}

