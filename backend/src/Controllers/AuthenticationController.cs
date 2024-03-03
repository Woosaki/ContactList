using ContactListAPI.Dtos;
using ContactListAPI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactListAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthenticationController(AuthService authService) : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var principal = authService.Login(request);
        if (principal == null)
        {
            return Unauthorized();
        }

        await HttpContext.SignInAsync(principal);

        return Ok();
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult Register([FromBody] RegisterRequest request)
    {
        if (authService.Register(request) == false)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();

        return Ok();
    }
}
