
namespace SzymiShop.WebApi.Business.Model.User
{
    public interface IUser : IEntity
    {
        string Login { get; }
        HashedPassword Password { get; }
    }
}
