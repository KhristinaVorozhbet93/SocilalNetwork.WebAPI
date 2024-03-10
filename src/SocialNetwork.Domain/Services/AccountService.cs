using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Interfaces;

namespace SocialNetwork.Domain.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository 
                ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        public async Task<Account> Register
            (string email, string password, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException($"\"{nameof(email)}\" не может быть неопределенным или пустым.", nameof(email));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException($"\"{nameof(password)}\" не может быть неопределенным или пустым.", nameof(password));
            }

            var existedAccount = await _accountRepository.FindAccountByEmail(email, cancellationToken);
            if (existedAccount is not null)
            {
                throw new InvalidOperationException($"Aккаунт с таким email уже существует."); 
            }
            var account = new Account(Guid.NewGuid(),email, password);
            await _accountRepository.Add(account, cancellationToken);
            return account; 
        }
    }
}
