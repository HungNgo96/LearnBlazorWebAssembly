using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Ultilities;
using AutoMapper;
using Domain.Entity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.Extensions;
using Shared.Requests;
using Shared.Responses.Products;
using Shared.Wrapper;

namespace Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly LazyInstanceUtils<IProductRepository> _productRepository;
        private readonly LazyInstanceUtils<IMapper> _mapper;

        public ProductService(ILogger<ProductService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _productRepository = new LazyInstanceUtils<IProductRepository>(serviceProvider);
            _mapper = new LazyInstanceUtils<IMapper>(serviceProvider);
        }

        public async Task<IResult<IEnumerable<ProductResponse>>> GetProducts()
        {
            _logger.Request(nameof(ProductService), nameof(GetProducts));

            var products = await _productRepository.Value.GetProducts();

            _logger.Response(nameof(ProductService), nameof(GetProducts));

            return products != null
                ? await Result<IEnumerable<ProductResponse>>.SuccessAsync(data: _mapper.Value.Map<IEnumerable<ProductResponse>>(products), message: " Thành công.")
                : await Result<IEnumerable<ProductResponse>>.FailAsync(message: " Thất bại.");
        }

        public async Task<IResult<PagedList<ProductResponse>>> GetProductsAsync(ProductRequest request, CancellationToken cancellationToken)
        {
            _logger.Request(nameof(ProductService), nameof(GetProducts), requestName: nameof(ProductRequest), JsonConvert.SerializeObject(request));

            var products = await _productRepository.Value.GetProductsAsync(request, cancellationToken);

            _logger.Response(nameof(ProductService), nameof(GetProducts));
            var productRes = _mapper.Value.Map<List<ProductResponse>>(products);
           
            return products != null
                ? await Result<PagedList<ProductResponse>>.SuccessAsync(data: PagedList<ProductResponse>.ToPagedList(productRes, request.PageNumber, request.PageSize), message: " Thành công.")
                : await Result<PagedList<ProductResponse>>.FailAsync(message: " Thất bại.");
        }
    }
}
