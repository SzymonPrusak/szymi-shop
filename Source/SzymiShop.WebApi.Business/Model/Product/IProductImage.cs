using SzymiShop.WebApi.Business.Model.Image;

namespace SzymiShop.WebApi.Business.Model.Product
{
    public interface IProductImage : IImage
    {
        int Order { get; }
    }
}
