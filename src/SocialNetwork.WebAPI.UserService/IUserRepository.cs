using SocialNetwork.Shared.Contracts;

namespace SocialNetwork.WebAPI.AccountService
{
    public interface IUserRepository : IRepositoryEF<User>
    {
        Task<User?> FindAccountByEmail(string email, CancellationToken cancellationToken);
        Task<User?> FindAccountById(Guid id, CancellationToken cancellationToken);
    }
}
