
namespace SzymiShop.WebApi.Business.Model.Image
{
    public interface IImage : IEntity
    {
        byte[]? Content { get; }
    }
}
