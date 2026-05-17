using Application.Interfaces;
using Application.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ICustomAuthenticationService _customAuthenticationService;

        public AuthenticationController(IConfiguration config, ICustomAuthenticationService customAuthenticationService)
        {
            _config = config;
            _customAuthenticationService = customAuthenticationService;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<string>> AuthenticateAsync(UserLoginRequest authenticationRequest)
        {
            var token = await _customAuthenticationService.AuthenticateAsync(authenticationRequest);

            if (token == null)
            {
                return Unauthorized(new { message = "Usuario o contraseña incorrecta" });
            }

            return Ok(token);
        }

    }
}