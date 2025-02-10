using My_WebsiteApi.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace My_WebsiteApi.Model
{
    public class LichsumuahangItem
    {
        [Key]
        public int Id_donhang { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime Ngay_dat { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime Ngay_nhan { get; set; }
        [Required]
        [StringLength(100)]
        public string Nguoinhan { get; set; }
        [Required]
        [StringLength(300)]
        public string Diachi { get; set; }
        [Required]
        public string sdt { get; set; }
        [Required]
        public int soluong { get; set; }
        [Required]
        public Decimal gia { get; set; }
        [Required]
        public List<Item_donhang> item_Donhangs { get; set; }
        public TrangthaiModel trangthai { get; set; }


     
    }
}
