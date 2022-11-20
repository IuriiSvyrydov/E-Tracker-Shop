using MediatR;

namespace E_Tracker.Application.Features.Commands.AppUser.RefreshTokenLogin
{
    public class RefreshTokenCommandRequest: IRequest<RefreshTokenCommandResponse>
    {
        public string RefreshToken { get; set; }
    }
}
