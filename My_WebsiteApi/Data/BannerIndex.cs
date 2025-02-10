using System.ComponentModel.DataAnnotations;

namespace My_WebsiteApi.Data
{
    public class BannerIndex
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Image { get; set; }
    }
    public class BannerIndexModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Image { get; set; }
    }
}
