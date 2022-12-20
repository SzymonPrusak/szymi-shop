
namespace SzymiShop.WebApi.Persistence.User
{
    public interface IRefreshTokenService
    {
        Task<RefreshToken?> Find(Guid id, CancellationToken token = default);
        Task Create(RefreshToken token);
        Task Delete(RefreshToken token);
    }
}
