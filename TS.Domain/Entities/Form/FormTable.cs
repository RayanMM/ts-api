using System;
using Dapper.Contrib.Extensions;

namespace TS.Domain.Entities.Form
{
    public class FormTable
    {
        [Key]
        public int FormTableId { get; set; }
        public int FormIdFk { get; set; }
        public string FormTableName { get; set; }
    }
}
