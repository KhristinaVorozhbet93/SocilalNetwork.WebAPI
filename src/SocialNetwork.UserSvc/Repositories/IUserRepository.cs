using SocialNetwork.Core.Contracts.Infrastructure;
using SocialNetwork.UserSvc.Entities;

namespace SocialNetwork.UserSvc.Repositories
{
    public interface IUserRepository : IRepositoryEF<User>
    {
        Task<User?> FindUserByEmail(string email, CancellationToken cancellationToken);
        Task<User?> FindUserById(Guid id, CancellationToken cancellationToken);
    }
}
