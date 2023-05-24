using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class Declaration
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public string Origin { get; set; }
        public string CustomerRights { get; set; }

        public Guid ProductId { get; set; }
        [NotMapped]
        public Product Product { get; set; }
    }
}
