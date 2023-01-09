using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SzymiShop.WebApi.Business.Model.Product;
using SzymiShop.WebApi.Business.Model.User;
using SzymiShop.WebApi.Controller.Product.Payload;
using SzymiShop.WebApi.Controller.Product.Response;
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
        [ProducesResponseType(typeof(ProductOverviewListResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListOverviews(Guid? userId = null, CancellationToken token = default)
        {
            IEnumerable<ProductOverview> products;
            if (userId.HasValue)
                products = await _productService.FindProductOverviewsByUser(userId.Value);
            else
                products = await _productService.ReadProductOverviews(token);

            var resp = new ProductOverviewListResponse
            {
                Products = products
                    .Select(p => new ProductOverviewResponse(p))
                    .ToList()
            };
            return Ok(resp);
        }

        [HttpGet("Details/{id:guid}")]
        [ProducesResponseType(typeof(ProductDetailsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDetails(Guid id, CancellationToken token = default)
        {
            var prod = await _productService.FindProductDetails(id, token);
            if (prod == null)
                return NotFound();

            var resp = new ProductDetailsResponse(prod);
            return Ok(resp);
        }

        [HttpPost]
        [HttpPut("{id:guid}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateUpdate(Guid? id, [FromBody] ProductDetailsPayload product)
        {
            if (product.Images.Count < 1)
            {
                ModelState.AddModelError("images", "at least one image needs to be passed");
                return BadRequest();
            }
            foreach (var img in product.Images)
            {
                if (!img.Id.HasValue && img.Content == null)
                {
                    ModelState.AddModelError("images", "image has either contain id to existing resource, or image contents");
                    return BadRequest();
                }
            }

            // TODO: validate if contains at least one correct image

            if (Request.Method == "PUT")
            {
                if (!id.HasValue)
                    return BadRequest();

                var exEnt = await _productService.FindProductDetails(id.Value);
                if (exEnt == null)
                    return NotFound();
                // TODO: check if owns
            }
            else
            {
                id = Guid.NewGuid();
            }

            // TODO: retrieve user
            var user = (User)null!;
            var prod = new ProductDetails()
            {
                Id = id.Value,
                Name = product.Name,
                Seller = user,
                Price = product.Price,
                Description = product.Description,
                Images = product.Images.Select(p =>
                        new ProductImage
                        {
                            Id = p.Content != null ? Guid.NewGuid() : p.Id!.Value,
                            Content = p.Content,
                            Order = p.Order
                        }
                    )
                    .ToList()
            };

            // TODO: prepare icon image from lowest-order image

            await _productService.CreateUpdate(prod);

            var resp = new ProductDetailsPayload(prod);
            foreach (var img in product.Images)
                img.Content = null;
            return Ok(resp);
        }
    }
}
