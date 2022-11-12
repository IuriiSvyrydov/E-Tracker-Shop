using System.Text.Json;
using E_Tracker.Application.Abstractions.Services;
using E_Tracker.Application.Abstractions.Token;
using E_Tracker.Application.DTOs;
using E_Tracker.Application.DTOs.Facebook;
using E_Tracker.Application.Features.Commands.AppUser.FaceBookLogin;
using Google.Apis.Auth;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace E_Tracker.Persistence.Services;

public class AuthService: IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenHandler _tokenHandler;

    public AuthService(IHttpClientFactory httpClient, IConfiguration configuration, UserManager<AppUser> userManager, ITokenHandler tokenHandler)
    {
        _configuration = configuration;
        _userManager = userManager;
        _tokenHandler = tokenHandler;
        _httpClient = httpClient.CreateClient();
    }

    public async Task<TokenDto> FaceBookLoginAsync(string authToken, int accessTokenLifeTime)
    {
        string accessTokenResponse = await _httpClient.
            GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_configuration["ExternalLoginSettings:Facebook:ClientId"]}&clientsecret={_configuration["ExternalLoginSettings:Facebook:ClientSecret"]}&grant_type=client_credentials");
        FaceBookTokenResponseDto? faceBookTokenResponseDto =
            JsonSerializer.Deserialize<FaceBookTokenResponseDto>(accessTokenResponse);
        string userAccessTokenValidation = await _httpClient.GetStringAsync(
            $"https://graph.facebook.com/debug_token?input_token={authToken}&access_token={faceBookTokenResponseDto?.AccessToken}");
        FaceBookAccessTokenValidation? validation =
            JsonSerializer.Deserialize<FaceBookAccessTokenValidation>(userAccessTokenValidation);
        if (validation?.Data.IsValid !=null)
        {
            string userInfoResponse =
            await _httpClient.GetStringAsync(
                    $"https://graph.facebook.com/me?fields=email,name&access_token={authToken}");
            FacebookUserInfoResponse? userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);


            var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");
            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            bool result = user != null;
            if (user == null)
                user = await _userManager.FindByEmailAsync(userInfo?.Email);
            if (user == null)
                user = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = userInfo?.Email,
                    UserName = userInfo?.Email,
                    SureName = userInfo?.Name
                };
            var identityResult = await _userManager.CreateAsync(user);
            result = identityResult.Succeeded;
            if (result)
            {
                await _userManager.AddLoginAsync(user, info);

                TokenDto token = _tokenHandler.CreateAccessToken(accessTokenLifeTime);
                return token;
            }

        }
        throw new Exception("Invalid external authentication");
    }
    public async Task<TokenDto> GoogleLoginAsync(string idToken, int accessTokenLifeTime)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = new List<string> { _configuration["Google:APP_ID"] }
        };
        var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
        var info = new UserLoginInfo("Google", payload.Subject, "Google");
        var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
        bool result = user != null;
        if (user == null)
            user = await _userManager.FindByEmailAsync(payload.Email);
        if (user == null)
            user = new()
            {
                Id = Guid.NewGuid().ToString(),
                Email = payload.Email,
                UserName = payload.Email,
                SureName = payload.Name
            };
        var identityResult = await _userManager.CreateAsync(user);
        result = identityResult.Succeeded;
        if (result)
            await _userManager.AddLoginAsync(user, info);
        else
            throw new Exception("Invalid external authentication");
        TokenDto token = _tokenHandler.CreateAccessToken(accessTokenLifeTime);
        return token;
    }

    public async Task TwitterLoginAsync()
    {
        throw new NotImplementedException();
    }
    public async Task LoginAsync()
    {
        throw new NotImplementedException();
    }
}