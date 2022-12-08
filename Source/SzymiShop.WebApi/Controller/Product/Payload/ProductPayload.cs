using System.Diagnostics.CodeAnalysis;
using SzymiShop.WebApi.Business.Model;

namespace SzymiShop.WebApi.Controller.Product.Payload
{
    public class ProductPayload
    {
        public ProductPayload() { }
        [SetsRequiredMembers]
        public ProductPayload(Business.Model.Product.Product product)
        {
            Id = product.Id;
            Name = product.Name;
            SellerId = product.Seller.Id;
            SellerName = product.Seller.Login;
            Price = product.Price;
        }

        public required Guid? Id { get; set; }
        public required string Name { get; set; }
        public required Guid SellerId { get; set; }
        public required string SellerName { get; set; }
        public required Price Price { get; set; }
    }
}
