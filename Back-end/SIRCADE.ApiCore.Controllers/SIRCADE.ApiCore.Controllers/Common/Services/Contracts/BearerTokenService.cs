using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SIRCADE.ApiCore.Controllers.Accounts.Responses;
using SIRCADE.ApiCore.Controllers.Common.Services.Interfaces;
using SIRCADE.ApiCore.Models.Users.Entities;

namespace SIRCADE.ApiCore.Controllers.Common.Services.Contracts;

public class BearerTokenService(IConfiguration configuration) : IBearerTokenService
{
    public AccountInfoResponse Generate(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Role, user.Role.Name),
            new("RoleId", user.RoleId.ToString()),
            new(ClaimTypes.Name, user.GetFullName()),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Detail.Email)
        };


        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JWT:Key")!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expirationDate = DateTime.UtcNow.AddHours(configuration.GetValue<int>("JWT:ExpirationInHours"));

        var issuer = configuration.GetValue<string>("JWT:Issuer");

        var audience = configuration.GetValue<string>("JWT:Audience");

        var securityToken = new JwtSecurityToken(issuer: issuer, audience: audience, claims: claims,
                                                 expires: expirationDate, signingCredentials: credentials);

        return new(new JwtSecurityTokenHandler().WriteToken(securityToken), expirationDate);

    }
}