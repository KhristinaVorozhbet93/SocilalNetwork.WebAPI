using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain.Exceptions;
using SocialNetwork.Domain.Services;
using SocilalNetwork.WebAPI.HttpModels.Requests;
using SocilalNetwork.WebAPI.HttpModels.Responses;

namespace SocilalNetwork.WebAPI.Controllers
{
    [Route("account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService
                ?? throw new ArgumentNullException(nameof(accountService));
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponse>> Register
            (RegisterRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response =
                    await _accountService.Register(request.Email, request.Password, cancellationToken);
                return Ok(new RegisterResponse(response.Id, response.Email));
            }
            catch (EmailAlreadyExistsException)
            {
                return BadRequest(new ErrorResponse("Аккаунт с таким email уже зарегистрирован"));
            }
        }

        [HttpPost("login_by_password")]
        public async Task<ActionResult<LoginResponse>> LoginByPassword(LoginRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _accountService.LoginByPassword(request.Email, request.Password, cancellationToken);
                return Ok(new LoginResponse(request.Email));
            }
            catch (AccountNotFoundException)
            {
                return BadRequest(new ErrorResponse("Аккаунт с таким e-mail не найден"));
            }
            catch (InvalidPasswordException)
            {
                return BadRequest(new ErrorResponse("Неверный пароль"));
            }
        }

        [HttpPost("delete_account")]
        public async Task<ActionResult> DeleteAccount(DeleteAccountRequest request, CancellationToken cancellationToken)
        {
            try {
                await _accountService.DeleteAccount(request.Id, cancellationToken);
                return Ok();
            }
            catch (AccountNotFoundException)
            {
                return BadRequest(new ErrorResponse("Аккаунт с таким e-mail не найден"));
            }
        }
    }
}
