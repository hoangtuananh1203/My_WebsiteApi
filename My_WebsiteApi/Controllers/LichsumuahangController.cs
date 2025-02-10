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
    public class LichsumuahangController : ControllerBase
    {
        private readonly MyDbcontext _context;

        public LichsumuahangController(MyDbcontext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetHistory()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new {mesage= "Vui lòng đăng nhập!" });
            }

            var donhang = _context.donhangs
                .Where(p => p.UserId == userId && (p.trangthai == TrangthaiModel.Dahuy || p.trangthai == TrangthaiModel.Dagiao))
                .ToList();

            if (!donhang.Any()) 
            {
                return Ok(new {message= "Bạn chưa có đơn hàng nào" });
            }

            var list = new List<LichsumuahangItem>();

            foreach (var i in donhang)
            {
                var ls = _context.lichsumuahangs.Where(p => p.Id_donhang == i.Id_donhang).ToList();

                foreach (var l in ls)
                {
                    var sl = _context.Item_Donhangs.Where(p => p.Id_donhang == i.Id_donhang).Sum(p => p.soluong);
                    var gia = _context.Item_Donhangs.Where(p => p.Id_donhang == i.Id_donhang).Sum(p => p.gia * p.soluong);
                    var sanpham = _context.Item_Donhangs.Where(p => p.Id_donhang == i.Id_donhang).ToList();
                    
                    var ite = ls.SingleOrDefault(p => p.Id_lichsu == l.Id_lichsu);
         

                    list.Add(new LichsumuahangItem
                    {
                        Ngay_dat = i.Ngay_dat,
                        Nguoinhan = i.Nguoinhan,
                        Diachi = i.Diachi,
                        sdt = i.sdt,
                        trangthai = i.trangthai,
                        gia = gia,
                        soluong = sl,
                        Ngay_nhan = ite.Ngaynhan, 
                        Id_donhang = i.Id_donhang,
                        item_Donhangs=sanpham
                    });
                }
            }

            return Ok(list);
        }

    }
}















    

