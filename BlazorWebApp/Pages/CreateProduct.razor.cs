using ApplicationClient.Interfaces;
using ApplicationClient.Requests;
using Microsoft.AspNetCore.Components;

namespace BlazorWebApp.Pages
{
    public partial class CreateProduct : IDisposable
    {
        private CancellationTokenSource cts = new CancellationTokenSource();
        private CreateProductClientRequest _product = new CreateProductClientRequest();

        [Inject]
        public IProductService ProductService { get; set; }

        public void Dispose()
        {
            cts.Cancel();
            cts.Dispose();
        }

        private async Task Create()
        {
            await ProductService.CreateProductAsync(_product, cts.Token);
        }
    }
}
