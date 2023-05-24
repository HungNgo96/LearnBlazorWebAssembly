using ApplicationClient.Interfaces;
using ApplicationClient.Requests;
using BlazorMaterialMud.Client.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using static MudBlazor.CategoryTypes;

namespace BlazorMaterialMud.Client.Pages
{
    public sealed partial class CreateProduct : IDisposable
    {
        private CancellationTokenSource cts = new CancellationTokenSource();
        private CreateProductClientRequest _product = new CreateProductClientRequest();
        private DateTime? _date = DateTime.Today;
        //private SuccessNotification _notification;
        private bool IsInvalidSubmit { get; set; } = false;
        [Inject] private IJSRuntime _js { get; set; }

        [Inject] private IProductService _productService { get; set; }

        [Inject] private NavigationManager NavManager { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
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
                //await _js.InvokeVoidAsync("alert","Tạo phiếu thành công");
                //_notification.Show();
                await ExecuteDialog();
            }
            else
            {
                await _js.InvokeVoidAsync("alert", "Tạo phiếu thất bại");
            }
        }

        private async Task ExecuteDialog()
        {
            var parameters = new DialogParameters
        {
            { "Content", "You have successfully created a new product." },
            { "ButtonColor", Color.Primary },
            { "ButtonText", "OK" }
        };
            var dialog = DialogService.Show<DialogNotification>("Success", parameters);
            var result = await dialog.Result;
            if (!result.Canceled)
            {
                bool.TryParse(result.Data.ToString(), out bool shouldNavigate);
                if (shouldNavigate)
                    NavManager.NavigateTo("fetchdatapaging");
            }
        }

        private void AssignImageUrl(string imgUrl) => _product.ImageUrl = imgUrl;

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
