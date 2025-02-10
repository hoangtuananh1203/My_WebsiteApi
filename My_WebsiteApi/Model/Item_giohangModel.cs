using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using My_WebsiteApi.Data;

namespace My_WebsiteApi.Model
{
    public class Item_giohangModel
    {
        [Key]
        public int Id_itemgiohang { get; set; }
        public int Id_giohang { get; set; }
        public int Id_sanpham { get; set; }
        [Required]
        public int soluong { get; set; }
       
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime Ngay_add { get; set; }

    }
}
