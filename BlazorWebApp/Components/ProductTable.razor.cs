using Microsoft.AspNetCore.Components;
using Shared.Responses;

namespace BlazorWebApp.Components
{
    public partial class ProductTable
    {
        [Parameter]
        public List<ProductResponse> Products { get; set; }
    }
}