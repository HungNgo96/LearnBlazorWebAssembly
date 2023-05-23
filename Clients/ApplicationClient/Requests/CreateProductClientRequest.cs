using System.ComponentModel.DataAnnotations;

namespace ApplicationClient.Requests
{
    public class CreateProductClientRequest
    {
        [Required(ErrorMessage = "Tên không được rỗng")]
        [MaxLength(40, ErrorMessage = "Tên không quá 40 kí tự")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Nhà sản xuất không được rỗng")]
        public string Supplier { get; set; }
        [Required(ErrorMessage = "Giá tiền không được rỗng")]
        [Range(5, 1000, ErrorMessage = "Price must be between 5 and 1000.")]
        public double Price { get; set; }
        public string ImageUrl { get; set; }
    }
}