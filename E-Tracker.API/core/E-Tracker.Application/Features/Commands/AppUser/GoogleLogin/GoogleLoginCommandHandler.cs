using E_Tracker.Application.Abstractions.Token;
using E_Tracker.Application.DTOs;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace E_Tracker.Application.Features.Commands.AppUser.GoogleLogin;

public class GoogleLoginCommandHandler:IRequestHandler<GoogleLoginCommandRequest,GoogleLoginCommandResponse>
{
    private readonly UserManager<Domain.Identity.AppUser> _userManager;
    private readonly ITokenHandler _tokenHandler;

    public GoogleLoginCommandHandler(UserManager<Domain.Identity.AppUser> userManager, ITokenHandler tokenHandler)
    {
        _userManager = userManager;
        _tokenHandler = tokenHandler;
    }

    public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = new List<string> { "1013805387157-s0i6nndte0mm6sebb6anv4t1gc2la380.apps.googleusercontent.com" }
        };
       var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken,settings);
       var info = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);
      var user = await _userManager.FindByLoginAsync(info.LoginProvider,info.ProviderKey);
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
        var identityResult =  await _userManager.CreateAsync(user);
     result = identityResult.Succeeded;
     if (result)
         await _userManager.AddLoginAsync(user, info);
     else
         throw new Exception("Invalid external authentication");
     TokenDto token = _tokenHandler.CreateAccessToken(5);
     return new()
     {
         Token = token
     };

    }
}