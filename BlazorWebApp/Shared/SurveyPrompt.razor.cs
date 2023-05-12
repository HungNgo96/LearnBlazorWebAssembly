using Microsoft.AspNetCore.Components;

namespace BlazorWebApp.Shared
{
    public partial class SurveyPrompt
    {
        // Demonstrates how a parent component can supply parameters
        [Parameter]
        public string? Title { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> ImageAttr1 { get; set; }

        [CascadingParameter(Name = "CasName")]
        public string CascadingValueDemo { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}