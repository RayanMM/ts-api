using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TS.Service.Config;

namespace TS_Api.Controllers
{
    [Route("Configuration")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
		private readonly ConfigurationService configurationService;

		public ConfigurationController(ConfigurationService configurationService)
		{
			this.configurationService = configurationService;
		}

		[HttpGet("GetConfigParameter")]
		[SwaggerOperation(Summary = "Get configuration parameter", Description = "Returns all configuration parameter")]
		[SwaggerResponse(200, "List successfully retrieved")]
		public async Task<IActionResult> GetConfigParameter()
		{
			try
			{
				return Ok(await configurationService.GetConfigParameter());
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
