using SocialNetwork.Core.Contracts.Infrastructure;

namespace SocialNetwork.ContactService
{
    public interface IContactsDetailsRepository : IRepositoryEF<ContactDetails>
    {
        Task GetContactDetailsByAccountId(Guid accountId, CancellationToken cancellationToken);
    }
}
