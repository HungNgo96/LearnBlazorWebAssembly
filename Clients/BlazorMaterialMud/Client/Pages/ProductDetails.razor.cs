using ApplicationClient.Interfaces;
using ApplicationClient.Responses;
using Domain.Entity;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace BlazorMaterialMud.Client.Pages
{
    public sealed partial class ProductDetails : IDisposable
    {
        private CancellationTokenSource cts = new CancellationTokenSource();
        [Inject]
        private IProductService ProductService { get; set; }
        public ProductResponse ProductDetail { get; set; } = new ProductResponse();
        [Parameter]
        public Guid ProductId { get; set; }

        private int _currentRating;
        private int _reviewCount;
        private int _questionCount;
        protected async override Task OnInitializedAsync()
        {
            Console.WriteLine("ProductDetails");
            var result = await ProductService.GetProductByIdAsync(ProductId.ToString(), cts.Token);
            Console.WriteLine("ProductDetails");
            if (result.Succeeded)
            {
                Console.WriteLine("ProductDetails::" + JsonConvert.SerializeObject(result));
                ProductDetail = result.Data;
            }
        }
        private int CalculateRating()
        {
            var rating = ProductDetail.Reviews.Any() ? ProductDetail.Reviews.Average(r => r.Rate) : 0;
            return Convert.ToInt32(Math.Round(rating));
        }
        private void RatingValueChanged(int value) =>
            Console.WriteLine($"The product is rated with {value} thumbs.");

        public ProductResponse Product { get; set; } = new ProductResponse
        {
            Reviews = new List<Review>(),
            Declaration = new Declaration(),
            QAs = new List<QA>()
        };

        public void Dispose()
        {
            cts.Cancel();
            cts.Dispose();
        }
    }
}
