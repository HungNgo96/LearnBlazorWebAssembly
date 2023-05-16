using ApplicationClient.Interfaces;
using ApplicationClient.Responses;
using Shared.Helper;
using Shared.Model.Http;
using ApplicationClient.Responses;
using Shared.Wrapper;

namespace ApplicationClient.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IResult<IEnumerable<ProductResponse>>> GetProductsAsync()
        {
           var http =  _httpClientFactory.CreateClient("ProductsAPI");

            (var result, var errorModel) = await CallApi<string, BaseResponse<List<ProductResponse>>>.GetAsJsonAsync(null!, http.BaseAddress.ToString(), "api/Products/Get", new() { Client = http }, default);

            if (!errorModel.Succeeded)
            {
                return await Result<IEnumerable<ProductResponse>>.FailAsync(message: errorModel.Message ?? string.Empty);
            }

            if (!result.Succeeded)
            {
                return await Result<IEnumerable<ProductResponse>>.FailAsync(messages: result.Messages ?? new List<string>());
            }
            return await Result<IEnumerable<ProductResponse>>.SuccessAsync(data: result.Data);
        }
    }
}
