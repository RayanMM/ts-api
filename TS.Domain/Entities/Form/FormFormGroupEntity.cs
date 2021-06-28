using System;
using Dapper.Contrib.Extensions;

namespace TS.Domain.Entities.Form
{
    public class FormFormGroupEntity
    {
        [Key]
        public int FormFormGroupId { get; set; }
        public int FormIdFk { get; set; }
        public int FormGroupId { get; set; }
    }
}
