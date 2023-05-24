using Application.Interfaces.Services;
using Application.Ultilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.Requests.Products;
using System.Net.Http.Headers;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    [AllowAnonymous]
    public class ProductsController : ControllerBase
    {
        private readonly LazyInstanceUtils<IProductService> _productService;

        public ProductsController(IServiceProvider serviceProvider)
        {
            _productService = new LazyInstanceUtils<IProductService>(serviceProvider);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _productService.Value.GetProducts();
            return Ok(products);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync([FromQuery] ProductRequest request, CancellationToken cancellationToken)
        {
            var products = await _productService.Value.GetProductsAsync(request, cancellationToken);

            if (products.Succeeded)
            {
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(products.Data.MetaData));
            }

            return Ok(products);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsVirtualAsync([FromQuery] ProductVirtualRequest request, CancellationToken cancellationToken)
        {
            var products = await _productService.Value.GetProductsVirtualAsync(request, cancellationToken);

            return Ok(products);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProductAsync(CreateProductRequest request, CancellationToken cancellationToken)
        {
            var products = await _productService.Value.CreateProductAsync(request, cancellationToken);
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("StaticFiles", "images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    //var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName!.Trim('"') + DateTime.Now.ToString("yyyyMMddTHHmmss");
                    var fileName = Path.GetFileNameWithoutExtension(file.FileName) + DateTime.Now.ToString("yyyyMMddTHHmmss") + System.IO.Path.GetExtension(file.FileName);
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var products = await _productService.Value.GetProductByIdAsync(id, cancellationToken);

            return Ok(products);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductAsync([FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var products = await _productService.Value.UpdateProductAsync(request, cancellationToken);

            return Ok(products);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var products = await _productService.Value.DeleteProductAsync(id, cancellationToken);

            return Ok(products);
        }
    }
}