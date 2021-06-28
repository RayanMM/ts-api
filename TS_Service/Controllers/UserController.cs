using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TS.Service;
using TS.Service.Exceptions;

namespace TS_Api.Controllers
{
    [Route("User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpGet("signin")]
        [SwaggerOperation(Summary = "Authenticates", Description = "Authenticates the user into the system")]
        [SwaggerResponse(200, "User authenticated succesfully")]
        public async Task<IActionResult> Signin(string userName, string password)
        {
            try
            {
                var authToken = await userService.SignInAsync(userName, password);
                return Ok(authToken);
            }
            catch(UserNotFoundException)
            {
                return BadRequest(new UserNotFoundDetails());
            }
        }
    }
}