using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApparelManufacturingApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class temp : ControllerBase
{
    private readonly IPasswordService _passwordService;

    public temp(IPasswordService passwordService)
    {
        _passwordService = passwordService;
    }
    // GET: api/<temp>
    [HttpGet]
    public string temphash()
    {
        return _passwordService.HashPassword("1234");
    }
}
