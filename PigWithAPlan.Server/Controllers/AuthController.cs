using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using PigWithAPlan.Server.Models;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] ILogin model)
    {
        var token = await _authService.Login(model.Username, model.Password);
        if (token != null)
        {
            Response.Cookies.Append("token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            return Ok(new { token });
        }

        return Unauthorized();
    }

    [HttpGet("GoogleLogin")]
    public IActionResult GoogleLogin()
    {
        var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet("GoogleResponse")]
    public async Task<IActionResult> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
        if (!result.Succeeded)
            return BadRequest();

        var token = await _authService.GoogleLoginAsync(result.Principal);
        if (token == null)
            return BadRequest();

        return Ok(new { token });
    }

    [HttpGet("CheckToken")]
    public async Task<IActionResult> CheckToken()
    {
        if (Request.Cookies.TryGetValue("token", out string? token))
        {
            var result = await _authService.CheckTokenAsync(token);
            if (result.IsValid)
            {
                return Ok(true);
            }
            return Ok(false);
        }

        return Ok(false);
    }



    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] IRegister model)
    {
        var result = await _authService.RegisterAsync(model.Username, model.Password);
        if (result)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpPost("Logout")]
    public IActionResult Logout()
    {
        if (Request.Cookies.ContainsKey("token"))
        {
            Response.Cookies.Delete("token");
        }

        return Ok();
    }

}
