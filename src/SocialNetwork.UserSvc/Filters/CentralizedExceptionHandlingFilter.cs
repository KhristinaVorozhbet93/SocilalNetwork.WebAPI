using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Contracts.Base;
using SocialNetwork.UserSvc.Exceptions;

namespace SocialNetwork.UserSvc.Filters
{
    public class CentralizedExceptionHandlingFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var (message, statusCode) = TryGetUserMessageFromException(context);

            if (message != null && statusCode != 0)
            {
                context.Result = new ObjectResult(new ErrorResponse(message, statusCode))
                {
                    StatusCode = statusCode
                };
                context.ExceptionHandled = true;
            }
        }

        private (string?, int) TryGetUserMessageFromException(ExceptionContext context)
        {
            return context.Exception switch
            {
                EmailAlreadyExistsException => ("Аккаунт с таким email уже зарегистрирован", StatusCodes.Status409Conflict),
                UserNotFoundException => ("Аккаунт с таким e-mail не найден", StatusCodes.Status404NotFound),
                InvalidPasswordException => ("Неверный пароль", StatusCodes.Status401Unauthorized),
                InvalidOperationException => ("Некорректный адрес e-mail адреса", StatusCodes.Status400BadRequest),
                DomainException => ("Неизвестная ошибка!", StatusCodes.Status500InternalServerError),
                _ => (null, 0)
            };
        }
    }
}
