using SzymiShop.WebApi.Business.Model.Image;

namespace SzymiShop.WebApi.Persistence.Image
{
    internal class Image : Entity, IImage
    {
        public required byte[] Content { get; set; }
    }
}
