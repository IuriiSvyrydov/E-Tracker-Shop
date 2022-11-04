using E_Tracker.Application.Abstractions.Token;
using E_Tracker.Application.DTOs;
using E_Tracker.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace E_Tracker.Application.Features.Commands.Login;

public class LoginCommandHandler: IRequestHandler<LoginCommandRequest,LoginCommandResponse>
{
    private readonly UserManager<Domain.Identity.AppUser> _userManager;
    private readonly SignInManager<Domain.Identity.AppUser> _signInManager;
    private readonly ITokenHandler _tokenHandler;
    public LoginCommandHandler(UserManager<Domain.Identity.AppUser> userManager, SignInManager<Domain.Identity.AppUser> signInManager, ITokenHandler tokenHandler)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHandler = tokenHandler;
    }

    public async Task<LoginCommandResponse> Handle(LoginCommandRequest request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserNameOrEmail);
        if (user == null)
            user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);
        if (user == null)
            throw new NotFoundUserException("User not found");
        SignInResult  result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
      if (result.Succeeded)
      {
          TokenDTO tokenDto =  _tokenHandler.CreateAccessToken(5);
      }

      return new();
    }

}