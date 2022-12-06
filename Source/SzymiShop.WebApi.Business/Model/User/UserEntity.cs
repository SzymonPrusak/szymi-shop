using SzymiShop.WebApi.Business.Validation;

namespace SzymiShop.WebApi.Business.Model.User
{
    public class UserEntity : Entity
    {
        public const int MinLoginLength = 5;
        public const int MaxLoginLength = 32;
        public const int MinPasswordLength = 8;
        public const int MaxPasswordLength = 64;

        private string _login;

        public UserEntity(string login, HashedPassword password, bool validateLogin = true)
        {
            _login = login;
            if (validateLogin)
                ValidateLogin(login);
            Password = password;
        }


        public string Login
        {
            get => _login;
            set
            {
                ValidateLogin(value);
                _login = value;
            }
        }

        public HashedPassword Password { get; private set; }


        public void ChangePassword(string newPassword)
        {
            StringValidate.NotNullOrWhitespace(newPassword);
            StringValidate.Length(newPassword, MinPasswordLength, MaxPasswordLength);

            Password = new HashedPassword(newPassword);
        }

        private void ValidateLogin(string login)
        {
            StringValidate.NotNullOrWhitespace(login);
            StringValidate.Length(login, MinLoginLength, MaxLoginLength);
        }
    }
}
