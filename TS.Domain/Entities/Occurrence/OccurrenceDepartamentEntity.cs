using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Domain.Entities.Occurrence
{
    public class OccurrenceDepartamentEntity
    {
        public int DepartamentId { get; set; }
        public int DepartamentGroupId { get; set; }
        public string DepartamentName { get; set; }
        public int IsEnabled { get; set; }        
    }   
}
