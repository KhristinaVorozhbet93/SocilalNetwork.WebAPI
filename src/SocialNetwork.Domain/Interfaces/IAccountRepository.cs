using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account?> FindAccountByEmail(string email, CancellationToken cancellationToken);
    }
}
