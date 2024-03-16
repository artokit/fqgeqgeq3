using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using WebApplication9.Common;
using WebApplication9.DTO;
using WebApplication9.Models;


namespace WebApplication9.Services;

public class AuthService
{
    private readonly UserService _userService;

    public AuthService(UserService userService)
    {
        _userService = userService;
    }

    public async Task<AuthResponseDto?> Register(RegisterDto user)
    {
        if (await _userService.GetUserByUsername(user.username) != null)
        {
            return null;
        }

        var userAdded= await _userService.AddUser(user);
        return userAdded is null ? null : new AuthResponseDto {AccessToken = GenerateAccessToken(userAdded.Id)};
    }

    public async Task<AuthResponseDto?> AuthByLoginPassword(string username, string password)
    {
        var user = await _userService.GetUserByUsername(username);
        if (user is null || user.Password != Common.Common.HashPassword(password))
        {
            return null;
        }
        return new AuthResponseDto {AccessToken = GenerateAccessToken(user.Id)};
    }
    
    public async Task<AuthResponseDto?> AuthByAccessToken(string accessToken)
    {
        int id = accessToken.GetUserId();
        if (id == default)
        {
            return null;
        }

        var user = await _userService.GetUserById(id);
        return user is null ? null : new AuthResponseDto {AccessToken = GenerateAccessToken(user.Id)};
    }
    
    private static string GenerateAccessToken(int id)
    {
        var claims = GetClaims(id);
        return GenerateJwtToken(claims, AuthOptions.LifeTimeAccessToken);
    }

    private static string GenerateJwtToken(List<Claim> claims, int timeExpire)
    {
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.Issuer,
            audience: AuthOptions.Audience,
            claims: claims,
            expires: DateTime.Now.Add(TimeSpan.FromMinutes(timeExpire)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
    
    private static List<Claim> GetClaims(int userId)
    {
        var claims = new List<Claim> { new(ClaimType.Id.ToString(), userId.ToString()) };
        return claims;
    }
}
