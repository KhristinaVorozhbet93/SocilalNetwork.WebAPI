using SocialNetwork.UserSvc.Entities;
using SocialNetwork.UserSvc.Exceptions;
using SocialNetwork.UserSvc.Repositories;


namespace SocialNetwork.UserSvc.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IApplicationPasswordHasher _passwordHasher;

        public UserService
            (IUserRepository userRepository,
            IApplicationPasswordHasher passwordHasher)
        {
            _userRepository = userRepository
                ?? throw new ArgumentNullException(nameof(userRepository));
            _passwordHasher = passwordHasher
                 ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

        public async Task<User> Register
            (string email, string password, CancellationToken cancellationToken)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(email));
            ArgumentException.ThrowIfNullOrEmpty(nameof(password));

            var existedUser = await _userRepository.FindUserByEmail(email, cancellationToken);
            if (existedUser is not null)
            {
                throw new EmailAlreadyExistsException("Aккаунт с таким email уже существует");
            }
            var user = new User(Guid.NewGuid(), new Email(email), EncryptPassword(password));
            await _userRepository.Add(user, cancellationToken);
            return user;
        }

        public async Task<User> LoginByPassword(string email, string password, CancellationToken cancellationToken)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(nameof(email));
            ArgumentException.ThrowIfNullOrWhiteSpace(nameof(password));

            var user = await _userRepository.FindUserByEmail(email, cancellationToken);
            if (user is null)
            {
                throw new UserNotFoundException("Аккаунт с таким e-mail не найден");
            }

            var isPasswordValid =
                _passwordHasher.VerifyHashedPassword
                (user.HashedPassword, password, out bool rehash);

            if (!isPasswordValid)
            {
                throw new InvalidPasswordException("Неверный пароль");
            }

            if (rehash)
            {
                await RehashPassword(password, user, cancellationToken);
            }
            return user;
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
            var user = await _userRepository.FindUserById(id, cancellationToken);
            if (user is null)
            {
                throw new UserNotFoundException("Аккаунт с таким id не найден");
            }

            await _userRepository.Delete(user, cancellationToken);
        }
    }
}
