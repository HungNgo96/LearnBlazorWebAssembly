using ApplicationClient.Interfaces;
using ApplicationClient.Responses;
using ApplicationClient.Services;
using Microsoft.AspNetCore.Components;
using Shared.Model.Paging;
using Shared.Requests.Products;

namespace BlazorWebApp.Pages
{
    public sealed partial class ProductsVirtual : IDisposable
    {
        private CancellationTokenSource cts = new CancellationTokenSource();
        public List<ProductVirtualResponse> ProductList { get; set; } = new List<ProductVirtualResponse>();
        public MetaData MetaData { get; set; } = new MetaData();
        public int TotalSize { get; set; }
        [Inject]
        private IProductService ProductService { get; set; }
        [Inject]
        public HttpInterceptorService Interceptor { get; set; }
        [Inject]
        public ILogger<Products> Logger { get; set; }
        public void Dispose()
        {
            cts.Cancel();
            cts.Dispose();
            Interceptor.DisposeEvent();
        }

        private async Task GetProductsVirtualAsync(ProductVirtualRequest request)
        {
            var pagingResponse = await ProductService.GetProductsVirtualAsync(request, cts.Token);
            if (pagingResponse.Succeeded)
            {
                ProductList = pagingResponse.Data.Items;
                TotalSize = pagingResponse.Data.TotalSize;
            }
        }
    }
}