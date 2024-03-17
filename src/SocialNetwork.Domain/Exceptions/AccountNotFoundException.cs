
namespace SocialNetwork.Domain.Exceptions
{
    public class AccountNotFoundException : DomainException
    {
        public AccountNotFoundException()
        {
        }

        public AccountNotFoundException(string? message) : base(message)
        {
        }

        public AccountNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
