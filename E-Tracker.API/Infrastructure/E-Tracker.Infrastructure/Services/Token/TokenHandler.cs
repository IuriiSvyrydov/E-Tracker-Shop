using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using E_Tracker.Application.Abstractions.Token;
using E_Tracker.Application.DTOs;
using E_Tracker.Domain.Identity;
using Microsoft.IdentityModel.Tokens;

namespace E_Tracker.Infrastructure.Services.Token;

public class TokenHandler: ITokenHandler
{
    private readonly IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public TokenDto CreateAccessToken(int second ,AppUser user)
    {
        TokenDto token = new();
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
        SigningCredentials signingCredentials = new(key, SecurityAlgorithms.HmacSha256);
        token.Experation = DateTime.UtcNow.AddSeconds(second);
        JwtSecurityToken securityToken = new(

            audience: _configuration["Token:Audience"],
               issuer: _configuration["Token:Issuer"],
            expires: token.Experation,
            notBefore: DateTime.UtcNow,
            signingCredentials: signingCredentials

        );
        JwtSecurityTokenHandler tokenHandler = new();
        token.AccessToken = tokenHandler.WriteToken(securityToken);
        token.RefreshToken = CreateRefreshToken();
         
        return token;
    }

    public string CreateRefreshToken()
    {
        byte[] number = new byte[32];
        using RandomNumberGenerator random = RandomNumberGenerator.Create();
        random.GetBytes(number);
        return Convert.ToBase64String(number);


    }
}