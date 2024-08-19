using SocialNetwork.Core.Contracts.Infrastructure;
using SocialNetwork.UserSvc.Entities;

namespace SocialNetwork.UserSvc.Repositories
{
    public interface IUserRepository : IRepositoryEF<User>
    {
        Task<User?> FindAccountByEmail(string email, CancellationToken cancellationToken);
        Task<User?> FindAccountById(Guid id, CancellationToken cancellationToken);
    }
}
