using SocialNetwork.Domain.Interfaces;

namespace SocialNetwork.WebAPI.ContactService
{
    public interface IContactsDetailsRepository : IRepositoryEF<ContactDetails>
    {
        Task GetContactDetailsByAccountId(Guid accountId, CancellationToken cancellationToken);
    }
}
