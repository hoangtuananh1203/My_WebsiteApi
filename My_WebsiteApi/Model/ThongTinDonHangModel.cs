using My_WebsiteApi.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_WebsiteApi.Model
{
    public class ThongTinDonHangModel
    {
        public int Id_donhang { get; set; }
        public string UserId { get; set; } // Liên kết với người dùng
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
        [Required]
        public int soluong { get; set; }
        [Required]
        public Decimal gia { get; set; }
        public List<Item_donhangTTModel> item_donhang { get; set; }
    }
}
