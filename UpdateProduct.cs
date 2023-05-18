using ApplicationClient.Interfaces;
using Microsoft.AspNetCore.Components;
using Shared.Requests;

namespace BlazorWebApp.Pages
{
    public partial class UpdateProduct : IDisposable
    {
        private CancellationTokenSource cts = new CancellationTokenSource();
        private UpdateProductRequest _product = new UpdateProductRequest();
        private SuccessNotification _notification;

        [Inject]
        IProductService ProductService { get; set; }
        [Parameter]
        public Guid Id { get; set; }

        protected async override Task OnInitializedAsync()
        {
            var result = await ProductService.GetProductsAsync(Id, cts.Token);

            if (!result.Succeeded)
            {
                var data = result.Data;
                _product = new UpdateProductRequest()
                {
                    Id = data.Id,
                    Name = data.Name,
                    Price = data.Price,
                    Supplier = data.Supplier,
                    ImageUrl = data.ImageUrl,
                };
            }
            
        }

        private async Task Update()
        {
            await ProductService.UpdateProductAsync(_product, cts.Token);
            _notification.Show();
        }

        private void AssignImageUrl(string imgUrl) => _product.ImageUrl = imgUrl;

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
