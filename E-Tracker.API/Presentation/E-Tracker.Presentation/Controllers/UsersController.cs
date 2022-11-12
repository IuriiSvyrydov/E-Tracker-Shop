

using E_Tracker.Application.Features.Commands.AppUser.CreateUser;
using E_Tracker.Application.Features.Commands.AppUser.FaceBookLogin;
using E_Tracker.Application.Features.Commands.AppUser.GoogleLogin;
using E_Tracker.Application.Features.Commands.Login;

namespace E_Tracker.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest )
        {
            var response = await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginCommandRequest loginCommandRequest)
        {
            var response = await _mediator.Send(loginCommandRequest);
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
