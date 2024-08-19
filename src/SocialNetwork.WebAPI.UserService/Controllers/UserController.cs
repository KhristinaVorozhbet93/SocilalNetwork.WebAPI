using Microsoft.AspNetCore.Mvc;
using SocialNetwork.HttpModels.HttpModels.Requests;
using SocialNetwork.HttpModels.HttpModels.Responses;
using SocialNetwork.WebAPI.AccountService.Exceptions;

namespace SocialNetwork.WebAPI.AccountService.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService accountService)
        {
            _userService = accountService
                ?? throw new ArgumentNullException(nameof(accountService));
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponse>> Register
            (RegisterRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response =
                    await _userService.Register(request.Email, request.Password, cancellationToken);
                return Ok(new RegisterResponse(response.Id, response.Email.ToString()));
            }
            catch (EmailAlreadyExistsException)
            {
                return BadRequest(new ErrorResponse("Аккаунт с таким email уже зарегистрирован"));
            }
            catch (InvalidOperationException)
            {
                return BadRequest(new ErrorResponse("Некорректный адрес e-mail адреса"));
            }
        }

        [HttpPost("login_by_password")]
        public async Task<ActionResult<LoginResponse>> LoginByPassword(LoginRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _userService.LoginByPassword(request.Email, request.Password, cancellationToken);
                return Ok(new LoginResponse(request.Email));
            }
            catch (UserNotFoundException)
            {
                return NotFound(new ErrorResponse("Аккаунт с таким e-mail не найден"));
            }
            catch (InvalidPasswordException)
            {
                return BadRequest(new ErrorResponse("Неверный пароль"));
            }
        }
    }
}
