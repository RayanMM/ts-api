using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Domain.Entities.Form
{
    public class FormComponentEntity
    {
        [Key]
        public int FormComponentId { get; set; }
        public string ComponentName { get; set; }
        public bool IsActive { get; set; }
        public string ComponentIcon { get; set; }
        public int ComponentContext { get; set; }
        public double ComponentSubContext { get; set; }
        public string ComponentType { get; set; }
        public bool AllowDrop { get; set; }
    }
}
