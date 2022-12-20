using Microsoft.IdentityModel.Tokens;

namespace SzymiShop.WebApi.Service.User
{
    public interface IAccessTokenService
    {
        string Generate(Guid userId);
        Guid? Validate(string? token);
    }
}
