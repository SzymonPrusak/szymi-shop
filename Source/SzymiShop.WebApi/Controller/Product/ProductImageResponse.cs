using System.Diagnostics.CodeAnalysis;
using SzymiShop.WebApi.Business.Model.Product;

namespace SzymiShop.WebApi.Controller.Product
{
    public class ProductImageResponse
    {
        public ProductImageResponse() { }
        [SetsRequiredMembers]
        public ProductImageResponse(ProductImage image)
        {
            Id = image.Id;
            Order = image.Order;
        }

        public required Guid Id { get; set; }
        public required int Order { get; set; }
    }
}
