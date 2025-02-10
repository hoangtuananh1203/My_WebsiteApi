using System.ComponentModel.DataAnnotations;

namespace My_WebsiteApi.Data
{
    public class Website_infomation

    {
        [Key]
        public int Id { get; set; }
        [Required]
        [EmailAddress(ErrorMessage ="Vui lòng nhập đúng định dạng!")]
        public string email { get; set; }
        [Required]
        [StringLength(100)]
        public string diachi { get; set; }
        [Required]
        public string sdt { get; set; }
        [Required]
        public string tencongty { get; set; }
		[Required]
		public string mota { get; set; }



	}
}
