using ApplicationClient.Interfaces;
using ApplicationClient.Requests;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorWebApp.Pages
{
    public partial class CreateProduct : IDisposable
    {
        private CancellationTokenSource cts = new CancellationTokenSource();
        private CreateProductClientRequest _product = new CreateProductClientRequest();
        private bool IsInvalidSubmit { get; set; } = false;
        [Inject] private IJSRuntime _js { get; set; }

        [Inject] private IProductService _productService { get; set; }

        [Inject] private NavigationManager _navigationManager { get; set; }

        public void Dispose()
        {
            cts.Cancel();
            cts.Dispose();
        }

        private async Task Create()
        {
            var result = await _productService.CreateProductAsync(_product, cts.Token);
            if (result.Succeeded)
            {
                await _js.InvokeVoidAsync("alert(\"Tạo phiếu.\")");
                _navigationManager.NavigateTo("/ll/products");
            }
            else
            {
                await _js.InvokeVoidAsync("alert(\"Tạo phiếu thất bại.\")");
            }
        }

        private async Task OnInvalidSubmit()
        {
            IsInvalidSubmit = true;
        }

        private async Task OnValidSubmit()
        {
            await _js.InvokeVoidAsync("alert(\"Thông tin  hợp lệ.\")");

            IsInvalidSubmit = false;
        }
    }
}