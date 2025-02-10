using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_WebsiteApi.Data;
using My_WebsiteApi.Model;
using System.Security.Claims;

namespace My_WebsiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Item_giohangController : ControllerBase
    {
        private readonly MyDbcontext _context;

        public Item_giohangController(MyDbcontext context)
        {
            _context = context;
        }
        [HttpGet("Count")]
        public IActionResult GetCount()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new {message= "Bạn chưa đăng nhâp!" });
            }

            var giohang = _context.giohang.SingleOrDefault(p => p.UserId == userId);
            if (giohang == null)
            {
                return NotFound(new {message= "Không có sản phầm nào trong giỏ!" });
            }
            else
            {
                var sanpham = _context.item_Giohangs.Count(p => p.Id_giohang == giohang.Id_giohang);
                return Ok(sanpham);
            }
        }
        [HttpPost]
        [Authorize(Roles = $"{PhanQuyen.Custommer},{PhanQuyen.Admin}")]
        public IActionResult CreateItem(Item_giohangModel model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new {message= "Bạn chưa đăng nhập vào hệ thống!" });
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

                var itemgh = new Item_giohang
                {
                    Id_giohang = gh.Id_giohang,
                    Id_sanpham = model.Id_sanpham,
                    soluong = model.soluong,
                
                    Ngay_add = model.Ngay_add,

                };
                _context.Add(itemgh);
                _context.SaveChanges();
                return Ok(itemgh);

            }
            else
            {
                var chechsame = _context.item_Giohangs.SingleOrDefault(p => p.Id_sanpham == model.Id_sanpham && p.Id_giohang == giohang.Id_giohang);
                DateTime date = DateTime.Now;

                if (chechsame != null)
                {
                    return Ok(new {message= "Sản phẩm đã tồn tại trong giỏ hàng!" });
                }
                var itemgh = new Item_giohang
                {
                    Id_giohang = giohang.Id_giohang,
                    Id_sanpham = model.Id_sanpham,
                    soluong = 1,
           
                    Ngay_add = date,

                };
                _context.Add(itemgh);
                _context.SaveChanges();
                return Ok(itemgh);
            }

        }
        [HttpPut("id")]
        [Authorize(Roles = $"{PhanQuyen.Custommer},{PhanQuyen.Admin}")]
        public IActionResult Update(int id, Item_giohangModel model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new {message="Vui lòng đăng nhập!"});
            }
            var giohang = _context.giohang.SingleOrDefault(p => p.UserId == userId);
          
            var finsp = _context.item_Giohangs.SingleOrDefault(p => p.Id_sanpham == id && p.Id_giohang == giohang.Id_giohang);
            var sanpham = _context.sanphams.SingleOrDefault(p => p.Id_sanpham == finsp.Id_sanpham);

            if (model.soluong <= 0)
            {
                _context.Remove(finsp);
                _context.SaveChanges();
                return Ok(finsp);
            }
            if (model.soluong >= sanpham.soluong)
            {
                return Ok(new { message = "Đạ đến giới hạn sản phẩm!" });
            }
            if (finsp == null)
            {
                return NotFound(new {message= "không tìm thấy sản phẩm!" });
            }

            finsp.soluong = model.soluong;
            _context.Update(finsp);
            _context.SaveChanges();
            return Ok(finsp);

        }
        [HttpDelete("id")]
        [Authorize(Roles = $"{PhanQuyen.Custommer},{PhanQuyen.Admin}")]
        public IActionResult Delete(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new { message = "Vui lòng đăng nhập!" });

            }
            var giohang = _context.giohang.SingleOrDefault(p => p.UserId == userId);
         
            var finsp = _context.item_Giohangs.SingleOrDefault(p => p.Id_sanpham == id && p.Id_giohang == giohang.Id_giohang);
            if (finsp == null)
            {
                return NotFound(new {message= "không tìm thấy sản phẩm" });
            }
         
            _context.Remove(finsp);
            _context.SaveChanges();
            return Ok(finsp);

        }

    }
}
