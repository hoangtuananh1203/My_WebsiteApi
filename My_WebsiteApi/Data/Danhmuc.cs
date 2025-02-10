using System.ComponentModel.DataAnnotations;

namespace My_WebsiteApi.Data
{
    public class Danhmuc
    {
        [Key]
        public int Id_danhmuc { get; set; }
        [Required]
        [StringLength(50)]
        public string Ten_danhmuc { get; set; }
        
        public string? Mota_danhmuc { get; set; }
        




    }
}
