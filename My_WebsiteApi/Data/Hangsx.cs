using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_WebsiteApi.Data
{
    public class Hangsx
    {
        [Key]
        public int Id_hangsx { get; set; }
        [Required]
        [StringLength(50)]
        public string Ten_hangsx { get; set; }
 
        [StringLength(250)]
        public string? Diachi_hangsx { get; set; }
       
        [StringLength(500)]
        public string? Mota_hangsx { get; set; }


     



    }
}
