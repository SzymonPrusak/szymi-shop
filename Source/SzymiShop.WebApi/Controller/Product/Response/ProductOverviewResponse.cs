using System.Diagnostics.CodeAnalysis;
using SzymiShop.WebApi.Business.Model.Product;
using SzymiShop.WebApi.Controller.Product.Payload;

namespace SzymiShop.WebApi.Controller.Product.Response
{
    public class ProductOverviewResponse : ProductPayload
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
