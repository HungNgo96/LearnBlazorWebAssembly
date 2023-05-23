using Microsoft.JSInterop;

namespace BlazorWasm.Toastr.Services
{
    public class ToastrService
    {
        private readonly IJSRuntime _jsRuntime;
        public ToastrService(IJSRuntime jSRuntime)
        {
            _jsRuntime = jSRuntime;
        }
        public async Task ShowInfoMessage()
        {
            await _jsRuntime.InvokeVoidAsync("window.toastrFunctions.showToastrInfo");
        }

        public async Task ShowInfoMessage(string message)
        {
            await _jsRuntime.InvokeVoidAsync("toastrFunctions.showToastrInfoParms", message);
        }

        public async Task ShowInfoMessage(string message, ToastOption option)
        {
            await _jsRuntime.InvokeVoidAsync("toastrFunctions.showToastrInfoOptions", message, option);
        }
    }

    public class ToastOption {
        public string Position { get; set; }
        public int HideDuration { get; set; }
        public string ShowMethod { get; set; }
        public string HideMethod { get; set; }
        public bool CloseButton { get; set; }
    }

}
