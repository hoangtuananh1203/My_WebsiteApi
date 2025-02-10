using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace My_WebsiteApi.Model
{
    public class ThanhtoanModel
    {
        [Key]
        public int Id_thanhtoan { get; set; }

      
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime Ngay_tt { get; set; }

        [Required]
        [StringLength(100)]
        public string trangthai { get; set; }
        [Required]
        public string Nguoinhan { get; set; }
        [Required]

        public Decimal tongtientt { get; set; }
        [Required]
       
        public LoaiThanhtoan type_thanhtoan { get; set; }
      
    }
}
