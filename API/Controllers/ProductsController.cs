using Application.Interfaces.Services;
using Application.Ultilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.Requests;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly LazyInstanceUtils<IProductService> _iProductService;

        public ProductsController(IServiceProvider serviceProvider)
        {
            _iProductService = new LazyInstanceUtils<IProductService>(serviceProvider);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _iProductService.Value.GetProducts();
            return Ok(products);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync([FromQuery] ProductRequest request, CancellationToken cancellationToken)
        {
            var products = await _iProductService.Value.GetProductsAsync(request, cancellationToken);

            if (products.Succeeded)
            {
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(products.Data.MetaData));
            }

            return Ok(products);
        }
    }
}