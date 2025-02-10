using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace My_WebsiteApi.Data

{
    public class Item_donhang
    {
        [Key]
        public int Id_itemdonhang { get; set; }

        [ForeignKey("madonhang")]
        public int Id_donhang { get; set; }
        public Donhang? madonhang { get; set; }
        [ForeignKey("masp")]
        public int Id_sanpham { get; set; }
        public Sanpham? masp { get; set; }
        [Required]
        public int soluong { get; set; }
        [Required]
        public Decimal gia { get; set; }
    }
}
