using E_Tracker.Application.DTOs;

namespace E_Tracker.Application.Abstractions.Services.AuthenticationService;

public interface IInternalAuthentication
{
    Task<TokenDto> LoginAsync(string userNameOrEmail, string password, int accessTokenLifetime);
    Task<TokenDto> RefreshTokenLoginAsync(string refreshToken);
}