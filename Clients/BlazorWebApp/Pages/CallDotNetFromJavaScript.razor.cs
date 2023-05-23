using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace BlazorWebApp.Pages
{
    public partial class CallDotNetFromJavaScript
    {
        private MouseCoordinates _coordinates = new MouseCoordinates();

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [JSInvokable]
        public static string CalculateSquareRoot(int number)
        {
            var result = Math.Sqrt(number);
            return $"The square root of {number} is {result}";
        }

        [JSInvokable("CalculateSquareRootWithJustResult")]
        public static string CalculateSquareRoot2(int number)
        {
            var result = Math.Sqrt(number);
            return $"The square root of {number} is {result}";
        }

        private async Task SendDotNetInstanceToJS()
        {
            var dotNetObjRef = DotNetObjectReference.Create(this);
            Console.WriteLine("dotNetObjRef::" + JsonConvert.SerializeObject(dotNetObjRef));
            await JSRuntime.InvokeVoidAsync("jsFunctions.registerMouseCoordinatesHandler", dotNetObjRef);
        }

        [JSInvokable]
        public void ShowCoordinates(MouseCoordinates coordinates)
        {
            _coordinates = coordinates;
            StateHasChanged();
        }
    }

    public class MouseCoordinates
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}