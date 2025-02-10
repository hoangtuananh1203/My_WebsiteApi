using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using My_WebsiteApi.Data;

namespace My_WebsiteApi.Model
{
    public class Item_dsLoveModel
    {
        [Key]
        public int Id_item_dslove { get; set; }    
        public int Id_dslove { get; set; }
        public int Id_sanpham { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime Ngay_add { get; set; }




    }
}
