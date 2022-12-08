using System.Diagnostics.CodeAnalysis;
using SzymiShop.WebApi.Business.Model.Product;

namespace SzymiShop.WebApi.Controller.Product.Payload
{
    public class ProductImagePayload
    {
        public ProductImagePayload() { }
        [SetsRequiredMembers]
        public ProductImagePayload(ProductImage image)
        {
            Id = image.Id;
            Order = image.Order;
            Content = image.Content;
        }

        public required Guid? Id { get; set; }
        public required int Order { get; set; }
        public required byte[]? Content { get; set; }
    }
}
