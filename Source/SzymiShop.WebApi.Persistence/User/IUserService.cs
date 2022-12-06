using SzymiShop.WebApi.Business.Model.User;

namespace SzymiShop.WebApi.Persistence.User
{
    public interface IUserService
    {
        Task<UserEntity?> FindByLogin(string login, CancellationToken token = default);
        Task Create(UserEntity user);
        Task Update(UserEntity user);
    }
}
