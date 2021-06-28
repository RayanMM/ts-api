using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using TS.Domain.Dtos.Report;
using TS.Infra.Repositories.Report;
using TS.Service.Base;
using System.Linq;

namespace TS.Service.Report
{
	public class ReportService : IService
	{
		private readonly ReportRepository reportRepository;

		public ReportService(ReportRepository reportRepository)
		{
			this.reportRepository = reportRepository;
		}


		public async Task<ReportDto> RetriveReportResultByMenu(int menuSubMenuId, bool menu, int page, int show, string json)
		{
			ReportDto report = new ReportDto();
			int countRows = 0;
			int queryFkIndex = 0;

			try
			{
				//appling filters
				List<ReportFilter> reportFilter = new List<ReportFilter>();

				var filter = JObject.Parse(json);

				var fields = filter["filter"].ToArray();

				foreach(var field in fields)
				{
					JProperty jProperty = field.ToObject<JProperty>();

					reportFilter.Add(new ReportFilter
					{
						Field = jProperty.Name,
						Value = jProperty.Value.ToString()
					});
				}

				var reportDataTable = await reportRepository.RetriveReportResultByMenu(menuSubMenuId, menu, page, show, reportFilter);

				report.IncludeEditModalSize = reportDataTable.IncludeEditModalSize;

				var data = reportDataTable.DataTable;

				List<ReportHeaderInfo> headerinfo = new List<ReportHeaderInfo>();

				headerinfo.Add(new ReportHeaderInfo
				{
                    Header = "Edit",
                    QueryField = "Edit",
                    ShowFilter = false
				});

				var splitHeader = string.IsNullOrEmpty(reportDataTable.ReportHeader) ? null : reportDataTable.ReportHeader.Split(';');

				for (int i = 0; i < data.Columns.Count; i++)
				{
					var column = data.Columns[i];

					if (column.ColumnName.Equals(reportDataTable.QueryPk))
						queryFkIndex = i;

					headerinfo.Add(new ReportHeaderInfo
					{
						Header = string.IsNullOrEmpty(reportDataTable.ReportHeader) ? column.ColumnName : splitHeader[i], 
						QueryField = column.ColumnName,
                        ShowFilter = true
					});
				}

				report.Header = new ReportHeader
				{
					HeaderInfo = headerinfo
				};

				List<ReportRow> reportRow = new List<ReportRow>();

				foreach(DataRow row in data.Rows)
				{
					List<ReportColumns> reportColumns = new List<ReportColumns>();

					reportColumns.Add(new ReportColumns
					{
						ColumnType = 1,
						ColumnValue = row.ItemArray[queryFkIndex].ToString()
					}) ;

					foreach (var rowColumn in row.ItemArray)
					{
						reportColumns.Add(new ReportColumns
						{
                            ColumnType = 0,
							ColumnValue = rowColumn.ToString().Trim()
						});
					}

					reportRow.Add(new ReportRow
					{
						Columns = reportColumns
					});

					countRows++;
				}

				double pages = (show < 0 ? 1 : (reportDataTable.TotalOfRows / show));

				report.MaxPage = (int)Math.Ceiling(pages)-1;

				report.Body = new ReportBody
				{
					Rows = reportRow
				};

				List<int> availablePages = new List<int>();

				if(report.MaxPage <= 5)
				{
					if (report.MaxPage == 0)
						availablePages.Add(0);
					else
						for (int i = 0; i <= report.MaxPage; i++)
							availablePages.Add(i);
				}
				else
				{
					if(page != report.MaxPage)
						page += 1;

					if (page >= 5)
					{
						int count = 0;
						while(count < 5)
						{
							availablePages.Add(page);
							page--;
							count++;
						}

						availablePages.Reverse();
							
					}
					else
						for (int i = 0; i < 5; i++)
							availablePages.Add(i);
				}

				report.Pagination = availablePages;

			}
			catch(Exception e)
			{
				Console.WriteLine(e);
			}

			return report;
		}
	}
}
