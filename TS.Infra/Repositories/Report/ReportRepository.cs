using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TS.Domain.Dtos.Report;
using TS.Domain.Entities.Form;
using TS.Infra.Base;
using TS.Infra.Context;

namespace TS.Infra.Repositories.Report
{
	public class ReportRepository : IRepository
	{
		private readonly DbContext context;

		public ReportRepository(DbContext context)
		{
			this.context = context;
		}

		public async Task<ReportDataTable> RetriveReportResultByMenu(int menuSubMenuId, bool menu, int page, int show, List<ReportFilter> reportFilter)
		{
			ReportDataTable table = new ReportDataTable();

			FormGroupEntity formGroupItems;
			string query;

			if (menu)
				formGroupItems = await context.Connection.QueryFirstOrDefaultAsync<FormGroupEntity>("SELECT * FROM FormGroup FG JOIN FormGroupMenu FGM on FG.FormGroupId = FGM.FormGroupIdFk WHERE FGM.MenusIdFk = @menuid", new { menuid = menuSubMenuId });
			else
				formGroupItems = await context.Connection.QueryFirstOrDefaultAsync<FormGroupEntity>("SELECT * FROM FormGroup FG JOIN FormGroupSubMenu FGSM on FG.FormGroupId = FGSM.FormGroupIdFk WHERE FGSM.SubMenusIdFk = @subMenusId", new { subMenusId = menuSubMenuId });

			table.IncludeEditModalSize = formGroupItems.FormIncludeEditModalSize;

			query = formGroupItems.FormGroupQuery;

			table.QueryPk = formGroupItems.FormGroupQueryPk;

			table.ReportHeader = formGroupItems.FormGroupHeader;

			string queryFilter = "";

			if (reportFilter.Count() > 0)
			{
				queryFilter += " where ";

				var last = reportFilter.Last();

				foreach (var filter in reportFilter)
				{
					string preperedfilter = $"{ (int.TryParse(filter.Value, out _) ? $"{filter.Field} = {filter.Value}" : $"{filter.Field} like '%" + filter.Value + "%'")}";

					if (!filter.Equals(last))
						queryFilter += $"{preperedfilter} AND ";
					else
						queryFilter += $"{preperedfilter}";
				}
			}

			var queryWithoutOrderBy = query.ToLower().Split(new string[] { "order by" }, StringSplitOptions.None);

			//running to get all rows
			table.TotalOfRows = await context.Connection.QueryFirstOrDefaultAsync<double>($"SELECT COUNT(*) FROM ({queryWithoutOrderBy[0]}) as t1 {queryFilter}");

			int skip = page * show;

			if (!query.ToLower().Contains("order by") && show != -1)
				query += " Order by 1";

			if(show != -1)
				query += $" OFFSET {skip} ROWS FETCH NEXT {show} ROWS ONLY";

			query = $"SELECT * FROM ({query}) as t1 {queryFilter}";

			DataTable dataTable = new DataTable();

			if (!string.IsNullOrEmpty(query))
				dataTable.Load(await context.Connection.ExecuteReaderAsync(query));

			table.DataTable = dataTable;

			return table;
		}
	}
}
