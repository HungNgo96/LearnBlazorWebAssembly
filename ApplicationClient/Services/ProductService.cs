using ApplicationClient.Interfaces;
using ApplicationClient.Options;
using ApplicationClient.Requests;
using ApplicationClient.Responses;
using ApplicationClient.Responses.Paging;
using Blazored.LocalStorage;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Shared.Helper;
using Shared.Model.Http;
using Shared.Model.Paging;
using Shared.Requests.Products;
using Shared.Wrapper;
using System.Text.Json;

namespace ApplicationClient.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly UrlOption _options;
        private readonly ILocalStorageService _localStorage;
        public ProductService(IHttpClientFactory httpClientFactory, IOptions<UrlOption> options, ILocalStorageService localStorageService)
        {
            _httpClientFactory = httpClientFactory;
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _options = options.Value;
            _localStorage = localStorageService;
        }

        public async Task<IResult<PagingResponse<ProductResponse>>> GetProductsAsync(ProductRequest request, CancellationToken cancellationToken)
        {

            Console.WriteLine("GetProductsAsync::" + JsonConvert.SerializeObject(request));
            var client = _httpClientFactory.CreateClient("ProductsAPI");
            string token = await _localStorage.GetItemAsync<string>("authToken",cancellationToken);
            (var result, var errorModel, var header) = await CallApi<ProductRequest, BaseResponse<List<ProductResponse>>>
                .GetAsJsonAndHeaderAsync(request, client.BaseAddress!.ToString(), _options.Products.GetProducts, new() { Client = client, Token = token }, cancellationToken);

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
                MetaData = System.Text.Json.JsonSerializer.Deserialize<MetaData>(header.GetValues("X-Pagination").First() ?? string.Empty, _jsonOptions)
            };

            return await Result<PagingResponse<ProductResponse>>.SuccessAsync(data: pagingResponse, messages: result.Messages);
        }

        public async Task<IResult<IEnumerable<ProductResponse>>> GetProductsAsync()
        {
            var http = _httpClientFactory.CreateClient("ProductsAPI");

            (var result, var errorModel) = await CallApi<string, BaseResponse<List<ProductResponse>>>
                .GetAsJsonAsync(null!, http.BaseAddress!.ToString(), _options.Products.Get, new() { Client = http }, default);

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
                .PostAsJsonAsync(request, string.Empty, _options.Products.CreateProduct, new() { Client = http }, cancellationToken);

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

            var postResult = await client.PostAsync(_options.Products.Upload, content);
            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                return await Result<string>.FailAsync(message: postResult.ReasonPhrase!);
            }
            else
            {
                var imgUrl = Path.Combine(_options.Url, postContent);
                return await Result<string>.SuccessAsync(data: imgUrl, message: postResult.ReasonPhrase!);
            }
        }

        public async Task<IResult<ProductResponse>> GetProductByIdAsync(string id, CancellationToken cancellationToken)
        {
            var http = _httpClientFactory.CreateClient("ProductsAPI");

            (var result, var errorModel) = await CallApi<string, BaseResponse<ProductResponse>>
                .GetAsJsonAsync(null!, string.Empty, $"{_options.Products.GetProductById}/{id}", new() { Client = http }, cancellationToken);

            if (!errorModel.Succeeded)
            {
                return await Result<ProductResponse>.FailAsync(message: errorModel.Message ?? string.Empty);
            }

            if (!result!.Succeeded)
            {
                return await Result<ProductResponse>.FailAsync(messages: result.Messages ?? new List<string>());
            }

            return await Result<ProductResponse>.SuccessAsync(data: result.Data);
        }

        public async Task<IResult<int>> DeleteProductAsync(Guid id, CancellationToken cancellationToken)
        {
            Console.WriteLine("ProductService DeleteProductAsync request");
            var client = _httpClientFactory.CreateClient("ProductsAPI");

            (var result, var errorModel) = await CallApi<string, BaseResponse<int?>>
                .DeleteAsJsonAsync(null!, string.Empty, $"{_options.Products.DeleteProduct}/{id}", new() { Client = client }, cancellationToken);

            if (!errorModel.Succeeded)
            {
                return await Result<int>.FailAsync(message: errorModel.Message ?? string.Empty);
            }

            if (!result!.Succeeded)
            {
                return await Result<int>.FailAsync(messages: result.Messages ?? new List<string>());
            }

            return await Result<int>.SuccessAsync(data: (int)result.Data!);
        }

        public async Task<IResult<int>> UpdateProductAsync(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient("ProductsAPI");

            (var result, var errorModel) = await CallApi<UpdateProductRequest, BaseResponse<int?>>
                .PutAsJsonAsync(request, string.Empty, _options.Products.UpdateProduct, new() { Client = client }, cancellationToken);

            if (!errorModel.Succeeded)
            {
                return await Result<int>.FailAsync(message: errorModel.Message ?? string.Empty);
            }

            if (!result!.Succeeded)
            {
                return await Result<int>.FailAsync(messages: result.Messages ?? new List<string>());
            }

            return await Result<int>.SuccessAsync(data: (int)result.Data!);
        }
    }
}
