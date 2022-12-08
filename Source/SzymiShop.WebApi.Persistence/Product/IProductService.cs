using SzymiShop.WebApi.Business.Model.Product;

namespace SzymiShop.WebApi.Persistence.Product
{
    public interface IProductService
    {
        Task<IEnumerable<ProductOverview>> ReadProductOverviews(CancellationToken token = default);

        Task<ProductDetails?> FindProductDetails(Guid id, CancellationToken token = default);

        Task CreateUpdate(ProductDetails product);
    }
}
