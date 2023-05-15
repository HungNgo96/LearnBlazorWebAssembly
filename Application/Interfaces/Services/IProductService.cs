using Domain.Entity;
using Shared.Responses;
using Shared.Wrapper;

namespace Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<IResult<IEnumerable<ProductResponse>>> GetProducts();
    }
}
