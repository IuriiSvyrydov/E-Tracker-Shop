using E_Tracker.Application.DTOs.User;
using E_Tracker.Domain.Identity;

namespace E_Tracker.Application.Abstractions.Services;

public interface IUserService
{
    Task<CreateUserResponse> CreateAsync(CreateUser model);
    Task UpdateRefreshToken(string refreshToken,AppUser user, DateTime accessTokenDate,
        int refreshTokenLifeTime);
}