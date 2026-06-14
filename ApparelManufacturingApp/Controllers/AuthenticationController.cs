using Application.Interfaces;
using Application.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(
        IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        UserLoginRequestDTO request)
    {
        var result =
            await _authenticationService.Login(request);

        if (result == null)
        {
            return Unauthorized(
                new
                {
                    Message = "Invalid credentials"
                });
        }

        return Ok(result);
    }
}