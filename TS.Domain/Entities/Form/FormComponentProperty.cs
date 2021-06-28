using Dapper.Contrib.Extensions;

namespace TS.Domain.Entities.Form
{
    public class FormComponentProperty
    {
        [Key]
        public int FormComponentPropertyId { get; set; }
        public int FormComponentIdFk { get; set; }
        public int FormPropertyIdFk { get; set; }
        public string DefaultValue { get; set; }
    }
}
