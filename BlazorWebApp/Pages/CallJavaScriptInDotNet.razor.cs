using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorWebApp.Pages
{
    public partial class CallJavaScriptInDotNet
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        private IJSObjectReference _jsModule;

        protected override async Task OnInitializedAsync()
        {

        }

        public async Task ShowAlertWindow()
        {
            await _jsModule.InvokeVoidAsync("showAlert", "JS function called from .NET");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./scripts/jsExamples.js");
            }
        }
    }
}
