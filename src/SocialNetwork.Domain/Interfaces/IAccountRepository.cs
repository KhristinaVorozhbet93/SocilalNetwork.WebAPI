using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Entities.Value_Objects;

namespace SocialNetwork.Domain.Interfaces
{
    public interface IAccountRepository : IRepositoryEF<Account>
    {
        Task<Account?> FindAccountByEmail(string email, CancellationToken cancellationToken);
        Task<Account?> FindAccountById(Guid id, CancellationToken cancellationToken);
    }
}
