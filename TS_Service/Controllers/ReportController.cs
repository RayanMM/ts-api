using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TS.Service.Report;

namespace TS_Api.Controllers
{
    [Route("Report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ReportService reportService;

        public ReportController(ReportService reportService)
        {
            this.reportService = reportService;
        }

        [HttpPost("ReportByMenu")]
        [SwaggerOperation(Summary = "Returns the dynamic data", Description = "Returns the data of the previous registered query from the form group related to menu")]
        [SwaggerResponse(200, "List successfully retrieved")]
        public async Task<IActionResult> RetriveReportResultByMenu(int menuSubMenuId, bool menu, int page, int show, [FromBody] JsonElement json)
        {
            try
            {
                string filter = JsonSerializer.Serialize(json);

                return Ok(await reportService.RetriveReportResultByMenu(menuSubMenuId, menu, page, show, filter));
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}