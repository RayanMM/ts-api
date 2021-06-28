using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;
using TS.Domain.Entities.Occurrence;
using TS.Service.Occurrence;

namespace TS_Api.Controllers
{
    [Route("Occurrence")]
	[ApiController]
	public class OccurrenceController : ControllerBase
	{
		private readonly OccurrenceService occurrenceService;

		public OccurrenceController(OccurrenceService occurrenceService)
		{
			this.occurrenceService = occurrenceService;
		}

		[HttpGet("GetClassification")]
		[SwaggerOperation(Summary = "Get occurrences classification", Description = "Returns all classifications from occurrence classification")]
		[SwaggerResponse(200, "List successfully retrieved")]
		public async Task<IActionResult> GetClassification()
		{
			try
			{
				return Ok(await occurrenceService.GetClassification());
			}
			catch(Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("GetType")]
		[SwaggerOperation(Summary = "Get occurrences type", Description = "Returns all type from occurrence")]
		[SwaggerResponse(200, "List successfully retrieved")]
		public async Task<IActionResult> GetOccurrenceType()
		{
			try
			{
				return Ok(await occurrenceService.GetOccurrenceType());
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("GetJob")]
		[SwaggerOperation(Summary = "Get job", Description = "Returns all job from occurrence")]
		[SwaggerResponse(200, "List successfully retrieved")]
		public async Task<IActionResult> GetJob()
		{
			try
			{
				return Ok(await occurrenceService.GetOccurrenceJob());
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("GetFacilities")]
		[SwaggerOperation(Summary = "Get Facilities", Description = "Returns all Facilities from occurrence")]
		[SwaggerResponse(200, "List successfully retrieved")]
		public async Task<IActionResult> GetFacilities()
		{
			try
			{
				return Ok(await occurrenceService.GetOccurrenceFacilities());
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("GetDepartament")]
		[SwaggerOperation(Summary = "Get Departament", Description = "Returns all departaments from occurrence")]
		[SwaggerResponse(200, "List successfully retrieved")]
		public async Task<IActionResult> GetDepartament()
		{
			try
			{
				return Ok(await occurrenceService.GetOccurrenceDepartament());
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("GetContractType")]
		[SwaggerOperation(Summary = "Get Contract Type", Description = "Returns all contract type from occurrence")]
		[SwaggerResponse(200, "List successfully retrieved")]
		public async Task<IActionResult> GetContractType()
		{
			try
			{
				return Ok(await occurrenceService.GetOccurrenceContractType());
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("GetOutSourcedCompanies")]
		[SwaggerOperation(Summary = "Get OutSourced Companies", Description = "Returns all outSourced companies from occurrence")]
		[SwaggerResponse(200, "List successfully retrieved")]
		public async Task<IActionResult> GetOccurrenceOutSourcedCompanies()
		{
			try
			{
				return Ok(await occurrenceService.GetOccurrenceOutSourcedCompanies());
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("GetHappened")]
		[SwaggerOperation(Summary = "Get Happened", Description = "Returns all happened from occurrence")]
		[SwaggerResponse(200, "List successfully retrieved")]
		public async Task<IActionResult> GetOccurrenceHappened(int happenedGroupId)
		{
			try
			{
				return Ok(await occurrenceService.GetOccurrenceHappened(happenedGroupId));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("GetHappenedGroup")]
		[SwaggerOperation(Summary = "Get Happened Group", Description = "Returns all happened group from occurrence")]
		[SwaggerResponse(200, "List successfully retrieved")]
		public async Task<IActionResult> GetOccurrenceHappenedGroup()
		{
			try
			{
				return Ok(await occurrenceService.GetOccurrenceHappenedGroup());
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost("OccurrenceEdition")]
		[SwaggerOperation(Summary = "Occurrence Edition", Description = "Edition of occurrence in the system")]
		[SwaggerResponse(200, "Component succesfully retrieved")]
		public async Task<IActionResult> OccurrenceEdition([FromBody] OccurrenceEventEntity OccurrenceData)
		{
			try
			{
				return Ok(await occurrenceService.OccurrenceEdition(OccurrenceData));
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}

		[HttpPost("OccurrenceInclude")]
		[SwaggerOperation(Summary = "Occurrence Include", Description = "Include of occurrence in the system")]
		[SwaggerResponse(200, "Component succesfully retrieved")]
		public async Task<IActionResult> OccurrenceInclude([FromBody] OccurrenceEventEntity OccurrenceData)
		{
			try
			{
				return Ok(await occurrenceService.OccurrenceInclude(OccurrenceData));
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}

		[HttpGet("GetOccurrenceEvent")]
		[SwaggerOperation(Summary = "Get occurrence events", Description = "Returns all occurrence events")]
		[SwaggerResponse(200, "List successfully retrieved")]
		public async Task<IActionResult> GetOccurrenceEvent()
		{
			try
			{
				return Ok(await occurrenceService.GetOccurrenceEvent());
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("GetOccurrenceEventById")]
		[SwaggerOperation(Summary = "Get occurrence event by id", Description = "Return occurrence event by id")]
		[SwaggerResponse(200, "List successfully retrieved")]
		public async Task<IActionResult> GetOccurrenceEventById(int eventId)
		{
			try
			{
				return Ok(await occurrenceService.GetOccurrenceEventById(eventId));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}	
	}
}
