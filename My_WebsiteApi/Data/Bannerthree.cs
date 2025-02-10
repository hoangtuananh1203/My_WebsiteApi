using System.ComponentModel.DataAnnotations;

namespace My_WebsiteApi.Data
{
    public class Bannerthree
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(50)]
        public string Image { get; set; }
        [Required]
        public string mota { get; set; }

    }

    public class BannerthreeModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]

        public string Title { get; set; }
        [Required]
        [StringLength(50)]

        public string Image { get; set; }
        [Required]
        public string mota { get; set; }

    }

}
