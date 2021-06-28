using System;
using System.Collections.Generic;

namespace TS.Domain.Dtos.Form
{
    public class FormIncludeOrUpdateData
    {
        public string Table { get; set; }
        public string Column { get; set; }

        public int ItemId { get; set; }
        public string Value { get; set; }
        public bool HasChildren { get; set; }
        public List<FormItemChildren> Children { get; set; }
    }

    public class FormItemChildren
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Value { get; set; }
    }
}
