
namespace SzymiShop.WebApi.Business.Model.Product
{
    public interface IProduct : IEntity
    {
        string Name { get; }
        User.User Seller { get; }
        Price Price { get; }
    }
}
