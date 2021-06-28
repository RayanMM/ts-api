using System;
using System.Collections.Generic;
using TS.Domain.Entities.Form;

namespace TS.Domain.Dtos.Form
{
    public class FormInfoDto
    {
        public string FormName { get; set; }
        public IEnumerable<FormTableDto> FormTables { get; set; }
        public int FormGroupId { get; set; }

        public static FormInfoDto From(FormEntity fromForm, IEnumerable<FormTableDto> fromTable, FormGroupEntity fromGroup)
        {
            return new FormInfoDto
            {
                FormName = fromForm.FormName,
                FormTables = fromTable,
                FormGroupId = fromGroup.FormGroupId
            };
        }
    }
}
