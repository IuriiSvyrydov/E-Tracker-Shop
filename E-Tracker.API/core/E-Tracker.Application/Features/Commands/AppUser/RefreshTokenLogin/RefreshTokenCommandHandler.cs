using E_Tracker.Application.Abstractions.Services;
using E_Tracker.Application.DTOs;
using MediatR;

namespace E_Tracker.Application.Features.Commands.AppUser.RefreshTokenLogin
{
    public class RefreshTokenCommandHandler: IRequestHandler<RefreshTokenCommandRequest,RefreshTokenCommandResponse>
    {
        private  readonly  IAuthService _authService;

        public RefreshTokenCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
        {
            
            TokenDto token = await _authService.RefreshTokenLoginAsync(request.RefreshToken);
            return new()
            {
                Token = token
            };
        }
    }
}
