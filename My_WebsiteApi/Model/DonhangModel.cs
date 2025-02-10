using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using My_WebsiteApi.Data;

namespace My_WebsiteApi.Model
{
    public class DonhangModel
    {
        public int Id_donhang { get; set; }
        public string UserId { get; set; } // Liên kết với người dùng
        // Navigation property
       
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime Ngay_dat { get; set; }
        [Required]
        [StringLength(100)]
        public string Nguoinhan { get; set; }
        [Required]
        [StringLength(300)]
        public string Diachi { get; set; }
        [Required]
        public string sdt { get; set; }
        [Required]
        
        public TrangthaiModel trangthai { get; set; }
        [Required]
       
        public LoaiThanhtoan type_thanhtoan { get; set; }






    }
}
