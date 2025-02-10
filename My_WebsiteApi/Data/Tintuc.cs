using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_WebsiteApi.Data
{
    public class Tintuc
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Tieude { get; set; }
        [Required]
        [StringLength (500)]
        public string noidung { get; set; }
        [Required]
        [StringLength (50)]
        public string image { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime ngaythem { get; set; }  
    }
    public class TintucModel
    {
        
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Tieude { get; set; }
        [Required]
        [StringLength(500)]
        public string noidung { get; set; }
        [Required]
        [StringLength(50)]
        public string image { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime ngaythem { get; set; }
    }
}
