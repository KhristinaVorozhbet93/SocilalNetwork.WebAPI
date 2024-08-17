namespace SocialNetwork.WebAPI.ContactService
{
    public class ContactDetailsRepository : EFRepository<ContactDetails>, IContactsDetailsRepository
    {

        public ContactDetailsRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task GetContactDetailsByAccountId(Guid accountId, CancellationToken cancellationToken)
        {

        }
    }
}
