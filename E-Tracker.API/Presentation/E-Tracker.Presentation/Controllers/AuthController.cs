


using E_Tracker.Application.Features.Commands.AppUser.RefreshTokenLogin;
using Google.Apis.Auth.OAuth2.Requests;

namespace E_Tracker.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginCommandRequest loginCommandRequest)
        {
            var response = await _mediator.Send(loginCommandRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshToken([FromBody]RefreshTokenCommandRequest refreshTokenRequest)
        
        {
            RefreshTokenCommandResponse response = await _mediator.Send(refreshTokenRequest);
            return Ok(response);
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest googleLoginCommandRequest)
        {
            GoogleLoginCommandResponse response = await _mediator.Send(googleLoginCommandRequest);
            return Ok(response);
        }
        [HttpPost("facebook-login")]
        public async Task<IActionResult> FaceBookLogin(FaceBookLoginCommandRequest faceBookLoginCommandRequest)
        {
            FaceBookLoginCommandResponse response = await _mediator.Send(faceBookLoginCommandRequest);
            return Ok(response);
        }
    }
}
