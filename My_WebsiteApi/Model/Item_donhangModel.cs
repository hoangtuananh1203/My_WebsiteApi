using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using My_WebsiteApi.Data;

namespace My_WebsiteApi.Model

{
    public class Item_donhangModel
    {
        [Key]
        public int Id_itemdonhang { get; set; }  
        public int Id_donhang { get; set; } 
        public int Id_sanpham { get; set; }
        [Required]
        public int soluong { get; set; }
        [Required]
        public Decimal gia { get; set; }

    }

    public class Item_donhangTTModel
    {
        [Key]
        public int Id_itemdonhang { get; set; }
        public int Id_donhang { get; set; }
        public int Id_sanpham { get; set; }
        [Required]
        public int soluong { get; set; }
        [Required]
        public Decimal gia { get; set; }
        [Required]
        [StringLength(100)]
        public string Name_sanpham { get; set; }

    }
}
