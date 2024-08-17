using Microsoft.EntityFrameworkCore;
using SocialNetwork.Shared;

namespace SocialNetwork.WebAPI.AccountService
{
    public class AccountRepository : EFRepository<Account>, IAccountRepository
    {
        public AccountRepository(AccountDbContext appDbContext) : base(appDbContext) { }
        public async Task<Account?> FindAccountByEmail(string email, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(email);

            return await Entities.SingleOrDefaultAsync(it => it.Email.Value == email, cancellationToken);
        }

        public async Task<Account?> FindAccountById(Guid id, CancellationToken cancellationToken)
        {
            return await Entities.SingleOrDefaultAsync(it => it.Id == id, cancellationToken);
        }
    }
}
