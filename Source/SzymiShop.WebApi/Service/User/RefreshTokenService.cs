using SzymiShop.WebApi.Persistence;
using SzymiShop.WebApi.Service.Crypto;

namespace SzymiShop.WebApi.Service.User
{
    public class RefreshTokenService : Persistence.User.RefreshTokenService, IRefreshTokenService
    {
        private readonly ISignatureService _sigService;

        public RefreshTokenService(ShopDbContext dbContext, ISignatureService sigService)
            : base(dbContext)
        {
            _sigService = sigService;
        }


        public string Sign(Persistence.User.RefreshToken token)
        {
            return _sigService.Sign(token.Id.ToString());
        }

        public bool Verify(Persistence.User.RefreshToken token, string signature)
        {
            return _sigService.Sign(token.Id.ToString()) == signature;
        }
    }
}
