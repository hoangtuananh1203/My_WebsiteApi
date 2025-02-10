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
    public class GiohangController : ControllerBase
    {
        private readonly MyDbcontext _context;

        public GiohangController(MyDbcontext context)
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
                var giohang = _context.giohang.SingleOrDefault(p => p.UserId == userId); // tim ds lop cuar nguoi dung co id do
                var danhMucs = _context.danhmucs.ToList();
                var hangSxes = _context.hangsxes.ToList();
                var culture = new CultureInfo("vi-VN");

                if (giohang != null)
                {
                    var item = _context.item_Giohangs.Where(p => p.Id_giohang == giohang.Id_giohang).ToList();
                    var listsp = new List<SanphamModelName> { };
                    foreach (var ss in item)
                    {
                        var soluong = ss.soluong;
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
                                soluong = soluong,
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
        [HttpGet]
        public IActionResult GetCount()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new { message = "Bạn chưa đăng nhâp!" });
            }

            var giohang = _context.giohang.SingleOrDefault(p => p.UserId == userId);
            var danhMucs = _context.danhmucs.ToList();
            var hangSxes = _context.hangsxes.ToList();
            var culture = new CultureInfo("vi-VN");
            if (giohang == null)
            {
                return NotFound(new { message = "Không có sản phầm nào trong giỏ!" });
            }
            else
            {
                var item = _context.item_Giohangs.Where(p => p.Id_giohang == giohang.Id_giohang).ToList();

                var listsp = new List<SanphamModelName> { };
                foreach (var ss in item)
                {
                    var sp = _context.sanphams.SingleOrDefault(p => p.Id_sanpham == ss.Id_sanpham);
                    var tendanhm = danhMucs.SingleOrDefault(p => p.Id_danhmuc == sp.Id_danhmuc); // tim danh muc theo id 
                    var tenhang = hangSxes.SingleOrDefault(p => p.Id_hangsx == sp.Id_hangsx); // tim 
                    if (sp != null)
                    {

                        listsp.Add(new SanphamModelName
                        {
                            Id_sanpham = sp.Id_sanpham,
                            Name_sanpham = sp.Name_sanpham,
                            tenDM = tendanhm.Ten_danhmuc,
                            tenHang = tenhang.Ten_hangsx,
                            mota_sp = sp.mota_sp,
                            Mausac = sp.Mausac,
                            Loai = sp.Loai,
                            gia = sp.gia.ToString("N0", culture),
                            soluong = sp.soluong,
                            image1 = sp.image1,
                            image2 = sp.image2,
                            Ngay_add = sp.Ngay_add,
                            Ngay_update = sp.Ngay_update,
                        });

                    }

                }



                return Ok(listsp);
            }
        }
        [HttpGet("Tinhtoan")]
        [Authorize(Roles = $"{PhanQuyen.Custommer},{PhanQuyen.Admin}")]
        public IActionResult GetTtGio()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("Bạn chưa đăng nhâp!");
            }

            var giohang = _context.giohang.SingleOrDefault(p => p.UserId == userId);
            if (giohang == null)
            {
                return NotFound("Không có sản phầm nào trong giỏ!");
            }
            else
            {
                var sanpham = _context.item_Giohangs.Where(p => p.Id_giohang == giohang.Id_giohang).ToList();
                Decimal giatritronggio = 0;
                foreach (var i in sanpham)
                {
                    var sanphammain = _context.sanphams.SingleOrDefault(p => p.Id_sanpham == i.Id_sanpham);// san pham trong gio
                    var sl_cuasp = sanpham.Where(p => p.Id_sanpham == i.Id_sanpham).Sum(p => p.soluong);
                    var giasanpham = sanphammain.gia;
                    giatritronggio = giatritronggio + (Decimal)sl_cuasp * giasanpham;
                }



                var tongsoluong = sanpham.Sum(p => p.soluong);

                return Ok(new
                {

                    tongsoluong = tongsoluong,
                    giatri = giatritronggio,
                });
            }
        }
        [HttpPost]
        [Authorize(Roles = $"{PhanQuyen.Custommer},{PhanQuyen.Admin}")]
        public IActionResult Create()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new { message = "Bạn chưa đăng nhập vào hệ thống!" });
            }
            var giohang = _context.giohang.SingleOrDefault(p => p.UserId == userId);
            if (giohang == null)
            {
                var gh = new Giohang
                {
                    UserId = userId,
                };
                _context.Add(gh);
                _context.SaveChanges();
               return Ok(new {message="ok"});

            }
            else
            {
                return Ok(new { message = "ok" });
            }

        }
    }
}
