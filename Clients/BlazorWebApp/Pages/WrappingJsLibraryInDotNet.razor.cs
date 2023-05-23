using BlazorWasm.Toastr.Enumerations;
using BlazorWasm.Toastr.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorWebApp.Pages
{
    public partial class WrappingJsLibraryInDotNet
    {
        [Inject]
        public ToastrService ToastrService { get; set; }

        private ToastOption _toastOption = new ToastOption()
        {
            CloseButton = true,
            HideDuration = 300,
            HideMethod = ToastrHideMethod.SlideUp.ToString(),
            ShowMethod = ToastrShowMethod.SlideDown.ToString(),
            Position = ToastrPosition.BottomRight.ToString()
        };
        private async Task ShowToastrInfo()
        {
            await ToastrService.ShowInfoMessage();
        }
        private async Task ShowToastrInfo(string message, ToastOption toastOption)
        {
            await ToastrService.ShowInfoMessage(message, toastOption);
        }

    }
}
