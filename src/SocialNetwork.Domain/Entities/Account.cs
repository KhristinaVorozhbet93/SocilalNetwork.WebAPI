namespace SocialNetwork.Domain.Entities
{
    public class Account 
    {
        private Guid _id;
        private string _email;
        private string _hashedPassword;

        public Account(Guid id, string email, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException(nameof(email));
            }
            if (string.IsNullOrWhiteSpace(hashedPassword))
            {
                throw new ArgumentNullException(nameof(hashedPassword));
            }
            _id = id;
            _email = email;
            _hashedPassword = hashedPassword;
        }

        public Guid Id
        {
            get
            {
                return _id;
            }
            init
            {
                _id = value;
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _email = value;
            }
        }

        public string HashedPassword
        {
            get
            {
                return _hashedPassword;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _hashedPassword = value;
            }
        }
    }
}
