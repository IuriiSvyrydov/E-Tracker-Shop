using E_Tracker.Application.Abstractions.Services;
using E_Tracker.Application.DTOs.User;
using E_Tracker.Application.Exceptions;

namespace E_Tracker.Persistence.Services;

public class UserService: IUserService
{
    private readonly UserManager<AppUser> _userManager;

    public UserService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<CreateUserResponse> CreateAsync(CreateUser model)
    {
        var result = await _userManager.CreateAsync(new()
        {
            Id = Guid.NewGuid().ToString(),
            SureName = model.NameSurName,
            UserName = model.UserName,
            Email = model.Email,

        }, model.Password);
        CreateUserResponse response = new() { Successeded = result.Succeeded };
        if (result.Succeeded)
           response.Message = "User was Created successfully";
            
        //   throw new UserCreatedFailException();
        else
            foreach (var error in result.Errors)
                response.Message += $"{error.Code}-{error.Description}";
        return response;
    }

    public async Task UpdateRefreshToken(string refreshToken, AppUser user,DateTime accessTokenDate, int refreshTokenLifeTime)
    {
        if (user!=null)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenEndDate = accessTokenDate.AddSeconds(refreshTokenLifeTime);
            await _userManager.UpdateAsync(user);

        }
  
        
        //else{
        //    throw new NotFoundUserException();
        //}
    }
}