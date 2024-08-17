using SocialNetwork.Shared.Interfaces;

namespace SocialNetwork.WebAPI.AccountService
{
    public interface IAccountRepository : IRepositoryEF<Account>
    {
        Task<Account?> FindAccountByEmail(string email, CancellationToken cancellationToken);
        Task<Account?> FindAccountById(Guid id, CancellationToken cancellationToken);
    }
}
