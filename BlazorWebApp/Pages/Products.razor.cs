using ApplicationClient.Interfaces;
using Microsoft.AspNetCore.Components;
using Shared.Responses;

namespace BlazorWebApp.Pages
{
    public partial class Products
    {
        public List<ProductResponse> ProductList { get; set; } = new List<ProductResponse>();

        [Inject]
        public IProductService ProductService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var result = await ProductService.GetProductsAsync();
            if (result.Succeeded)
            {
                ProductList = result.Data.ToList();
            }
            ////just for testing
            //foreach (var product in ProductList)
            //{
            //    Console.WriteLine(product.Name);
            //}
        }
    }
}
  