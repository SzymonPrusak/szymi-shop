using System.Diagnostics.CodeAnalysis;
using SzymiShop.WebApi.Business.Model.Product;

namespace SzymiShop.WebApi.Controller.Product
{
    public class ProductDetailsResponse : ProductResponse
    {
        public ProductDetailsResponse() { }
        [SetsRequiredMembers]
        public ProductDetailsResponse(ProductDetails product)
            : base(product)
        {
            Description= product.Description;
            Images = product.Images
                .Select(i => new ProductImageResponse(i))
                .ToList();
        }


        public required string Description { get; set; }
        public required IList<ProductImageResponse> Images { get; set; }
    }
}
