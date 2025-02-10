using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace My_WebsiteApi.Data
{
    public class SanphamQuangba
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title1 { get; set; }
        [Required]
        [StringLength(50)]
        public string Title2 { get; set; }
        [Required]
        [StringLength(50)]
        public string Image { get; set; }
        [Required]
        public string mota { get; set; }
    }
    public class SanphamQuangbaModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title1 { get; set; }
        [Required]
        [StringLength(50)]
        public string Title2 { get; set; }
        [Required]
        [StringLength(50)]
        public string Image { get; set; }
        [Required]
        public string mota { get; set; }
    }
}
