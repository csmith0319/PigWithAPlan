using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Configuration;
using PigWithAPlan.Server.Data;
using PigWithAPlan.Server.Models;

public interface IAuthService
{
    Task<string?> Login(User user);
    Task<string> GoogleLoginAsync(ClaimsPrincipal principal);
    string GenerateJwtToken(string username);
    Task<bool> RegisterAsync(User user);
    Task<(bool IsValid, string? UserId)> CheckTokenAsync(string token);

}


public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;
    private readonly IUserService _userService;

    public AuthService(IConfiguration configuration, ApplicationDbContext context, IUserService userService)
    {
        _configuration = configuration;
        _context = context;
        _userService = userService;
    }

    public async Task<string?> Login(User user)
    {
        var isValidUser = await _userService.ValidateUser(user);
        if (isValidUser)
        {
            return GenerateJwtToken(user.Username);
        }

        return null;
    }

    public async Task<string> GoogleLoginAsync(ClaimsPrincipal principal)
    {
        var username = principal.Identity?.Name;

        if (username == null)
        {
            return string.Empty;
        }

        var token = GenerateJwtToken(username);
        return await Task.FromResult(token);
    }

    public async Task<bool> RegisterAsync(User user)
    {
        return await _userService.RegisterAsync(user);
    }

    public async Task<(bool IsValid, string? UserId)> CheckTokenAsync(string token)
    {
        try
        {
            var jwtKey = _configuration["JWT_KEY"] ?? throw new ArgumentNullException("JWT_KEY", "JWT Key must be provided in the configuration.");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtKey);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero,
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = jwtToken.Claims.First(x => x.Type == "sub").Value;

            return await Task.FromResult((true, userId));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return (false, null);
        }
    }

    public string GenerateJwtToken(string username)
    {
        var jwtKey = _configuration["JWT_KEY"] ?? throw new ArgumentNullException("JWT_KEY", "JWT Key must be provided in the configuration.");
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username)
            },
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenString;
    }
}
