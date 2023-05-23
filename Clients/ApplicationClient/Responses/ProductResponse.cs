using Domain.Entity;

namespace ApplicationClient.Responses
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Supplier { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public DateTime ManufactureDate { get; set; } = DateTime.Now;

        public ICollection<Review> Reviews { get; set; }
        public ICollection<QA> QAs { get; set; }
        public Declaration Declaration { get; set; }
    }
}
