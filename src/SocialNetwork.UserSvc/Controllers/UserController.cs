using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Contracts.User;
using SocialNetwork.UserSvc.Services;

namespace SocialNetwork.UserSvc.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponse>> Register
            (RegisterRequest request, CancellationToken cancellationToken)
        {
            var response =
                await _userService.Register(request.Email, request.Password, cancellationToken);
            return Ok(new RegisterResponse(response.Id, response.Email.ToString()));
        }


        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> LoginByPassword(LoginRequest request, CancellationToken cancellationToken)
        {
            var response = await _userService.LoginByPassword(request.Email, request.Password, cancellationToken);
            return Ok(new LoginResponse(response.Id, response.Email.ToString()));
        }
    }
}
