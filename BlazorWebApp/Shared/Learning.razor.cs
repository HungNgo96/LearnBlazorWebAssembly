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


        [Parameter]
        public RenderFragment LeaningChildContent { get; set; }

        public override Task SetParametersAsync(ParameterView parameters)
        {
            Console.WriteLine($"SetParametersAsync => parameters: {JsonConvert.SerializeObject(parameters)}");
            return base.SetParametersAsync(parameters);
        }

        protected override Task OnInitializedAsync()
        {
            Console.WriteLine($"OnInitialized => Title: {Title}, CurrentCount: {CurrentCount}");
            return base.OnInitializedAsync();
        }
        protected override Task OnParametersSetAsync()
        {
            Console.WriteLine($"OnParameterSet => Title: {Title}, CurrentCount: {CurrentCount}");
            return base.OnParametersSetAsync();
        }
        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Console.WriteLine("This is the first render of the component");
            }
            else
            {
                Console.WriteLine("This is the render of the component");
            }

            return base.OnAfterRenderAsync(firstRender);
        }
        protected override bool ShouldRender()
        {
            //return false;
            return base.ShouldRender();//dèault truek
        }
    }
}
