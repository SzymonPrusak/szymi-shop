using SzymiShop.WebApi.Persistence.User;

namespace SzymiShop.WebApi.Service.User
{
    public interface IRefreshTokenService : Persistence.User.IRefreshTokenService
    {
        string Sign(RefreshToken token);
        bool Verify(RefreshToken token, string signature);
    }
}
