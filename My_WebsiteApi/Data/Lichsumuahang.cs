using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using My_WebsiteApi.Model;

namespace My_WebsiteApi.Data
{
    public class Lichsumuahang
    {
        [Key]
        public int Id_lichsu { get; set; }

        [ForeignKey("madonhang")]
        public int Id_donhang { get; set; }
        public Donhang? madonhang { get; set; }


        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime Ngaynhan { get; set; }

        [Required]
   
        public TrangthaiModel trangthai { get; set; }
       

       
    }
}
