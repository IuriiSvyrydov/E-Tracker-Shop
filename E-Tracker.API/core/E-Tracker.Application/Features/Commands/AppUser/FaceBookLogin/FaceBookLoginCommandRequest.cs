using MediatR;

namespace E_Tracker.Application.Features.Commands.AppUser.FaceBookLogin;

public class FaceBookLoginCommandRequest: IRequest<FaceBookLoginCommandResponse>
{
    public string AuthToken { get; set; }
}