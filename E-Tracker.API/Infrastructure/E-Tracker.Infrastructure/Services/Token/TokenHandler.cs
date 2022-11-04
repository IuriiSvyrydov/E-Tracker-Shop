using System.IdentityModel.Tokens.Jwt;
using System.Text;
using E_Tracker.Application.Abstractions.Token;
using E_Tracker.Application.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace E_Tracker.Infrastructure.Services.Token;

public class TokenHandler: ITokenHandler
{
    private readonly IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public TokenDTO CreateAccessToken(int minute)
    {
        TokenDTO token = new();
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
        SigningCredentials signingCredentials = new(key, SecurityAlgorithms.HmacSha256);
        token.Experation = DateTime.UtcNow.AddMinutes(minute);
        JwtSecurityToken securityToken = new(

            audience: _configuration["Token:Audience"],
            issuer: _configuration["Token:Issuer"],
            expires: token.Experation,
            notBefore: DateTime.UtcNow,
            signingCredentials: signingCredentials

        );
        JwtSecurityTokenHandler tokenHandler = new();
        token.AccessToken = tokenHandler.WriteToken(securityToken);
        return token;
        
        return null;
    }
}