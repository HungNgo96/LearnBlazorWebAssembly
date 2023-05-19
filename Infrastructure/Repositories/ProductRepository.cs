using Application.Interfaces.Repositories;
using Domain.Entity;
using Infrastructure.DbContexts;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Shared.Requests.Products;

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
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<Product> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id.Equals(id), cancellationToken);
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

        public async Task<(int, string)> UpdateProductAsync(Product request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken);

            if (product is null)
            {
                return (0, "Không tồn tại sản phẩm.");
            }

            product.Name = request.Name;
            product.Supplier = request.Supplier;
            product.Price = request.Price;
            product.ImageUrl = request.ImageUrl;

            int isUpdate = await _context.SaveChangesAsync(cancellationToken);

            return isUpdate > 0
                ? (isUpdate, "Cập nhật thành công.")
                : (isUpdate, "Cập nhật thất bại.");
        }

        public async Task<(int, string)> DeleteProductAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id.Equals(id), cancellationToken);

            if (product is null)
            {
                return (0, "Không tồn tại sản phẩm.");
            }

            _ = _context.Products.Remove(product);

            int isDelete = await _context.SaveChangesAsync(cancellationToken);

            return isDelete > 0
                ? (isDelete, "Xoá thành công.")
                : (isDelete, "Xoá thất bại.");
        }
    }
}