using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using SzymiShop.WebApi.Business.Model;
using SzymiShop.WebApi.Business.Model.Image;
using SzymiShop.WebApi.Business.Model.Product;

namespace SzymiShop.WebApi.Persistence.Product
{
    [Owned]
    [Index(nameof(ProductId))]
    internal class ProductImage : IProductImage
    {
        public required Guid ProductId { get; set; }

        /// <summary>
        /// -1 is icon image.
        /// </summary>
        public required int Order { get; set; }

        public Guid ImageId { get; set; }
        [Required]
        public Image.Image? Image { get; set; }


        Guid IEntity.Id => ImageId;
        byte[]? IImage.Content => Image?.Content;
    }
}
