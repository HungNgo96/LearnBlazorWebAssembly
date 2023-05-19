using ApplicationClient.Requests;
using ApplicationClient.Responses;
using ApplicationClient.Responses.Paging;
using Shared.Requests.Products;
using Shared.Wrapper;

namespace ApplicationClient.Interfaces
{
    public interface IProductService
    {
        Task<IResult<IEnumerable<ProductResponse>>> GetProductsAsync();
        Task<IResult<PagingResponse<ProductResponse>>> GetProductsAsync(ProductRequest request, CancellationToken cancellationToken);
        Task<IResult<bool>> CreateProductAsync(CreateProductClientRequest request, CancellationToken cancellationToken);

        Task<IResult<string>> UploadProductImage(MultipartFormDataContent content);
        Task<IResult<ProductResponse>> GetProductByIdAsync(string id, CancellationToken cancellationToken);
        Task<IResult<int>> DeleteProductAsync(Guid id, CancellationToken cancellationToken);
        Task<IResult<int>> UpdateProductAsync(UpdateProductRequest request, CancellationToken cancellationToken);
    }
}
