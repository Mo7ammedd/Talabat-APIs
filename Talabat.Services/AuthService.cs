using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Talabat.Core.Models.Identity;
using Talabat.Core.Services.Contract;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Talabat.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _config;

    public AuthService(IConfiguration config)
    {
        _config = config;
    }
    public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
    {
        var authClaims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.NameId, user.Id),
        };
        var userRoles = await userManager.GetRolesAsync(user);
        foreach (var role in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecretKey"]));
        var token = new JwtSecurityToken(
            issuer: _config["JWT:ValidIssuer"],
            audience: _config["JWT:ValidAudience"],
            expires: DateTime.UtcNow.AddDays(double.Parse(_config["JWT:TokenLifeTime"])),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}