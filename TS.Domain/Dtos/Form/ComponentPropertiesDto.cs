using System;
using TS.Domain.Entities.Form;

namespace TS.Domain.Dtos.Form
{
    public class ComponentPropertiesDto
    {
        public int FormItemPropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyType { get; set; }
        public string FormItemValue { get; set; }

        public static ComponentPropertiesDto From(PropertyEntity from)
        {
            return new ComponentPropertiesDto
            {
                FormItemPropertyId = from.FormItemPropertyId,
                PropertyName = from.PropertyName,
                PropertyType = from.PropertyType,
                FormItemValue = from.FormItemValue
            };
        }
    }
}
