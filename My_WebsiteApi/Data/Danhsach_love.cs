using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace My_WebsiteApi.Data
{
    public class Danhsach_love
    {
        [Key]
        public int Id_dslove { get; set; }

        public string UserId { get; set; } // Liên kết với người dùng
        // Navigation property
        public virtual ApplicationUser User { get; set; }

       


    }
}
