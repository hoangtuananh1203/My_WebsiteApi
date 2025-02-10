using System.ComponentModel.DataAnnotations;

namespace My_WebsiteApi.Model
{
    public class SigInModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [StringLength(100)]
        public string Password { get; set; } = null!;
   

    }
}
