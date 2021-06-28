using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TS.Domain.Dtos.Investigation;
using TS.Domain.Entities.Investigation;
using TS.Service.Investigation;

namespace TS_Api.Controllers
{
    [Route("Investigation")]
    [ApiController]
    public class InvestigationController : ControllerBase
    {
        private readonly InvestigationService investigationService;

        public InvestigationController(InvestigationService investigationService)
        {
            this.investigationService = investigationService;
        }

		[HttpGet("GetBodyParts")]
		[SwaggerOperation(Summary = "Get body Parts", Description = "Returns all body parts")]
		[SwaggerResponse(200, "List successfully retrieved")]
		public async Task<IActionResult> GetBodyPart()
		{
			try
			{
				return Ok(await investigationService.GetBodyPart());
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}	


		[HttpGet("GetConditionsWeather")]
		[SwaggerOperation(Summary = "Get conditions weather", Description = "Returns all conditions weather")]
		[SwaggerResponse(200, "List successfully retrieved")]
		public async Task<IActionResult> GetConditionsWeather()
		{
			try
			{
				return Ok(await investigationService.GetConditionsWeather());
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("GetConditionsRoad")]
		[SwaggerOperation(Summary = "Get conditions road", Description = "Returns all conditions road")]
		[SwaggerResponse(200, "List successfully retrieved")]
		public async Task<IActionResult> GetConditionsRoad()
		{
			try
			{
				return Ok(await investigationService.GetConditionsRoad());
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

        [HttpGet("GetRegionBrazil")]
        [SwaggerOperation(Summary = "Get Region of Brazil", Description = "Returns all Region of Brazil")]
        [SwaggerResponse(200, "List successfully retrieved")]
        public IActionResult GetRegionBrazil()
        {
            try
            {
                return Ok(investigationService.GetRegionBrazil());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetVehicleType")]
		[SwaggerOperation(Summary = "Get vehicle type", Description = "Returns all vehicle type")]
		[SwaggerResponse(200, "List successfully retrieved")]
		public async Task<IActionResult> GetVehicleType()
		{
			try
			{
				return Ok(await investigationService.GetVehicleType());
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost("InvestigationEdition")]
		[SwaggerOperation(Summary = "Investigation Edition", Description = "Edition of Investigation in the system")]
		[SwaggerResponse(200, "Component succesfully retrieved")]
		public async Task<IActionResult> OccurrenceEdition([FromBody] CommandInvestigationTask InvestigationData)
		{
			try
			{
				return Ok(await investigationService.InvestigationEdition(InvestigationData));
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}

		[HttpGet("GetInvestigation")]
		[SwaggerOperation(Summary = "Get Investigation", Description = "Returns Investigation from occurrence")]
		[SwaggerResponse(200, "List successfully retrieved")]
		public async Task<IActionResult> GetInvestigation(int eventId)
		{
			try
			{
				return Ok(await investigationService.GetInvestigationTasks(eventId));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}	
	}
}
