using ApplicationClient.Interfaces;
using ApplicationClient.Requests;
using ApplicationClient.Responses;
using ApplicationClient.Responses.Paging;
using Newtonsoft.Json;
using Shared.Helper;
using Shared.Model.Http;
using Shared.Model.Paging;
using Shared.Requests;
using Shared.Wrapper;
using System.Text.Json;
using static System.Net.WebRequestMethods;
using System.Threading;

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

            (var result, var errorModel, var header) = await CallApi<ProductRequest, BaseResponse<List<ProductResponse>>>
                .GetAsJsonAndHeaderAsync(request, client.BaseAddress!.ToString(), "api/Products/GetProducts", new() { Client = client }, cancellationToken);

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

            (var result, var errorModel) = await CallApi<string, BaseResponse<List<ProductResponse>>>
                .GetAsJsonAsync(null!, http.BaseAddress!.ToString(), "api/Products/Get", new() { Client = http }, default);

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

        public async Task<IResult<bool>> CreateProductAsync(CreateProductClientRequest request, CancellationToken cancellationToken)
        {
            var http = _httpClientFactory.CreateClient("ProductsAPI");

            (var result, var errorModel) = await CallApi<CreateProductClientRequest, BaseResponse<bool?>>
                .PostAsJsonAsync(request, string.Empty, "api/Products/CreateProduct", new() { Client = http }, cancellationToken);

            if (!errorModel.Succeeded)
            {
                return await Result<bool>.FailAsync(message: errorModel.Message ?? string.Empty);
            }

            if (!result!.Succeeded)
            {
                return await Result<bool>.FailAsync(messages: result.Messages ?? new List<string>());
            }

            return await Result<bool>.SuccessAsync(data: (bool)result.Data!);
        }

        public async Task<IResult<string>> UploadProductImage(MultipartFormDataContent content)
        {
            var client = _httpClientFactory.CreateClient("ProductsAPI");

            var postResult = await client.PostAsync("/api/Products/upload", content);
            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                return await Result<string>.FailAsync(message: postResult.ReasonPhrase);
            }
            else
            {
                var imgUrl = Path.Combine("https://localhost:5011/", postContent);
                return await Result<string>.SuccessAsync(data: imgUrl, message: postResult.ReasonPhrase);
            }
        }
    }
}
