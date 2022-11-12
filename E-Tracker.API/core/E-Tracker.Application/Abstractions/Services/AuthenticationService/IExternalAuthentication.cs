using E_Tracker.Application.DTOs;

namespace E_Tracker.Application.Abstractions.Services.AuthenticationService;

public interface IExternalAuthentication
{
    Task<TokenDto> FaceBookLoginAsync(string authToken,int accessTokenLifeTime);
    Task<TokenDto> GoogleLoginAsync(string idToken, int accessTokenLifeTime);
  //  Task TwitterLoginAsync();


}