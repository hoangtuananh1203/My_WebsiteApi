using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace My_WebsiteApi.Model
{
    public class Danhsach_loveModel
    {
        [Key]
        public int Id_dslove { get; set; }

        public string UserId { get; set; } // Liên kết với người dùng

        // Navigation property

        

    }
}
