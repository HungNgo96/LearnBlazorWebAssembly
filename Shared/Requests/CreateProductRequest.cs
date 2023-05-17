namespace Shared.Requests
{
    public class CreateProductRequest
    {
        public string? Name { get; set; }
        public string? Supplier { get; set; }
        public double Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
