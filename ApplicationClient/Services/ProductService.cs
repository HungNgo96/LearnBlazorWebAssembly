using ApplicationClient.Interfaces;
using ApplicationClient.Responses;
using ApplicationClient.Responses.Paging;
using Newtonsoft.Json;
using Shared.Helper;
using Shared.Model.Http;
using Shared.Model.Paging;
using Shared.Requests;
using Shared.Wrapper;
using System.Text.Json;

namespace ApplicationClient.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _options;
        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IResult<PagingResponse<ProductResponse>>> GetProductsAsync(ProductRequest request, CancellationToken cancellationToken)
        {

            Console.WriteLine("GetProductsAsync::" + JsonConvert.SerializeObject(request));
            var client = _httpClientFactory.CreateClient("ProductsAPI");

            (var result, var errorModel, var header) = await CallApi<ProductRequest, BaseResponse<List<ProductResponse>>>.GetAsJsonAndHeaderAsync(request, client.BaseAddress!.ToString(), "api/Products/GetProducts", new() { Client = client }, default);

            if (!errorModel.Succeeded)
            {
                return await Result<PagingResponse<ProductResponse>>.FailAsync(message: errorModel.Message ?? string.Empty);
            }

            if (!result!.Succeeded)
            {
                return await Result<PagingResponse<ProductResponse>>.FailAsync(messages: result.Messages ?? new List<string>());
            }
            
            var pagingResponse = new PagingResponse<ProductResponse>
            {
                Items = result.Data,
                MetaData = System.Text.Json.JsonSerializer.Deserialize<MetaData>(header.GetValues("X-Pagination").First() ?? string.Empty, _options)
            };

            return await Result<PagingResponse<ProductResponse>>.SuccessAsync(data: pagingResponse, messages: result.Messages);
        }

        public async Task<IResult<IEnumerable<ProductResponse>>> GetProductsAsync()
        {
           var http =  _httpClientFactory.CreateClient("ProductsAPI");

            (var result, var errorModel) = await CallApi<string, BaseResponse<List<ProductResponse>>>.GetAsJsonAsync(null!, http.BaseAddress!.ToString(), "api/Products/Get", new() { Client = http }, default);

            if (!errorModel.Succeeded)
            {
                return await Result<IEnumerable<ProductResponse>>.FailAsync(message: errorModel.Message ?? string.Empty);
            }

            if (!result!.Succeeded)
            {
                return await Result<IEnumerable<ProductResponse>>.FailAsync(messages: result.Messages ?? new List<string>());
            }

            return await Result<IEnumerable<ProductResponse>>.SuccessAsync(data: result.Data);
        }
    }
}
