using E_Tracker.Application.Exceptions.UserException;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace E_Tracker.Application.Features.Commands.AppUser.CreateUser;

public class CreateUserCommandHandler: IRequestHandler<CreateUserCommandRequest,CreateUserCommandResponse>
{
    private readonly UserManager<Domain.Identity.AppUser> _userManager;

    public CreateUserCommandHandler(UserManager<Domain.Identity.AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    { 
        var result = await _userManager.CreateAsync(new()
        {
            Id = Guid.NewGuid().ToString(),
            SureName = request.NameSurName,
            UserName = request.UserName,
            Email = request.Email,

        }, request.Password);
        CreateUserCommandResponse response = new(){Successed = result.Succeeded};
        if (result.Succeeded)

            return new()
            {
                Successed = true,
                Message = "User was Created successfully"
            };
        //   throw new UserCreatedFailException();
        else
            foreach (var error in result.Errors)
                response.Message += $"{error.Code}-{error.Description}<br>";
        return response;



    }
}