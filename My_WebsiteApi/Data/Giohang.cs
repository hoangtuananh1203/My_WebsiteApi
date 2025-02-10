using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace My_WebsiteApi.Data
{
    public class Giohang
    {
        [Key]
        public int Id_giohang { get; set; }
        public string UserId { get; set; } // Liên kết với người dùng
        // Navigation property
        public virtual ApplicationUser User { get; set; }
    }
}
