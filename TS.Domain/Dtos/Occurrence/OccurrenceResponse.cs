using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Domain.Dtos.Occurrence
{
    public class OccurrenceResponse
    {
        public bool Success { get; set; }
        public int? OccurrenceId { get; set; }
        public string Message { get; set; }
    }
}
