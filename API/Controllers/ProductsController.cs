using Application.Interfaces.Services;
using Application.Ultilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.Requests;
using System.Net.Http.Headers;

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

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync(CreateProductRequest request, CancellationToken cancellationToken)
        {
            var products = await _iProductService.Value.CreateProductAsync(request, cancellationToken);
            return Ok(products);
        }

        [HttpPost]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("StaticFiles", "images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName!.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(dbPath);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}