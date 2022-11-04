using MediatR;

namespace E_Tracker.Application.Features.Commands.Login;

public class LoginCommandRequest: IRequest<LoginCommandResponse>
{
    public string UserNameOrEmail { get; set; }
    public string Password { get; set; }
}