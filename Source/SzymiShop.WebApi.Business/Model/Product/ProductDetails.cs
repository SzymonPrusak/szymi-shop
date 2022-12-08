using System.Diagnostics.CodeAnalysis;

namespace SzymiShop.WebApi.Business.Model.Product
{
    public class ProductDetails : Product
    {
        public ProductDetails() { }
        [SetsRequiredMembers]
        public ProductDetails(IProduct product, string description, IList<ProductImage> images)
            : base(product)
        {
            Description = description;
            Images = images;
        }


        public required string Description { get; set; }
        public required IList<ProductImage> Images { get; set; }
    }
}
