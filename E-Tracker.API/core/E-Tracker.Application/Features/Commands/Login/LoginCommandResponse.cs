using E_Tracker.Application.DTOs;

namespace E_Tracker.Application.Features.Commands.Login;

public class LoginCommandResponse
{
    public TokenDTO TokenDto { get; set; }
}