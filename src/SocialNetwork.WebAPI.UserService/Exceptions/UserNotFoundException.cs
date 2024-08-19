using SocialNetwork.Shared.Exceptions;

namespace SocialNetwork.WebAPI.AccountService.Exceptions
{
    public class UserNotFoundException : DomainException
    {
        public UserNotFoundException()
        {
        }

        public UserNotFoundException(string? message) : base(message)
        {
        }

        public UserNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
