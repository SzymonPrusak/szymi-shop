using System.Diagnostics.CodeAnalysis;

namespace SzymiShop.WebApi.Business.Model.Product
{
    public class ProductImage : Image.Image, IProductImage
    {
        public ProductImage() { }
        [SetsRequiredMembers]
        public ProductImage(IProductImage img)
            : base(img)
        {
            Order = img.Order;
        }

        public required int Order { get; set; }
    }
}
