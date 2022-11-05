using E_Tracker.Application.DTOs;

namespace E_Tracker.Application.Features.Commands.Login;

public class LoginCommandResponse
{

}

public class LoginCommandSuccessResponse : LoginCommandResponse
{
    public TokenDTO TokenDto { get; set; }

}

public class LoginCommandErrorResponse : LoginCommandResponse
{
    public String Message { get; set; }
}