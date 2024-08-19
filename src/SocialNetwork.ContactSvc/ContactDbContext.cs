using Microsoft.EntityFrameworkCore;

namespace SocialNetwork.ContactService
{
    public class ContactDbContext : DbContext
    {
        DbSet<ContactDetails> ContactDetails => Set<ContactDetails>();
        public ContactDbContext(DbContextOptions<ContactDbContext> options)
            : base(options) { }

    }
}
