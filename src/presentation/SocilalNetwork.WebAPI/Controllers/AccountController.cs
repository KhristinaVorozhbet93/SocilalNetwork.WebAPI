using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain.Services;
using SocilalNetwork.WebAPI.HttpModels.Responses;

namespace SocilalNetwork.WebAPI.Controllers
{
    [Route ("account")]
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
            catch (InvalidOperationException)
            {
                return Conflict("Аккаунт с таким email уже существует!");
            }
            catch (ArgumentException)
            {
                return BadRequest("Поле не может быть неопределенным или пустым");
            }
        }
    }
}
