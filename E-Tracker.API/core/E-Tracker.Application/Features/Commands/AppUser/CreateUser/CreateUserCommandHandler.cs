using E_Tracker.Application.Abstractions.Services;
using E_Tracker.Application.DTOs.User;
using E_Tracker.Application.Exceptions.UserException;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace E_Tracker.Application.Features.Commands.AppUser.CreateUser;

public class CreateUserCommandHandler: IRequestHandler<CreateUserCommandRequest,CreateUserCommandResponse>
{
    private readonly IUserService _userService;

    public CreateUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
       CreateUserResponse response = await _userService.CreateAsync(new()
        {
            Email = request.Email,
            NameSurName = request.NameSurName,
            UserName = request.UserName,
            Password = request.Password,
            PasswordConfirm = request.PasswordConfirm
        });
        return new()
        {
            Message = response.Message,
            Successed = response.Successeded
        };



    }
}