using Domain.Entity;
using Shared.Requests.Products;
using Shared.Responses.Products;
using Shared.Wrapper;

namespace Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<IResult<IEnumerable<ProductResponse>>> GetProducts();
        Task<IResult<PagedList<ProductResponse>>> GetProductsAsync(ProductRequest request, CancellationToken cancellationToken);

        Task<IResult<bool>> CreateProductAsync(CreateProductRequest request, CancellationToken cancellationToken);
        Task<IResult<ProductResponse>> GetProductByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IResult<int>> UpdateProductAsync(UpdateProductRequest request, CancellationToken cancellationToken);
        Task<IResult<int>> DeleteProductAsync(Guid id, CancellationToken cancellationToken);
    }
}
