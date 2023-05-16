using ApplicationClient.Responses;
using Shared.Wrapper;

namespace ApplicationClient.Interfaces
{
    public interface IProductService
    {
        Task<IResult<IEnumerable<ProductResponse>>> GetProductsAsync();
    }
}
