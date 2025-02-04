﻿using SocialNetwork.Core.Contracts.Infrastructure;

namespace SocialNetwork.ContactService
{
    public class ContactDetailsRepository : EFRepository<ContactDetails>, IContactsDetailsRepository
    {

        public ContactDetailsRepository(ContactDbContext appDbContext) : base(appDbContext) { }

        public async Task GetContactDetailsByAccountId(Guid accountId, CancellationToken cancellationToken)
        {

        }
    }
}
