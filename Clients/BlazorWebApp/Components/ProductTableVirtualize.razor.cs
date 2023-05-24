using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components;
using Shared.Requests.Products;
using ApplicationClient.Responses;

namespace BlazorWebApp.Components
{
    public partial class ProductTableVirtualize
    {
        [Parameter]
        public List<ProductVirtualResponse> Products { get; set; }

        [Parameter]
        public int TotalSize { get; set; }

        [Parameter]
        public EventCallback<ProductVirtualRequest> OnScroll { get; set; }

        private async ValueTask<ItemsProviderResult<ProductVirtualResponse>> LoadProducts(ItemsProviderRequest request)
        {
            Console.WriteLine("request.StartIndex::" + request.StartIndex);
            var productNum = Math.Min(request.Count, TotalSize - request.StartIndex);
            await OnScroll.InvokeAsync(new ProductVirtualRequest
            {
                StartIndex = request.StartIndex,
                PageSize = productNum == 0 ? request.Count : productNum
            });

            return new ItemsProviderResult<ProductVirtualResponse>(Products, TotalSize);
        }
    }
}
