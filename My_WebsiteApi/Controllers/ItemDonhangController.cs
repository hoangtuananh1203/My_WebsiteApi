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
    public class ItemDonhangController : ControllerBase
    {

        private readonly MyDbcontext _context;

        public ItemDonhangController(MyDbcontext context)
        {
            _context = context;
        }
        //[HttpGet("item")]
        //[Authorize(Roles = PhanQuyen.Custommer)]
        //public IActionResult getItem()
        //{
        //    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    if (userId == null)
        //    {
        //        return Unauthorized(" Vui lòng đăng nhập!");
        //    }
        //    var donhang = _context.donhangs.SingleOrDefault(p => p.UserId == userId && p.trangthai != TrangthaiModel.None);
        //    if (donhang == null)
        //    {
        //        return NotFound();
        //    }
        //    var item = _context.Item_Donhangs.Where(p => p.Id_donhang == donhang.Id_donhang).ToList();
        //    var list = new List<SanphamtrongdonhangModel> { };
        //    foreach (var ss in item)
        //    {
        //        var sp = _context.sanphams.SingleOrDefault(p => p.Id_sanpham == ss.Id_sanpham);
        //        list.Add(new SanphamtrongdonhangModel
        //        {
        //            Id_itemdonhang=ss.Id_itemdonhang,
                    
        //            Name_sanpham = sp.Name_sanpham,
        //            mota_sp = sp.mota_sp,
        //            Mausac = sp.Mausac,
        //            Loai = sp.Loai,
        //            gia = ss.gia,
        //            soluonggio = ss.soluong,
        //            image1 = sp.image1,
        //            image2 = sp.image2,
        //            Ngay_add = sp.Ngay_add,
        //            Ngay_update = sp.Ngay_update,
        //            Id_danhmuc = sp.Id_danhmuc,
        //            Id_sanpham = sp.Id_sanpham,


        //        });
        //    }
        //    return Ok(list);
        //}
        [HttpGet("GetTT_cbi")]
        public IActionResult GetTT1()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(" Vui lòng đăng nhập!");
            }
            var donhang = _context.donhangs.SingleOrDefault(p => p.UserId == userId && p.trangthai == TrangthaiModel.None);
            if (donhang == null)
            {
                return NotFound();
            }
            var itemdonhang = _context.Item_Donhangs.Where(p => p.Id_donhang == donhang.Id_donhang).ToList();
            var sl = 0;
            decimal tonggia = 0;
            foreach(var i in itemdonhang)
            {
                sl = sl + i.soluong;

                tonggia = tonggia + (decimal)i.gia*i.soluong;

            }
 

            var sp = new SL_GiaModel
            {
                soluong = sl,
                gia = tonggia,
            };


            return Ok(sp);
        }
        [HttpGet("GetTT_xuly")]
        public IActionResult GetTT2()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(" Vui lòng đăng nhập!");
            }
            var donhang = _context.donhangs.SingleOrDefault(p => p.UserId == userId && p.trangthai == TrangthaiModel.Dangxuly);
            if (donhang == null)
            {
                return NotFound();
            }
            var itemdonhang = _context.Item_Donhangs.Where(p => p.Id_donhang == donhang.Id_donhang).ToList();
            var sl = 0;
            decimal tonggia = 0;
            foreach (var i in itemdonhang)
            {
                sl = sl + i.soluong;

                tonggia = tonggia + (decimal)i.gia * i.soluong;

            }


            var sp = new SL_GiaModel
            {
                soluong = sl,
                gia = tonggia,
            };



            return Ok(sp);
        }
        [HttpPost]
        [Authorize(Roles = $"{PhanQuyen.Custommer},{PhanQuyen.Admin}")]
        public IActionResult CreateItemInDonhang(Item_donhangModel model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("Vui lòng đăng nhập!");
            }
            var giasp = _context.sanphams.SingleOrDefault(p => p.Id_sanpham == model.Id_sanpham);
            if (giasp == null)
            {
                return NotFound("Sản phẩm không tồn tại!");
            }

          
            var donhangsToDelete = _context.donhangs
                .Where(p => p.UserId == userId && p.trangthai == TrangthaiModel.None)
                .ToList();

            foreach (var dh in donhangsToDelete)
            {
                var itemsToDelete = _context.Item_Donhangs
                    .Where(p => p.Id_donhang == dh.Id_donhang)
                    .ToList();
                _context.Item_Donhangs.RemoveRange(itemsToDelete);
                _context.donhangs.Remove(dh); 
            }

            _context.SaveChanges(); 

            // Tạo đơn hàng mới
            var newDonhang = new Donhang
            {
                Ngay_dat = DateTime.Now,
                UserId = userId,
                trangthai = TrangthaiModel.None, // Trạng thái mới là None
                Nguoinhan = "",
                Diachi = "",
                sdt = "",
                type_thanhtoan = LoaiThanhtoan.toankhinhan,
            };

            _context.donhangs.Add(newDonhang);
            _context.SaveChanges();

            // Thêm sản phẩm vào đơn hàng mới
            var newItem = new Item_donhang
            {
                Id_sanpham = model.Id_sanpham,
                Id_donhang = newDonhang.Id_donhang,
                soluong = 1, // Số lượng mặc định là 1 cho nút mua ngay
                gia = giasp.gia,
            };

            _context.Item_Donhangs.Add(newItem);
            _context.SaveChanges();

            return Ok(new { message = "Đã thêm sản phẩm vào đơn hàng mới thành công!" });
        }

        [HttpPut("Updateitem")]
        [Authorize(Roles = $"{PhanQuyen.Custommer},{PhanQuyen.Admin}")]
        public IActionResult UpdateItemInDonhang(int id,Item_donhangModel model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(" Vui lòng đăng nhập!");
            }
            var donhang = _context.donhangs.FirstOrDefault(p => p.UserId == userId && p.trangthai == TrangthaiModel.None);
            if (donhang == null)
            {
                return Ok(new {message= "Không tìm thấy đơn hàng của bạn" });
            }
        
            var item = _context.Item_Donhangs.SingleOrDefault(p => p.Id_itemdonhang == id && p.Id_donhang == donhang.Id_donhang);
            if (item == null)
            {
                return BadRequest(new { message = "Không tìm thấy sản phẩm trong đơn hàng." });
            }

            if (model.soluong <= 0)
            {
                _context.Remove(item);
                _context.SaveChanges();
                return Ok(item);
            }         
          item.soluong= model.soluong;
      
            _context.Update(item);
            _context.SaveChanges();
            return Ok(item);
        }
        [HttpDelete("deleteItem")]
        [Authorize(Roles = $"{PhanQuyen.Custommer},{PhanQuyen.Admin}")]
        public IActionResult DeleteItemInDonhang(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(" Vui lòng đăng nhập!");
            }
            var donhang = _context.donhangs.FirstOrDefault(p => p.UserId == userId && p.trangthai == TrangthaiModel.None);
            if (donhang == null) {
                return NotFound(new {message= "Không tìm thấy đơn hàng của bạn" });
            }
            var item = _context.Item_Donhangs.SingleOrDefault(p => p.Id_itemdonhang == id && p.Id_donhang == donhang.Id_donhang);


            if (item == null)
            {
                return NotFound(new {message= "khong tim thay don hang" });
            }
            _context.Remove(item);
            _context.SaveChanges();
            return Ok(new {message="Xoa thành công!"});
        }

















    }
}
