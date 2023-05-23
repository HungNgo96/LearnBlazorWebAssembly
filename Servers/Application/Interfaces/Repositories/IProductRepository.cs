using Domain.Entity;
using Shared.Requests.Products;
using Shared.Wrapper;

namespace Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<IList<Product>> GetProductsAsync(ProductRequest request, CancellationToken cancellationToken);
        Task<bool> CreateProductAsync(Product product, CancellationToken cancellationToken);
        Task<Product> GetProductByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<(int, string)> UpdateProductAsync(Product request, CancellationToken cancellationToken);
        Task<(int, string)> DeleteProductAsync(Guid id, CancellationToken cancellationToken);
    }
}
