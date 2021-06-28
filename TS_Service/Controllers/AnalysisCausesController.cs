using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TS.Domain.Dtos.AnalysisCause;
using TS.Domain.Entities.AnalysisCause;
using TS.Service.AnalysisCause;

namespace TS_Api.Controllers
{
    [Route("AnalysisCause")]
    [ApiController]
    public class AnalysisCausesController : ControllerBase
    {
		private readonly AnalysisCausesService analysisCausesService;

		public AnalysisCausesController(AnalysisCausesService analysisCausesService)
		{
			this.analysisCausesService = analysisCausesService;
		}

		[HttpGet("GetAnalisysCausesWhy")]
		[SwaggerOperation(Summary = "Get analysis causes why", Description = "Returns all analysis causes why")]
		[SwaggerResponse(200, "List successfully retrieved")]
		public async Task<IActionResult> GetAnalysisCauseWhy(int eventId)
		{
			try
			{
				return Ok(await analysisCausesService.GetAnalysisCausesWhy(eventId));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("GetAnalysisCausesWhySubject")]
		[SwaggerOperation(Summary = "Get analysis causes why subject", Description = "Returns all analysis causes why subject")]
		[SwaggerResponse(200, "List successfully retrieved")]
		public async Task<IActionResult> GetAnalysisCausesWhySubject()
		{
			try
			{
				return Ok(await analysisCausesService.GetAnalysisCausesWhySubject());
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost("IncludeAnalysisCausesWhy")]
		[SwaggerOperation(Summary = "Include analysis causes why", Description = "Include all analysis causes why")]
		[SwaggerResponse(200, "List successfully retrieved")]
		public async Task<IActionResult> IncludeAnalysisCausesWhy([FromBody] AnalisysOfCauseRequestDto command)
		{
			try
			{
				return Ok(await analysisCausesService.IncludeAnalysisCausesWhy(command));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
