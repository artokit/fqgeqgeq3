using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApplication9.DTO;
using WebApplication9.Models;
using WebApplication9.Services;

namespace WebApplication9.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthModelDto authModelDto)
    {
        if (authModelDto.Login.IsNullOrEmpty() || authModelDto.Password.IsNullOrEmpty())
        {
            return BadRequest();
        }

        var res = await _authService.AuthByLoginPassword(authModelDto.Login, authModelDto.Password);
        if (res is null)
        {
            return BadRequest();
        }

        return Ok(res);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto user)
    {
        if (user.username.IsNullOrEmpty() || user.password.IsNullOrEmpty())
        {
            return BadRequest();
        }

        var res = await _authService.Register(user);
        if (res is null)
        {
            return BadRequest();
        }

        return Ok(res);
    }

}