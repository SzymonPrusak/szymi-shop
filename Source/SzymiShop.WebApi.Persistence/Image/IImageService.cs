
namespace SzymiShop.WebApi.Persistence.Image
{
    internal interface IImageService
    {
        Task<bool> LoadContent(Business.Model.Image.Image i, CancellationToken token = default);
    }
}
