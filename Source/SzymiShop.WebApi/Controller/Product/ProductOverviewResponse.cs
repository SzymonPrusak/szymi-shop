using System.Diagnostics.CodeAnalysis;
using SzymiShop.WebApi.Business.Model;
using SzymiShop.WebApi.Business.Model.Product;

namespace SzymiShop.WebApi.Controller.Product
{
    public class ProductOverviewResponse : ProductResponse
    {
        public ProductOverviewResponse() { }
        [SetsRequiredMembers]
        public ProductOverviewResponse(ProductOverview product)
            : base(product)
        {
            IconId = product.Icon.Id;
        }

        public required Guid IconId { get; set; }
    }
}
