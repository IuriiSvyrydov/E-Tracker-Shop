using E_Tracker.Application.Abstractions.Token;
using E_Tracker.Application.DTOs;
using E_Tracker.Application.DTOs.Facebook;
using MediatR;
using Microsoft.AspNetCore.Identity;
using IHttpClientFactory = System.Net.Http.IHttpClientFactory;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace E_Tracker.Application.Features.Commands.AppUser.FaceBookLogin;

public class FaceBookLoginCommandHandler: IRequestHandler<FaceBookLoginCommandRequest,FaceBookLoginCommandResponse>
{
    private readonly UserManager<Domain.Identity.AppUser> _userManager;
    private readonly ITokenHandler _tokenHandler;
    private readonly HttpClient _httpClient;

    public FaceBookLoginCommandHandler(UserManager<Domain.Identity.AppUser> userManager, ITokenHandler tokenHandler, 
        IHttpClientFactory httpClient)
    {
        _userManager = userManager;
        _tokenHandler = tokenHandler;
        _httpClient = httpClient.CreateClient();
    }

    public async Task<FaceBookLoginCommandResponse> Handle(FaceBookLoginCommandRequest request, CancellationToken cancellationToken)
    {
        string accessTokenResponse = await _httpClient.
            GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id=784148045978382&client_secret=4bf0d58dc8e59fa645061f1e0f7ce5ab&grant_type=client_credentials");
        FaceBookTokenResponseDto faceBookTokenResponseDto =
            JsonSerializer.Deserialize<FaceBookTokenResponseDto>(accessTokenResponse);
        string userAccessTokenValidation = await _httpClient.GetStringAsync(
            $"https://graph.facebook.com/debug_token?input_token={request.AuthToken}&access_token={faceBookTokenResponseDto.AccessToken}");
        FaceBookAccessTokenValidation validation =
            JsonSerializer.Deserialize<FaceBookAccessTokenValidation>(userAccessTokenValidation);
        if (validation.Data.IsValid)
        {
            string userInfoResponse =
                await _httpClient.GetStringAsync(
                    $"https://graph.facebook.com/me?fields=email,name&access_token={request.AuthToken}");
            FacebookUserInfoResponse userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);


            var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");
            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            bool result = user != null;
            if (user == null)
                user = await _userManager.FindByEmailAsync(userInfo.Email);
            if (user == null)
                user = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = userInfo.Email,
                    UserName = userInfo.Email,
                    SureName = userInfo.Name
                };
            var identityResult = await _userManager.CreateAsync(user);
            result = identityResult.Succeeded;
            if (result)
            {
                await _userManager.AddLoginAsync(user, info);


                TokenDto token = _tokenHandler.CreateAccessToken(5);
                return new()
                {
                    TokenDto = token
                };
            }
        }
        throw new Exception("Invalid external authentication");
    }
}