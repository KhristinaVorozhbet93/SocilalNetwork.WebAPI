namespace SocialNetwork.WebAPI.ContactService
{
    public class ContactDetailsService
    {
        private readonly IContactsDetailsRepository _contactsDetailsRepository;

        public ContactDetailsService(IContactsDetailsRepository contactsDetailsRepository)
        {
            _contactsDetailsRepository = contactsDetailsRepository
              ?? throw new ArgumentNullException(nameof(contactsDetailsRepository));
        }

        public async Task GetContactDetails(Guid accountId, CancellationToken cancellationToken)
        {
            //сначала нужно найти аккаунт, у которого эти контактные данные, а затем уже поменять


        }
        //получить контактные данные по Id аккаунта
        //редактировать контактные данные 

    }
}
