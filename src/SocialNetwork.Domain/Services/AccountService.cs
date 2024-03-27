using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Exceptions;
using SocialNetwork.Domain.Interfaces;

namespace SocialNetwork.Domain.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IApplicationPasswordHasher _passwordHasher;

        public AccountService
            (IAccountRepository accountRepository,
            IApplicationPasswordHasher passwordHasher)
        {
            _accountRepository = accountRepository
                ?? throw new ArgumentNullException(nameof(accountRepository));
            _passwordHasher = passwordHasher
                 ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

        public async Task<Account> Register
            (string email, string password, CancellationToken cancellationToken)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(email));
            ArgumentException.ThrowIfNullOrEmpty(nameof(password));

            var existedAccount = await _accountRepository.FindAccountByEmail(email, cancellationToken);
            if (existedAccount is not null)
            {
                throw new EmailAlreadyExistsException("Aккаунт с таким email уже существует");
            }
            var account = new Account(Guid.NewGuid(), email, EncryptPassword(password));
            await _accountRepository.Add(account, cancellationToken);
            return account;
        }

        public async Task LoginByPassword(string email, string password, CancellationToken cancellationToken) 
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(nameof(email));
            ArgumentException.ThrowIfNullOrWhiteSpace(nameof(password));

            var account = await _accountRepository.FindAccountByEmail(email, cancellationToken);
            if (account is null)
            {
                throw new AccountNotFoundException("Аккаунт с таким e-mail не найден");
            }

            var isPasswordValid = 
                _passwordHasher.VerifyHashedPassword
                (account.HashedPassword, password, out bool rehash);

            if (!isPasswordValid)
            {
                throw new InvalidPasswordException("Неверный пароль");
            }

            if (rehash)
            {
                await RehashPassword(password, account, cancellationToken);
            }
        }

        private async Task RehashPassword(string password, Account account, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(nameof(account));
            ArgumentException.ThrowIfNullOrEmpty(nameof(password));
            account.HashedPassword = EncryptPassword(password);
            await _accountRepository.Update(account, cancellationToken);
        }

        private string EncryptPassword(string password)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(nameof(password));
            return _passwordHasher.HashPassword(password);
        }

        public async Task DeleteAccount(Guid id, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.FindAccountById(id, cancellationToken);
            if (account is null)
            {
                throw new AccountNotFoundException("Аккаунт с таким id не найден");
            }

            await _accountRepository.Delete(account, cancellationToken);
        }
    }
}
