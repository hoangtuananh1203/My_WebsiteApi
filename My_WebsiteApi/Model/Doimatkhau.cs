using System.ComponentModel.DataAnnotations;

namespace My_WebsiteApi.Model
{
    public class Doimatkhau
    {
       
        [StringLength(100)]
        public string Password { get; set; } = null!;
        [StringLength(100)]
        public string Passwordnew { get; set; } = null!;
        [StringLength(100)]
        public string ConfirmPass { get; set; } = null!;
    }
}
