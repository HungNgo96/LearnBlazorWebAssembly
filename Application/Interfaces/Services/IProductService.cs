using Domain.Entity;
using Shared.Requests;
using Shared.Responses.Products;
using Shared.Wrapper;

namespace Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<IResult<IEnumerable<ProductResponse>>> GetProducts();
        Task<IResult<PagedList<ProductResponse>>> GetProductsAsync(ProductRequest request, CancellationToken cancellationToken);
    }
}
