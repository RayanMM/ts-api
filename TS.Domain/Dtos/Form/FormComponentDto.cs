using TS.Domain.Entities.Form;

namespace TS.Domain.Dtos.Form
{
    public class FormComponentDto
    {
        public int FormComponentId { get; set; }
        public string ComponentName { get; set; }
        public string ComponentIcon { get; set; }
        public int ComponentContext { get; set; }
        public double ComponentSubContext { get; set; }
        public string ComponentType { get; set; }
        public bool AllowDrop { get; set; }

        public static FormComponentDto From(FormComponentEntity from)
        {
            return new FormComponentDto
            {
                FormComponentId = from.FormComponentId,
                ComponentName = from.ComponentName,
                ComponentIcon = from.ComponentIcon,
                ComponentContext = from.ComponentContext,
                ComponentSubContext = from.ComponentSubContext,
                ComponentType = from.ComponentType,
                AllowDrop = from.AllowDrop
            };
        }
    }
}
