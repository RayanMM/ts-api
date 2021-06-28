using System;
using Dapper.Contrib.Extensions;

namespace TS.Domain.Entities.Form
{
    public class FormGroupEntity
    {
        [Key]
        public int FormGroupId { get; set; }
        public string FormGroupName { get; set; }
        public string FormGroupQuery { get; set; }
        public string FormGroupQueryPk { get; set; }
        public string FormIncludeEditModalSize { get; set; }
		public string FormGroupHeader { get; set; }
	}
}
