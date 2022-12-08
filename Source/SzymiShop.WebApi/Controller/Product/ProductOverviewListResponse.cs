namespace SzymiShop.WebApi.Controller.Product
{
    public class ProductOverviewListResponse
    {
        public required IList<ProductOverviewResponse> Products { get; set; }
    }
}
