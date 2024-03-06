using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.DataEntityFramework
{
    public class AppDbContext : DbContext
    {
        DbSet<Account> Accounts => Set<Account>();
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
    }
}
