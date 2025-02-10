using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_WebsiteApi.Model
{
    public class HangsxModel
    {
        
        public int Id_hangsx { get; set; }
        [Required]
        [StringLength(50)]
        public string Ten_hangsx { get; set; }
        [Required]
        [StringLength(250)]
        public string Diachi_hangsx { get; set; }
        [Required]
        [StringLength(500)]
        public string Mota_hangsx { get; set; }


     




    }
}
