using ApplicationClient.Interfaces;
using ApplicationClient.Ultilities;
using Microsoft.Extensions.DependencyInjection;
using Shared.Helper;
using Shared.Model.Http;
using Shared.Responses;
using Shared.Wrapper;
using System.Collections;
using System.Net.Http;
using System.Text.Json;

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

            (var result, var errorModel) = await CallApi<string, BaseResponse<List<ProductResponse>>>.GetAsJsonAsync(null, http.BaseAddress.ToString(), "api/Products/Get", new() {Client = http }, default);

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
