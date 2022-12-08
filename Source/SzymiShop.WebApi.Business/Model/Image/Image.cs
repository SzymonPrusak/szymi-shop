using System.Diagnostics.CodeAnalysis;

namespace SzymiShop.WebApi.Business.Model.Image
{
    public class Image : Entity, IImage
    {
        public Image() { }
        [SetsRequiredMembers]
        public Image(IImage img) : base(img)
        {
            Content = img.Content;
        }


        public required byte[]? Content { get; set; }
    }
}
