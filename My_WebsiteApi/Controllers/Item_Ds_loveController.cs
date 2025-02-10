using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_WebsiteApi.Data;
using My_WebsiteApi.Model;
using System.Security.Claims;

namespace My_WebsiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Item_Ds_loveController : ControllerBase
    {
        private readonly MyDbcontext _context;

        public Item_Ds_loveController(MyDbcontext context)
        {
            _context = context;
        }
        [HttpPost]
        [Authorize(Roles = $"{PhanQuyen.Custommer},{PhanQuyen.Admin}")]
        public IActionResult CreateItemDs(Item_dsLoveModel model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
            if (userId == null)
            {
                return Unauthorized(new { message = "Người dùng chưa đăng nhập hoặc không hợp lệ." });

            }
            else
            {
                var dslove = _context.danhsach_Loves.SingleOrDefault(p => p.UserId == userId); // tim ds lop cuar nguoi dung

                if (dslove != null)
                {
                    var checksame = _context.item_DsLoves.SingleOrDefault(p => p.Id_sanpham == model.Id_sanpham && p.Id_dslove == dslove.Id_dslove);
                    if (checksame != null) {
                        return Ok(new { message = "Sản phẩm này đã có trong danh sách yêu thích của bạn." });
                    }
                    var item = new Item_dsLove
                    {
                        Id_dslove = dslove.Id_dslove,
                        Id_sanpham = model.Id_sanpham,
                        Ngay_add = model.Ngay_add,
                    };
                    _context.Add(item);
                    _context.SaveChanges();
                    return Ok(item);
                    
                }
                else
                {
                    var ds = new Danhsach_love
                    {
                        UserId = userId,
                    };
                    _context.Add(ds);
                    _context.SaveChanges();
                    var item = new Item_dsLove
                    {
                        Id_dslove = ds.Id_dslove,
                        Id_sanpham = model.Id_sanpham,
                        Ngay_add = model.Ngay_add,
                    };
                    _context.Add(item);
                    _context.SaveChanges();
                    return Ok(item);

                }
            }


        }

      
        [HttpGet("count")]
        public IActionResult GetCountItem()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // lay id nguoi dung
            if (userId == null)
            {
                return Unauthorized(new { message = "Người dùng chưa đăng nhập hoặc không hợp lệ." });

            }
            var dslove = _context.danhsach_Loves.SingleOrDefault(p => p.UserId == userId); // tim ds lop cuar nguoi dung co id do
            if (dslove != null)
            {
                var item = _context.item_DsLoves.Count(p => p.Id_dslove == dslove.Id_dslove);
            
              
                return Ok(item);

            }
            return NotFound(new { message = "Không có sản phẩm nào trong danh sách yêu thích" });

        }
        [HttpDelete("id")]
        [Authorize(Roles = $"{PhanQuyen.Custommer},{PhanQuyen.Admin}")]
        public IActionResult Delete(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // lay id nguoi dung
            if (userId == null)
            {
                return Unauthorized(new { message = "Người dùng chưa đăng nhập hoặc không hợp lệ." });

            }
            var dslove = _context.danhsach_Loves.SingleOrDefault(p => p.UserId == userId); // tim ds lop cuar nguoi dung co id do
            if (dslove != null)
            {
                var item = _context.item_DsLoves.SingleOrDefault(p => p.Id_sanpham == id &&p.Id_dslove==dslove.Id_dslove);
                _context.Remove(item);
                _context.SaveChanges();
                return Ok(item);
            }
                return NotFound(new { message = "Không tìm thấy sản phẩm" });
             
            
        
        }


    }
}
