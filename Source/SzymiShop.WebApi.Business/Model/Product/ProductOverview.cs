using System.Diagnostics.CodeAnalysis;

namespace SzymiShop.WebApi.Business.Model.Product
{
    public class ProductOverview : Product
    {
        public ProductOverview() { }
        [SetsRequiredMembers]
        public ProductOverview(IProduct product, Image.Image icon)
            : base(product)
        {
            Icon = icon;
        }


        public required Image.Image Icon { get; set; }
    }
}
