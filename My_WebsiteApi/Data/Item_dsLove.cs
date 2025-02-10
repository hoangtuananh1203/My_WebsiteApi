using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace My_WebsiteApi.Data
{
    public class Item_dsLove
    {
        [Key]
        public int Id_item_dslove { get; set; }
        [ForeignKey("madanhsach")]
        public int Id_dslove { get; set; }
        public Danhsach_love? madanhsach { get; set; }
        //fk_key
		[ForeignKey("masp")]
        public int Id_sanpham { get; set; }
        public Sanpham? masp { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime Ngay_add { get; set; }
    }
}
