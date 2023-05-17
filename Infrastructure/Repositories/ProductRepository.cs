using Application.Interfaces.Repositories;
using Domain.Entity;
using Infrastructure.DbContexts;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Shared.Requests;
using Shared.Wrapper;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly BlazorWebContext _context;

        public ProductRepository(BlazorWebContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateProductAsync(Product product, CancellationToken cancellationToken)
        {
            _ = _context.AddAsync(product, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0 ;
        }

        public async Task<IEnumerable<Product>> GetProducts() => await _context.Products.ToListAsync();

        public async Task<IList<Product>> GetProductsAsync(ProductRequest request, CancellationToken cancellationToken)
        {
            var products = await _context.Products
                .Search(request.SearchTerm!)
                .Sort(request.OrderBy)
                .ToListAsync(cancellationToken);

            return products;
        }
    }
}
