using System.Diagnostics.CodeAnalysis;

namespace SzymiShop.WebApi.Business.Model.Product
{
    public class Product : Entity, IProduct
    {
        public Product() { }
        [SetsRequiredMembers]
        public Product(IProduct product)
            : base(product)
        {
            Name = product.Name;
            Seller = product.Seller;
            Price = product.Price;
        }


        public required string Name { get; set; }
        public required User.User Seller { get; set; }

        public required Price Price { get; set; }
    }
}
