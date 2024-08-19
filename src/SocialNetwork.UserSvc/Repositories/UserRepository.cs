using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Contracts.Infrastructure;
using SocialNetwork.UserSvc.Entities;

namespace SocialNetwork.UserSvc.Repositories
{
    public class UserRepository : EFRepository<User>, IUserRepository
    {
        public UserRepository(UserDbContext appDbContext) : base(appDbContext) { }
        public async Task<User?> FindUserByEmail(string email, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(email);

            return await Entities.SingleOrDefaultAsync(it => it.Email.Value == email, cancellationToken);
        }

        public async Task<User?> FindUserById(Guid id, CancellationToken cancellationToken)
        {
            return await Entities.SingleOrDefaultAsync(it => it.Id == id, cancellationToken);
        }
    }
}
