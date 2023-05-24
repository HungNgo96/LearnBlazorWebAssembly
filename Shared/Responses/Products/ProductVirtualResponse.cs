namespace Shared.Responses.Products
{
    public class ProductVirtualResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int ItemNumber { get; set; }
        public string Supplier { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
