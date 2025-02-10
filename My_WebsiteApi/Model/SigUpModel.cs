using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_WebsiteApi.Model
{
    public class SigUpModel
    {

  

    public string FirtName { get; set; } 
        public string LastName { get; set; } 
        public string sdt { get; set; }
    [DataType(DataType.Date)]
    [Column(TypeName = "date")]
    public DateTime Ngay_sinh { get; set; }
        public string diachi { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        [StringLength(100)]
        public string Password { get; set; } = null!;
        [StringLength(100)]
        public string ConfirmPass { get; set; } = null!;


    }
}
