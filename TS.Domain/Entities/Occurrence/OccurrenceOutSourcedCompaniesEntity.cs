using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Domain.Entities.Occurrence
{
    public class OccurrenceOutSourcedCompaniesEntity
    {
        public int OutSourcedCompaniesId { get; set; }
        public string OutSourcedCompaniesCode { get; set; }
        public string OutSourcedCompaniesName { get; set; }
        public string OutSourcedCompaniesAdress { get; set; }
        public int IsEnabled { get; set; }
    }
}
