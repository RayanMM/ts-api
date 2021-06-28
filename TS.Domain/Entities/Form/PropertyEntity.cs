using Dapper.Contrib.Extensions;

namespace TS.Domain.Entities.Form
{
    public class PropertyEntity
    {
        [Key]
        public int FormItemPropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyType { get; set; }
        public string FormItemValue { get; set; }
    }
}
