
namespace SzymiShop.WebApi.Persistence.User
{
    public interface IUserService
    {
        Task<Business.Model.User.User?> FindByLogin(string login, CancellationToken token = default);
        Task Create(Business.Model.User.User user);
        Task Update(Business.Model.User.User user);
    }
}
