using SocialNetwork.Shared.Contracts;

namespace SocialNetwork.WebAPI.ContactService
{
    public class ContactDetails : IEntity
    {
        private Guid _id;
        private string? _phone;
        private string? _city;

        protected ContactDetails() { }
        public ContactDetails(Guid id)
        {
            _id = id;
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

        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
            }
        }


        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
            }
        }

    }
}
