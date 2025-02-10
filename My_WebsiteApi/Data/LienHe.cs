using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace My_WebsiteApi.Data
{
    public class LienHe
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string name { get; set; }
        [Required]
        [StringLength(50)]
        public string email { get; set; }
        [Required]
        [StringLength(500)]
        public string noidung { get; set; }
        [Required]
        [StringLength(20)]
        public string sdt { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime ngaythem { get; set; }
    }
    public class LienheModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string name { get; set; }
        [Required]
        [StringLength(50)]
        public string email { get; set; }
        [Required]
        [StringLength(500)]
        public string noidung { get; set; }
        [Required]
        [StringLength(20)]
        public string sdt { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime ngaythem { get; set; }
    }
}
