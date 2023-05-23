using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace BlazorWebApp.Shared
{
    public partial class Learning
    {
        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public int CurrentCount { get; set; }
        public Dictionary<string, object> ImageAttr1 { get; set; } = new Dictionary<string, object>
        {
            { "src", "images/1083382.jpg" },
            { "alt", "products image for the Home component" },
            { "height", "200px" },
            { "width", "200px" },
        };

        [Inject] private ILogger<Learning> Logger { get; set; }
        [Parameter]
        public RenderFragment LeaningChildContent { get; set; }

        protected override Task OnInitializedAsync()
        {
            Logger.LogInformation($"1. Learning - OnInitialized => Title: {Title}, CurrentCount: {CurrentCount}");
            return base.OnInitializedAsync();
        }

        public override Task SetParametersAsync(ParameterView parameters)
        {
            Logger.LogInformation($"2.1 Learning - SetParametersAsync => parameters: {JsonConvert.SerializeObject(parameters)}");
            return base.SetParametersAsync(parameters);
        }

        protected override Task OnParametersSetAsync()
        {
            Logger.LogInformation($"2.2 Learning - OnParameterSet => Title: {Title}, CurrentCount: {CurrentCount}");
            return base.OnParametersSetAsync();
        }
        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Logger.LogInformation("3.1 Learning - OnAfterRenderAsync:: This is the first render of the component");
            }
            else
            {
                Logger.LogInformation("3.2 Learning - OnAfterRenderAsync:: This is the render of the component");
            }

            return base.OnAfterRenderAsync(firstRender);
        }
        protected override bool ShouldRender()
        {
            Logger.LogInformation("4. Learning - ShouldRender:: Render");
            //return false;
            return base.ShouldRender();//dèault truek
        }

        private void ReRender()
        {
            Logger.LogInformation("5. Learning - ReRender");
            StateHasChanged();
        }
    }
}
