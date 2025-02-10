using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

namespace My_WebsiteApi.Data
{
    public class Danhgia_sp
    {
        [Key]
        public int Id_danhgia { get; set; }
        [Required]
        public int Diem { get; set; }
        [Required]
        [StringLength(300)]
        public string noidung { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime Ngay_add { get; set; }
        public string UserId { get; set; } // Liên kết với người dùng
        [ForeignKey("masp")]
        public int Id_sanpham { get; set; }
        public Sanpham? masp { get; set; }
        // Navigation property
        public virtual ApplicationUser User { get; set; }

    }
  
}
