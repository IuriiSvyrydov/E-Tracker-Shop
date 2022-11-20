

using E_Tracker.Application.Features.Commands.AppUser.CreateUser;


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
       
    }
}
