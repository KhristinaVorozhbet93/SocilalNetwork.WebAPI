using SocialNetwork.Core.Contracts.Base;

namespace SocialNetwork.UserSvc.Exceptions
{
    public class EmailAlreadyExistsException : DomainException
    {
        public EmailAlreadyExistsException()
        {
        }

        public EmailAlreadyExistsException(string? message) : base(message)
        {
        }

        public EmailAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
