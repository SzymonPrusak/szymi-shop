using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using SzymiShop.WebApi.Controller.Product.Payload;

namespace SzymiShop.WebApi.Controller.Product.Response
{
    public class ProductResponse : ProductPayload
    {
        [SetsRequiredMembers]
        public ProductResponse(Business.Model.Product.Product product)
            : base(product)
        {
            SellerName = product.Seller.Login;
        }

        [Required]
        public required string SellerName { get; set; }
    }
}
