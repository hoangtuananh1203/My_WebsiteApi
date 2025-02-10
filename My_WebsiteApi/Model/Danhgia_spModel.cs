using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;
using My_WebsiteApi.Data;

namespace My_WebsiteApi.Model
{
    public class Danhgia_spModel
    {

        [Key]
        public int Id_danhgia { get; set; }
        [Required]
        public int Diem { get; set; }
        [Required]
        [StringLength(300)]
        public string noidung { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime Ngay_add { get; set; }
        public string UserId { get; set; }
        public int Id_sanpham { get; set; }


       

    }
}
