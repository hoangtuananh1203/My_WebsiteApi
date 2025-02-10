using System.ComponentModel.DataAnnotations;

namespace My_WebsiteApi.Model
{
    public class DanhmucModel
    {
        [Key]
        public int Id_danhmuc { get; set; }
        [Required]
        [StringLength(50)]
        public string Ten_danhmuc { get; set; }
        [Required]
        public string Mota_danhmuc { get; set; }
      




    }
}
