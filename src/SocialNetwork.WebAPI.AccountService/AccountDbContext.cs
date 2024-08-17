using Microsoft.EntityFrameworkCore;

namespace SocialNetwork.WebAPI.AccountService
{
    public class AccountDbContext : DbContext
    {
        DbSet<Account> Accounts => Set<Account>();
        public AccountDbContext(DbContextOptions<AccountDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .OwnsOne(a => a.Email);
        }
    }
}
