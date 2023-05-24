using ApplicationClient.Responses;
using Domain.Entity;
using Microsoft.AspNetCore.Components;

namespace BlazorMaterialMud.Client.Pages
{
    public partial class AdditionalProductInfo
    {
        [Parameter]
        public ProductResponse ProductInfo { get; set; }
        [Parameter]
        public int ReviewCount { get; set; }
        [Parameter]
        public int QuestionCount { get; set; }
    }
}
