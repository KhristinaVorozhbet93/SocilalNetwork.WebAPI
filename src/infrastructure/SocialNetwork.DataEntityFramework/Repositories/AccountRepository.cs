using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Interfaces;

namespace SocialNetwork.DataEntityFramework.Repositories
{
    public class AccountRepository : EFRepository<Account>, IAccountRepository
    {
        public AccountRepository(AppDbContext appDbContext) : base (appDbContext) {}
        public async Task<Account?> FindAccountByEmail(string email, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            return await Entities.SingleOrDefaultAsync(it => it.Email == email, cancellationToken);
        }
    }
}
