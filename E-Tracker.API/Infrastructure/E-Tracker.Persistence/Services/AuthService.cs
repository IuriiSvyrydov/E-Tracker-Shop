using System.Security.Authentication;
using System.Text.Json;
using E_Tracker.Application.Abstractions.Services;
using E_Tracker.Application.Abstractions.Token;
using E_Tracker.Application.DTOs;
using E_Tracker.Application.DTOs.Facebook;
using E_Tracker.Application.Exceptions;
using E_Tracker.Application.Features.Commands.AppUser.FaceBookLogin;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace E_Tracker.Persistence.Services;

public class AuthService: IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenHandler _tokenHandler;
    private readonly IUserService _userService;
    private readonly SignInManager<AppUser> _signInManager;
    public AuthService(IHttpClientFactory httpClient, IConfiguration configuration, 
        UserManager<AppUser> userManager, ITokenHandler tokenHandler, 
        SignInManager<AppUser> signInManager, IUserService userService)
    {
        _configuration = configuration;
        _userManager = userManager;
        _tokenHandler = tokenHandler;
        _signInManager = signInManager;
        _userService = userService;
        _httpClient = httpClient.CreateClient();
    }

    public async Task<TokenDto> CreateExternalAsync(AppUser user,string name, string email, UserLoginInfo userInfo,int accessTokenLifeTime)
    {
        bool result = user != null;
        if (user == null)
            user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            user = new()
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,
                UserName = email,
                SureName = name
            };
        var identityResult = await _userManager.CreateAsync(user);
        result = identityResult.Succeeded;
        if (result)
        {
            await _userManager.AddLoginAsync(user, userInfo);

            TokenDto token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
            await _userService.UpdateRefreshToken(token.RefreshToken,user,token.Experation,5);

            return token;
        }
        throw new Exception("Invalid external authentication");
    }
    public async Task<TokenDto> FaceBookLoginAsync(string authToken, int accessTokenLifeTime)
    {
        string accessTokenResponse = await _httpClient.
            GetStringAsync
                ($"https://graph.facebook.com/oauth/access_token?client_id={_configuration["ExternalLoginSettings:Facebook:clientId"]}&client_secret={_configuration["ExternalLoginSettings:Facebook:clientSecret"]}&grant_type=client_credentials");
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

          return  await CreateExternalAsync(user, userInfo.Name, userInfo.Email, info, accessTokenLifeTime);

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
        return await CreateExternalAsync(user, payload.Name, payload.Email, info, accessTokenLifeTime);
    }
    public async Task<TokenDto> LoginAsync(string userNameOrEmail, string password,int accessTokenLifetime)
    {
        var user = await _userManager.FindByNameAsync(userNameOrEmail);
        if (user == null)
            user = await _userManager.FindByEmailAsync(userNameOrEmail);
        if (user == null)
            throw new NotFoundUserException("User not found");
        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (result.Succeeded)
        {
            TokenDto tokenDto = _tokenHandler.CreateAccessToken(accessTokenLifetime,user);
            await _userService.UpdateRefreshToken(tokenDto.RefreshToken, user, tokenDto.Experation, 5);

            return tokenDto;
        }

        //return new LoginCommandErrorResponse
        //{
        //    Message = "User Not Found"
        //};
        throw new AuthenticationException();

    }

    public async Task<TokenDto> RefreshTokenLoginAsync(string refreshToken)
    {
        AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => refreshToken == refreshToken);
        if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
        {
            TokenDto token = _tokenHandler.CreateAccessToken(15,user);
           await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Experation, 15);
            return token;

        }
        else
            throw new NotFoundUserException();

    }
}