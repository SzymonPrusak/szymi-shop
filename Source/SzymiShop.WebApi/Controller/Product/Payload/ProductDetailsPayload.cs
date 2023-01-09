using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using SzymiShop.WebApi.Business.Model.Product;

namespace SzymiShop.WebApi.Controller.Product.Payload
{
    public class ProductDetailsPayload : ProductPayload
    {
        public ProductDetailsPayload() { }
        [SetsRequiredMembers]
        public ProductDetailsPayload(ProductDetails product)
            : base(product)
        {
            Description = product.Description;
            Images = product.Images
                .Select(i => new ProductImagePayload(i))
                .ToList();
        }


        [Required]
        public required string Description { get; set; }
        public required IList<ProductImagePayload> Images { get; set; }
    }
}
