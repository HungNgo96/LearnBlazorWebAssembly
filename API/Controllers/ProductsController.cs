using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Ultilities;
using Microsoft.AspNetCore.Mvc;

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
    }
}
