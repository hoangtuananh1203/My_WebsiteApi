using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using My_WebsiteApi.Data;
using My_WebsiteApi.Model;
using System.Security.Claims;

namespace My_WebsiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DanhgiaController : ControllerBase
    {

        private readonly MyDbcontext _context;

        public DanhgiaController(MyDbcontext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var tt = _context.danhgia_Sps.ToList();
            return Ok(tt);
        }


        [HttpPost("id")]
        [Authorize(Roles = $"{PhanQuyen.Custommer},{PhanQuyen.Admin}")]
        public IActionResult CreateReview(int id, Danhgia_spModel model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new { message = "Vui lòng đăng nhập!" });
            }

        
            var donhang = _context.donhangs
                .Where(p => p.UserId == userId && p.trangthai == TrangthaiModel.Dagiao) // Trạng thái là Đã giao
                .ToList();

            if (!donhang.Any())
            {
                return Ok(new { message = "Bạn chưa có đơn hàng nào để đánh giá." });
            }

 
            var isProductPurchased = false;
            foreach (var items in donhang)
            {
                var itemInOrder = _context.Item_Donhangs
                    .FirstOrDefault(p => p.Id_sanpham == id && p.Id_donhang == items.Id_donhang);

                if (itemInOrder != null)
                {
                    isProductPurchased = true;
                    var existingReview = _context.danhgia_Sps
                        .FirstOrDefault(p => p.UserId == userId && p.Id_sanpham == id);

                    if (existingReview != null)
                    {
                        return Ok(new { message = "Bạn đã đánh giá sản phẩm này rồi!" });
                    }

                
                    var review = new Danhgia_sp
                    {
                        Id_sanpham = id,
                        UserId = userId,
                        Diem = model.Diem,
                        noidung = model.noidung,
                        Ngay_add = DateTime.Now
                    };

                    _context.Add(review);
                    _context.SaveChanges();
                    return Ok(new { message = "Đánh giá sản phẩm thành công!" });
                }
            }

            if (!isProductPurchased)
            {
                return Ok(new { message = "Vui lòng mua sản phẩm trước để đánh giá." });
            }

            return BadRequest(new { message = "Có lỗi xảy ra trong quá trình đánh giá." });
        }






        [HttpDelete("id")]
        [Authorize(Roles = PhanQuyen.Admin)]
        public IActionResult Delete(int id)
        {
            var tt = _context.danhgia_Sps.SingleOrDefault(tt => tt.Id_danhgia == id);
            if (tt == null)
            {
                return NotFound();
            }

            _context.Remove(tt);
            _context.SaveChanges();
            return Ok(tt);
        }

    }
}
