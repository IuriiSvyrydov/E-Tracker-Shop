using E_Tracker.Application.Abstractions.Services;
using MediatR;

namespace E_Tracker.Application.Features.Commands.AppUser.FaceBookLogin;

public class FaceBookLoginCommandHandler: IRequestHandler<FaceBookLoginCommandRequest,FaceBookLoginCommandResponse>
{
    private readonly IAuthService _authService;


    public FaceBookLoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<FaceBookLoginCommandResponse> Handle(FaceBookLoginCommandRequest request, CancellationToken cancellationToken)
    {
      var token =  await _authService.FaceBookLoginAsync(request.AuthToken,15);
      
          return new()
          {
              TokenDto = token
          };


    }
}