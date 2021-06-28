using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TS.Service.Menus;

namespace TS_Api.Controllers
{
    [Route("Menu")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly MenusService service;

        public MenusController(MenusService service)
        {
            this.service = service;
        }

        [HttpGet("Menus")]
        [SwaggerOperation(Summary = "Get menu", Description = "Gets menu list according to the specified group")]
        [SwaggerResponse(200, "Menu list retrieved successfully")]
        public async Task<IActionResult> GetMenus(int menuGroup)
        {
            try
            {
                string authHeader = HttpContext.Request.Headers["Authorization"].ToString().Split(" ").LastOrDefault();

                return Ok(await service.GetMenus(menuGroup, authHeader));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("SubMenus")]
        [SwaggerOperation(Summary = "Get submenu", Description = "Gets submenu list according to the specified menu id")]
        [SwaggerResponse(200, "SubMenu list retrieved successfully")]
        public async Task<IActionResult> GetSubMenus(int menuId)
        {
            try
            {
                return Ok(await service.GetSubMenus(menuId));
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}