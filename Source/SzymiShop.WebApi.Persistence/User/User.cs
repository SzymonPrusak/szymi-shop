using SzymiShop.WebApi.Business.Model.User;

namespace SzymiShop.WebApi.Persistence.User
{
    internal class User : Entity, IUser
    {
        public required string Login { get; set; }
        public required string PasswordHash { get; set; }
        public required string PasswordSalt { get; set; }

        HashedPassword IUser.Password => new HashedPassword(PasswordHash, PasswordSalt);
    }
}
