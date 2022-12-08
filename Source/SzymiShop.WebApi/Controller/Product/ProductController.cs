using Microsoft.AspNetCore.Mvc;
using SzymiShop.WebApi.Persistence.Product;

namespace SzymiShop.WebApi.Controller.Product
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public async Task<IActionResult> ListOverviews(CancellationToken token = default)
        {
            var products = await _productService.ReadProductOverviews(token);

            var resp = new ProductOverviewListResponse
            {
                Products = products
                    .Select(p => new ProductOverviewResponse(p))
                    .ToList()
            };
            return Ok(resp);
        }

        [HttpGet("Details/{id:guid}")]
        public async Task<IActionResult> GetDetails(Guid id, CancellationToken token = default)
        {
            var prod = await _productService.FindProductDetails(id, token);
            if (prod == null)
                return NotFound();

            var resp = new ProductDetailsResponse(prod);
            return Ok(resp);
        }
    }
}
