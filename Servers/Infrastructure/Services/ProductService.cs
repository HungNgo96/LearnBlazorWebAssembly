using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Ultilities;
using AutoMapper;
using Domain.Entity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.Extensions;
using Shared.Requests.Products;
using Shared.Responses;
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

        public async Task<IResult<bool>> CreateProductAsync(CreateProductRequest request, CancellationToken cancellationToken)
        {
            _logger.Request(nameof(ProductService), nameof(CreateProductAsync), requestName: nameof(CreateProductRequest), JsonConvert.SerializeObject(request));

            var result = await _productRepository.Value.CreateProductAsync(_mapper.Value.Map<Product>(request), cancellationToken);

            _logger.Response(nameof(ProductService), nameof(CreateProductAsync));

            return result
                ? await Result<bool>.SuccessAsync(data: result, message: "Tạo thành công.")
                : await Result<bool>.FailAsync(message: "Tạo thất bại.");
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
            _logger.Request(nameof(ProductService), nameof(GetProductsAsync), requestName: nameof(ProductRequest), JsonConvert.SerializeObject(request));

            var products = await _productRepository.Value.GetProductsAsync(request, cancellationToken);

            _logger.Response(nameof(ProductService), nameof(GetProductsAsync));
            var productRes = _mapper.Value.Map<List<ProductResponse>>(products);

            return products != null
                ? await Result<PagedList<ProductResponse>>.SuccessAsync(data: PagedList<ProductResponse>.ToPagedList(productRes, request.PageNumber, request.PageSize), message: " Thành công.")
                : await Result<PagedList<ProductResponse>>.FailAsync(message: " Thất bại.");
        }

        public async Task<IResult<ProductResponse>> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            _logger.Request(nameof(ProductService), nameof(GetProductByIdAsync));

            var product = await _productRepository.Value.GetProductByIdAsync(id, cancellationToken);

            _logger.Response(nameof(ProductService), nameof(GetProductByIdAsync));

            return product is not null
                ? await Result<ProductResponse>.SuccessAsync(data: _mapper.Value.Map<ProductResponse>(product), message: " Thành công.")
                : await Result<ProductResponse>.FailAsync(message: " Thất bại.");
        }

        public async Task<IResult<int>> UpdateProductAsync(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _logger.Request(nameof(ProductService), nameof(UpdateProductAsync));

            (var result, var message) = await _productRepository.Value.UpdateProductAsync(_mapper.Value.Map<Product>(request), cancellationToken);

            _logger.Response(nameof(ProductService), nameof(UpdateProductAsync));

            return result > 0
                ? await Result<int>.SuccessAsync(data: result, message: message)
                : await Result<int>.FailAsync(message: message);
        }

        public async Task<IResult<int>> DeleteProductAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _logger.Request(nameof(ProductService), nameof(DeleteProductAsync));

            (var result, var message) = await _productRepository.Value.DeleteProductAsync(id, cancellationToken);

            _logger.Response(nameof(ProductService), nameof(DeleteProductAsync));

            return result > 0
                ? await Result<int>.SuccessAsync(data: result, message: message)
                : await Result<int>.FailAsync(message: message);
        }
        public async Task<IResult<VirtualizeResponse<ProductVirtualResponse>>> GetProductsVirtualAsync(ProductVirtualRequest request, CancellationToken cancellationToken)
        {
            _logger.Request(nameof(ProductService), nameof(GetProductsVirtualAsync), requestName: nameof(ProductVirtualRequest), JsonConvert.SerializeObject(request));

            (var products, var total) = await _productRepository.Value.GetProductsVirtualAsync(request, cancellationToken);

            _logger.Response(nameof(ProductService), nameof(GetProductsVirtualAsync));
            var productRes = _mapper.Value.Map<List<ProductVirtualResponse>>(products);

            return products != null
                ? await Result<VirtualizeResponse<ProductVirtualResponse>>.SuccessAsync(data: new()
                {
                    Items = productRes,
                    TotalSize = total
                }, message: " Thành công.")
                : await Result<VirtualizeResponse<ProductVirtualResponse>>.FailAsync(message: " Thất bại.");
        }

    }
}