using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_WebsiteApi.Data;
using My_WebsiteApi.Model;
using System.Globalization;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace My_WebsiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Danhsach_loveController : ControllerBase
    {
        private readonly MyDbcontext _context;

        public Danhsach_loveController(MyDbcontext context)
        {
            _context = context;
        }
        [HttpGet("GetItem")]
     
        public IActionResult GetAllItem()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // lay id nguoi dung
            if (userId == null)
            {
                return Unauthorized(new { message = "Người dùng chưa đăng nhập hoặc không hợp lệ." });

            }
            else
            {
                var dslove = _context.danhsach_Loves.SingleOrDefault(p => p.UserId == userId); // tim ds lop cuar nguoi dung co id do
                var danhMucs = _context.danhmucs.ToList();
                var hangSxes = _context.hangsxes.ToList();
                var culture = new CultureInfo("vi-VN");

                if (dslove != null)
                {
                    var item = _context.item_DsLoves.Where(p => p.Id_dslove == dslove.Id_dslove).ToList();
                    var listsp = new List<SanphamModelName> { };
                    foreach (var ss in item)
                    {
                        var sanpham = _context.sanphams.SingleOrDefault(p => p.Id_sanpham == ss.Id_sanpham);
                        var tendanhm = danhMucs.SingleOrDefault(p => p.Id_danhmuc == sanpham.Id_danhmuc); // tim danh muc theo id 
                        var tenhang = hangSxes.SingleOrDefault(p => p.Id_hangsx == sanpham.Id_hangsx); // tim 

                        if (sanpham != null)
                        {
                            listsp.Add(new SanphamModelName
                            {
                                Id_sanpham = sanpham.Id_sanpham,
                                Name_sanpham = sanpham.Name_sanpham,
                                tenDM = tendanhm.Ten_danhmuc,
                                tenHang = tenhang.Ten_hangsx,
                                mota_sp = sanpham.mota_sp,
                                Mausac = sanpham.Mausac,
                                Loai = sanpham.Loai,
                                gia = sanpham.gia.ToString("N0", culture),
                                soluong = sanpham.soluong,
                                image1 = sanpham.image1,
                                image2 = sanpham.image2,
                                Ngay_add = sanpham.Ngay_add,
                                Ngay_update = sanpham.Ngay_update,
                            });
                        }

                    }



                    return Ok(listsp);

                }
            }
            return NotFound(new { message = "Không có sản phẩm nào trong danh sách yêu thích" });

        }
        [HttpPost]
        [Authorize(Roles = $"{PhanQuyen.Custommer},{PhanQuyen.Admin}")]
        public IActionResult Create( )
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new { message = "Bạn chưa đăng nhập vào hệ thống!" });
            }
            var giohang = _context.danhsach_Loves.SingleOrDefault(p => p.UserId == userId);
            if (giohang == null)
            {
                var gh = new Danhsach_love
                {
                    UserId = userId,
                };
                _context.Add(gh);
                _context.SaveChanges();
                return Ok(new { message = "ok" });

            }
            else
            {
                return Ok(new { message = "ok" });
            }

        }

    }
}
