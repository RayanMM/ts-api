using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TS.Domain.Dtos.Report
{
	public class ReportDto
	{
		public string IncludeEditModalSize { get; set; }
		public int MaxPage { get; set; }
		public ReportHeader Header { get; set; }
		public ReportBody Body { get; set; }
		public List<int> Pagination { get; set; }
	}

	public class ReportHeader
	{
		public List<ReportHeaderInfo> HeaderInfo { get; set; }
	}

	public class ReportHeaderInfo
	{
		public string Header { get; set; }
		public string QueryField { get; set; }
        public bool ShowFilter { get; set; }
    }

	public class ReportBody
	{
		public List<ReportRow> Rows { get; set; }
	}

	public class ReportRow
	{
		public List<ReportColumns> Columns { get; set; }
	}

	public class ReportColumns
	{
        public int ColumnType { get; set; }
		public string ColumnValue { get; set; }
	}

	public class ReportDataTable
	{
		public string IncludeEditModalSize { get; set; }
		public DataTable DataTable { get; set; }
		public double TotalOfRows { get; set; }
        public string QueryPk { get; set; }
		public string ReportHeader { get; set; }
	}

	public class ReportFilter
	{
		public string Field { get; set; }
		public string Value { get; set; }
	}
}
