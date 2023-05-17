using Domain.Entity;
using Shared.Requests;
using Shared.Wrapper;

namespace Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<IList<Product>> GetProductsAsync(ProductRequest request, CancellationToken cancellationToken);
        Task<bool> CreateProductAsync(Product product, CancellationToken cancellationToken);
    }
}
