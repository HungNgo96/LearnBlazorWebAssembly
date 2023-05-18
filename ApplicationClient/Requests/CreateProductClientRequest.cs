using System.ComponentModel.DataAnnotations;

namespace ApplicationClient.Requests
{
    public class CreateProductClientRequest
    {
        [Required(ErrorMessage = "Tên không được rỗng",AllowEmptyStrings = false)]
        [MaxLength(40, ErrorMessage = "Tên không quá 40 kí tự")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Nhà sản xuất không được rỗng", AllowEmptyStrings = false)]
        public string? Supplier { get; set; }
        [Required(ErrorMessage = "Giá tiền lớn 0")]
        public double Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}