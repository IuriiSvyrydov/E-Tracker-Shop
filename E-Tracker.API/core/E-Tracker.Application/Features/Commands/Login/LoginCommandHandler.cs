
using E_Tracker.Application.Abstractions.Services;
using MediatR;

namespace E_Tracker.Application.Features.Commands.Login;

public class LoginCommandHandler: IRequestHandler<LoginCommandRequest,LoginCommandResponse>
{

    private readonly IAuthService _authService;

    public LoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<LoginCommandResponse> Handle(LoginCommandRequest request,
        CancellationToken cancellationToken)
    {

        var token = await _authService.LoginAsync(request.UserNameOrEmail, request.Password,15);
        return new LoginCommandSuccessResponse()
        {
            TokenDto = token
        };

    }

}