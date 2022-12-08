using SzymiShop.WebApi.Business.Model.Product;

namespace SzymiShop.WebApi.Persistence.Product
{
    internal class Product : Entity, IProduct
    {
        public required string Name { get; set; }
        public required User.User Seller { get; set; }
        public required int Price { get; set; } 
        public required string Description { get; set; }

        public required IList<ProductImage> Images { get; set; }


        Business.Model.User.User IProduct.Seller => new Business.Model.User.User(Seller);
        Business.Model.Price IProduct.Price => Business.Model.Price.FromValue(Price);
    }
}
