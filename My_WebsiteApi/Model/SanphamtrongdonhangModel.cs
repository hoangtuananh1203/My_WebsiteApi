using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace My_WebsiteApi.Model
{
    public class SanphamtrongdonhangModel
    {
        public int Id_itemdonhang { get; set; }
        public int Id_sanpham { get; set; }
        [Required]
        [StringLength(100)]
        public string Name_sanpham { get; set; }
        [Required]
        [StringLength(300)]
        public string mota_sp { get; set; }
        [Required]
        [StringLength(20)]
        public string Mausac { get; set; }
        [Required]
        [StringLength(100)]
        public string Loai { get; set; }
        [Required]
        public Decimal gia { get; set; }
        [Required]
        public int soluonggio { get; set; }
        [Required]
        [StringLength(200)]
        public string image1 { get; set; }
        [Required]
        [StringLength(200)]
        public string image2 { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime Ngay_add { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime Ngay_update { get; set; }
        public int Id_danhmuc { get; set; }
        public int Id_hangsx { get; set; }
    }
}
