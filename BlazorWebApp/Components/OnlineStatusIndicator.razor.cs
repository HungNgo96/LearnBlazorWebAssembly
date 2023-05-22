using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorWebApp.Components
{
    public partial class OnlineStatusIndicator
    {
        private string _color;
        private int currentCount = 0;
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var dotNetObjRef = DotNetObjectReference.Create(this);
                await JSRuntime.InvokeVoidAsync("jsFunctions.registerOnlineStatusHandler", dotNetObjRef);
            }
            Console.WriteLine("OnlineStatusIndicatorOnAfterRenderAsync::" + firstRender);
        }

        [JSInvokable]
        public void SetOnlineStatusColor(bool isOnline)
        {
            _color = isOnline ? "green" : "red";
            StateHasChanged();
        }

        private async Task IncrementCount()
        {
            currentCount++;
        }
    }
}
