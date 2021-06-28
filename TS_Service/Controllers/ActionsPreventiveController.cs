using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TS.Domain.Entities.ActionsPreventive;
using TS.Service.ActionsPreventive;

namespace TS_Api.Controllers
{
    [Route("ActionsPreventive")]
    [ApiController]
    public class ActionsPreventiveController : ControllerBase
    {
        private readonly ActionsPreventiveService actionsPreventiveService;
        public ActionsPreventiveController(ActionsPreventiveService actionsPreventiveService)
        {
            this.actionsPreventiveService = actionsPreventiveService;
        }

		[HttpGet("GetActionsPreventiveActionSubject")]
		[SwaggerOperation(Summary = "Get preventive action subject", Description = "Returns all preventive action subject")]
		[SwaggerResponse(200, "List successfully retrieved")]
		public async Task<IActionResult> GetActionsPreventiveActionSubject()
		{
			try
			{
				return Ok(await actionsPreventiveService.GetActionsPreventiveActionSubject());
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		[HttpGet("GetActionsPreventiveAction")]
		[SwaggerOperation(Summary = "Get preventive action", Description = "Returns all preventive action")]
		[SwaggerResponse(200, "List successfully retrieved")]
		public async Task<IActionResult> GetActionsPreventiveAction(int eventId)
		{
			try
			{
				return Ok(await actionsPreventiveService.GetActionsPreventiveAction(eventId));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		[HttpPost("IncludeActionsPreventiveAction")]
		[SwaggerOperation(Summary = "Include Actions Preventive ", Description = "Include actions preventive action")]
		[SwaggerResponse(200, "Component succesfully retrieved")]
		public async Task<IActionResult> IncludeActionsPreventiveAction(List<ActionsPreventiveActionEntity> actionsPreventiveAction)
		{
			try
			{
				return Ok(await actionsPreventiveService.IncludeActionsPreventiveAction(actionsPreventiveAction));
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}
	}
}
