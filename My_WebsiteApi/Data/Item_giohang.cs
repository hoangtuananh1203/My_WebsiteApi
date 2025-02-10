using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace My_WebsiteApi.Data
{
    public class Item_giohang
    {
        [Key]
        public int Id_itemgiohang { get; set; }

        [ForeignKey("magiohang")]
        public int Id_giohang { get; set; }
        public Giohang? magiohang { get; set; }
        [ForeignKey("masp")]
        public int Id_sanpham { get; set; }
        public Sanpham? masp { get; set; }
        [Required]
        public int soluong { get; set; } 
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime Ngay_add { get; set; }


    }
}
