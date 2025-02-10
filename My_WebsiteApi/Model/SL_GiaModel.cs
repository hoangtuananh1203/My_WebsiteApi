using System.ComponentModel.DataAnnotations;

namespace My_WebsiteApi.Model
{
    public class SL_GiaModel
    {
        [Required]
        public Decimal gia { get; set; }
        [Required]
        public int soluong { get; set; }
    }
}
