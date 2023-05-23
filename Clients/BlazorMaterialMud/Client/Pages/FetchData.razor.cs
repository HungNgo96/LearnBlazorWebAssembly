using ApplicationClient.Interfaces;
using ApplicationClient.Responses;
using Microsoft.AspNetCore.Components;
using Shared.Requests.Products;

namespace BlazorMaterialMud.Client.Pages
{
    public sealed partial class FetchData : IDisposable
    {
        private CancellationTokenSource cts = new CancellationTokenSource();
        private ProductRequest _productRequest = new ProductRequest();
        [Inject]
        private IProductService ProductService { get; set; }

        public List<ProductResponse> Products { get; set; } = new List<ProductResponse>();

        protected override async Task OnInitializedAsync()
        {
            await GetProductsAsync();
        }

        public void Dispose()
        {
            cts.Cancel();
            cts.Dispose();
        }

        private async Task GetProductsAsync()
        {
            var pagingResponse = await ProductService.GetProductsAsync();

            if (pagingResponse.Succeeded)
            {
                Products = pagingResponse.Data.ToList();
          
            }
        }
    }
}
