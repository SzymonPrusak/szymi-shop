using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using SzymiShop.WebApi.Business.Model.Product;
using SzymiShop.WebApi.Controller.Product.Payload;

namespace SzymiShop.WebApi.Controller.Product.Response
{
    public class ProductDetailsResponse : ProductDetailsPayload
    {
        [SetsRequiredMembers]
        public ProductDetailsResponse(ProductDetails product)
            : base(product)
        {
            SellerName = product.Seller.Login;
        }

        [Required]
        public required string SellerName { get; set; }
    }
}
