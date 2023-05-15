using ApplicationClient.Interfaces;
using ApplicationClient.Ultilities;
using Microsoft.Extensions.DependencyInjection;
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

            var response = await http.GetAsync("api/Products/Get");
            var content = await response.Content.ReadAsStreamAsync();

            if (!response.IsSuccessStatusCode)
            {
                return await Result<IEnumerable<ProductResponse>>.FailAsync(message: response.RequestMessage.ToString());
            }
            var resultStream = await JsonSerializer.DeserializeAsync<Result<List<ProductResponse>>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return await Result<IEnumerable<ProductResponse>>.SuccessAsync(data: resultStream.Data);
        }
    }
}
