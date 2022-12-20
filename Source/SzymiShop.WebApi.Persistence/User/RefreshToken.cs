
namespace SzymiShop.WebApi.Persistence.User
{
    public class RefreshToken : Entity
    {
        public required Guid UserId { get; set; }
    }
}
