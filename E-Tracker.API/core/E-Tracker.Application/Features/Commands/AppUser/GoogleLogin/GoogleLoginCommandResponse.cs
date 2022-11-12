using E_Tracker.Application.DTOs;

namespace E_Tracker.Application.Features.Commands.AppUser.GoogleLogin;

public class GoogleLoginCommandResponse
{
    public TokenDto Token { get; set; }
}