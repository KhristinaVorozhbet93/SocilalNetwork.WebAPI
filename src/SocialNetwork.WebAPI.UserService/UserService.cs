using SocialNetwork.WebAPI.AccountService.Exceptions;

namespace SocialNetwork.WebAPI.AccountService
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IApplicationPasswordHasher _passwordHasher;

        public UserService
            (IUserRepository accountRepository,
            IApplicationPasswordHasher passwordHasher)
        {
            _userRepository = accountRepository
                ?? throw new ArgumentNullException(nameof(accountRepository));
            _passwordHasher = passwordHasher
                 ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

        public async Task<User> Register
            (string email, string password, CancellationToken cancellationToken)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(email));
            ArgumentException.ThrowIfNullOrEmpty(nameof(password));

            var existedAccount = await _userRepository.FindAccountByEmail(email, cancellationToken);
            if (existedAccount is not null)
            {
                throw new EmailAlreadyExistsException("Aккаунт с таким email уже существует");
            }
            var account = new User(Guid.NewGuid(), new Email(email), EncryptPassword(password));
            await _userRepository.Add(account, cancellationToken);
            return account;
        }

        public async Task LoginByPassword(string email, string password, CancellationToken cancellationToken)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(nameof(email));
            ArgumentException.ThrowIfNullOrWhiteSpace(nameof(password));

            var account = await _userRepository.FindAccountByEmail(email, cancellationToken);
            if (account is null)
            {
                throw new UserNotFoundException("Аккаунт с таким e-mail не найден");
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

        private async Task RehashPassword(string password, User account, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(nameof(account));
            ArgumentException.ThrowIfNullOrEmpty(nameof(password));
            account.HashedPassword = EncryptPassword(password);
            await _userRepository.Update(account, cancellationToken);
        }

        private string EncryptPassword(string password)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(nameof(password));
            return _passwordHasher.HashPassword(password);
        }

        public async Task DeleteAccount(Guid id, CancellationToken cancellationToken)
        {
            var account = await _userRepository.FindAccountById(id, cancellationToken);
            if (account is null)
            {
                throw new UserNotFoundException("Аккаунт с таким id не найден");
            }

            await _userRepository.Delete(account, cancellationToken);
        }
    }
}
