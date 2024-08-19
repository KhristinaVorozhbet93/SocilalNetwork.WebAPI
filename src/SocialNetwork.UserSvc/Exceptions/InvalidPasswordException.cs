using SocialNetwork.Core.Contracts.Base;

namespace SocialNetwork.UserSvc.Exceptions
{
    public class InvalidPasswordException : DomainException
    {
        public InvalidPasswordException()
        {
        }

        public InvalidPasswordException(string? message) : base(message)
        {
        }

        public InvalidPasswordException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
