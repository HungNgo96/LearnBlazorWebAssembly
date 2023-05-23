using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class QA
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string User { get; set; }

        public Guid ProductId { get; set; }
        [NotMapped]
        public Product Product { get; set; }
    }
}
