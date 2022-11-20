using E_Tracker.Application.DTOs;
using E_Tracker.Domain.Identity;

namespace E_Tracker.Application.Abstractions.Token;

public interface ITokenHandler
{
    DTOs.TokenDto CreateAccessToken(int second,AppUser user);
    string CreateRefreshToken();
}