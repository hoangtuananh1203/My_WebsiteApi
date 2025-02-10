using My_WebsiteApi.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace My_WebsiteApi.Model
{
    public class SanphamModelName
    {
        [Key]
        public int Id_sanpham { get; set; }
        [Required]
        [StringLength(100)]
        public string Name_sanpham { get; set; }
        public string tenDM { get; set; }
        public string tenHang { get; set; }
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
        [NotMapped] // Không ánh xạ với cột trong cơ sở dữ liệu
        public string gia { get; set; }
        [Required]
        public int soluong { get; set; }
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

    }
}
