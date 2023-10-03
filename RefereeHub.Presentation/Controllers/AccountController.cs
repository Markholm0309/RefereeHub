using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RefereeHub.Domain.Helpers;
using RefereeHub.Domain.Interfaces.Services;
using RefereeHub.Domain.ViewModels;

namespace RefereeHub.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _service;

    public AccountController(IAccountService service)
    {
        _service = service;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(AuthenticateRequest request)
    {
        return await _service.Register(request);
    }
    
    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate(AuthenticateRequest request)
    {
        return await _service.Authenticate(request);
    }
    
    [Authorize(Roles = Roles.User)]
    [HttpGet("test")]
    public async Task<IActionResult> AuthTest()
    {
        return Ok("content");
    }
}