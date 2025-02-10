using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace My_WebsiteApi.Data
{
    public class ApplicationUser:IdentityUser
    {
        public string? FirtName { get; set; } 
        public string? LastName { get; set; } 
        public string? sdt { get; set; }
        public string? diachi { get; set; }
      
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime? Ngay_sinh { get; set; }


    }
}
