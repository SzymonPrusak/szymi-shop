namespace SzymiShop.WebApi.Controller.Product.Response
{
    public class ProductOverviewListResponse
    {
        public required IList<ProductOverviewResponse> Products { get; set; }
    }
}
