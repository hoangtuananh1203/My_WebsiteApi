using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_WebsiteApi.Data;
using My_WebsiteApi.Model;
using System.Globalization;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Xml;

namespace My_WebsiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonhangController : ControllerBase
    {
        private readonly MyDbcontext _context;

        public DonhangController(MyDbcontext context)
        {
            _context = context;
        }
        [HttpGet("Donhangthongke")]
        public IActionResult GetCountDH()
        {
            var culture = new CultureInfo("vi-VN");
            var sanpham =_context.sanphams.ToList();
            var soluuongSP =_context.sanphams.Count();
            var danhgia = _context.danhgia_Sps.Count();
            var tongsanpham = sanpham.Sum(p => p.soluong);
            var lienhe = _context.lienHes.Count();
            var donhangtong = _context.donhangs.Count();
            var donhanghuy = _context.donhangs.Count(p => p.trangthai == TrangthaiModel.Dahuy);
            var donhangnhan = _context.donhangs.Count(p => p.trangthai == TrangthaiModel.Dagiao);
            var donhangdanggiao = _context.donhangs.Count(p => p.trangthai == TrangthaiModel.Danggiao);
            var donhangchoxyly = _context.donhangs.Count(p => p.trangthai == TrangthaiModel.Dangxuly);
            var donnhanroi = _context.donhangs.Where(p => p.trangthai == TrangthaiModel.Dagiao).ToList();
            long sum = 0;
            int banra = 0;
            foreach (var item in donnhanroi)
            {
                var itemdonhang = _context.Item_Donhangs.Where(p => p.Id_donhang==item.Id_donhang).ToList();
                foreach (var i in itemdonhang)
                {
                    sum += (int)i.gia * i.soluong;
                    banra += i.soluong;
                }

            }
            var ngayBatDauTinh = DateTime.Now.AddDays(-30);

            // Lọc các đơn hàng đã giao trong 30 ngày gần nhất
            var danhSachDonHangDaGiao = _context.donhangs
                .Where(dh => dh.trangthai == TrangthaiModel.Dagiao && dh.Ngay_dat >= ngayBatDauTinh)
                .ToList();

            long tongDoanhThu = 0;

            // Tính tổng doanh thu
            foreach (var donHang in danhSachDonHangDaGiao)
            {
                var danhSachSanPhamTrongDonHang = _context.Item_Donhangs
                    .Where(sp => sp.Id_donhang == donHang.Id_donhang)
                    .ToList();

                foreach (var sanPham in danhSachSanPhamTrongDonHang)
                {
                    tongDoanhThu += (int)sanPham.gia * sanPham.soluong;
                }
            }   
            return Ok(new
            {
                soSP = soluuongSP,
                tongsanpham = tongsanpham,
                lienhe = lienhe,
                danhgia = danhgia,
                tong = donhangtong,
                huy = donhanghuy,
                nhan = donhangnhan,
                danggiao = donhangdanggiao,
                banra=banra,
                xuly = donhangchoxyly,
                doanhthu =
                sum.ToString("N0", culture),
               donhthuthang=tongDoanhThu.ToString("N0", culture)

            });
        }
        [HttpGet("itemchuamua")]
        public IActionResult GetAllitem()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            { 
                return Unauthorized("Bạn chưa đăng nhập!");
            }
            var donhang = _context.donhangs.SingleOrDefault(p => p.UserId == userId && p.trangthai==TrangthaiModel.None);
            if (donhang == null)
            {
                return NotFound("Không có đơn hàng nào!");
            }
            var item = _context.Item_Donhangs.Where(p => p.Id_donhang == donhang.Id_donhang).ToList();
            if (item == null)
            {
                return NotFound("Không có sản phảm nào trong đơn hàng");
            }
            var list = new List<SanphamtrongdonhangModel> { };
            foreach (var ss in item)
            {
                var sp = _context.sanphams.SingleOrDefault(p => p.Id_sanpham == ss.Id_sanpham);
                list.Add(new SanphamtrongdonhangModel
                {
                    Id_itemdonhang = ss.Id_itemdonhang,
                    Name_sanpham = sp.Name_sanpham,
                    mota_sp = sp.mota_sp,
                    Mausac = sp.Mausac,
                    Loai = sp.Loai,
                    gia = ss.gia,
                    soluonggio = ss.soluong,
                    image1 = sp.image1,
                    image2 = sp.image2,
                    Ngay_add = sp.Ngay_add,
                    Ngay_update = sp.Ngay_update,
                    Id_danhmuc = sp.Id_danhmuc,
                    Id_sanpham = sp.Id_sanpham,
                });
            }
            return Ok(list);
        }
        [HttpGet("itemDangxuuly")]
        public IActionResult GetAllitem2()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("Bạn chưa đăng nhập!");
            }
            var donhang = _context.donhangs.SingleOrDefault(p => p.UserId == userId && p.trangthai == TrangthaiModel.Dangxuly);
            if (donhang == null)
            {
                return NotFound("Không có đơn hàng nào!");
            }
            var item = _context.Item_Donhangs.Where(p => p.Id_donhang == donhang.Id_donhang).ToList();
            if (item == null)
            {
                return NotFound("Không có sản phảm nào trong đơn hàng");
            }
            var list = new List<SanphamtrongdonhangModel> { };
            foreach (var ss in item)
            {
                var sp = _context.sanphams.SingleOrDefault(p => p.Id_sanpham == ss.Id_sanpham);
                list.Add(new SanphamtrongdonhangModel
                {
                    Id_itemdonhang = ss.Id_itemdonhang,
                    Name_sanpham = sp.Name_sanpham,
                    mota_sp = sp.mota_sp,
                    Mausac = sp.Mausac,
                    Loai = sp.Loai,
                    gia = ss.gia,
                    soluonggio = ss.soluong,
                    image1 = sp.image1,
                    image2 = sp.image2,
                    Ngay_add = sp.Ngay_add,
                    Ngay_update = sp.Ngay_update,
                    Id_danhmuc = sp.Id_danhmuc,
                    Id_sanpham = sp.Id_sanpham,
                });
            }
            return Ok(list);
        }
        [HttpGet]
        public IActionResult GetThongtinDonhang()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("Bạn chưa đăng nhập!");
            }
            var thongtin = _context.donhangs.Where(p=>p.trangthai==TrangthaiModel.Dangtrungchuyen || p.trangthai == TrangthaiModel.Dangxuly || p.trangthai == TrangthaiModel.Danggiao || p.trangthai == TrangthaiModel.None).Select(p => new DonhangModel
            {
               Id_donhang = p.Id_donhang,
                UserId = p.UserId,
                Ngay_dat = p.Ngay_dat,
                Nguoinhan = p.Nguoinhan,
                Diachi = p.Diachi,
                sdt = p.sdt,
                trangthai = p.trangthai,
                type_thanhtoan = p.type_thanhtoan,
            }).FirstOrDefault(P => P.UserId == userId);
            return Ok(thongtin);
        }
        [HttpPost]
        [Authorize(Roles = $"{PhanQuyen.Custommer},{PhanQuyen.Admin}")]
        public IActionResult CreateTT(DonhangModel mmodel)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("Vui lòng đăng nhập!");
            }
            var donhang = _context.donhangs.FirstOrDefault(p => p.UserId == userId && p.trangthai == TrangthaiModel.None);
            if (donhang == null)
            {
                return NotFound();
            }
            var tt = new Donhang
            {
                UserId = userId,
                Ngay_dat= DateTime.Now,

                Nguoinhan= mmodel.Nguoinhan,
                Diachi  = mmodel.Diachi,
                sdt= mmodel.sdt,
                trangthai=TrangthaiModel.None,
                type_thanhtoan=LoaiThanhtoan.toankhinhan
            };
            _context.Add(tt);
            _context.SaveChanges();
            return Ok("Thêm thông tin thành công");
        }
        [HttpPut("Muangay")]
        [Authorize(Roles = $"{PhanQuyen.Custommer},{PhanQuyen.Admin}")]
        public IActionResult Muangay(DonhangModel mmodel)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("Vui lòng đăng nhập!");
            }
            var donhang = _context.donhangs.FirstOrDefault(p => p.UserId == userId && p.trangthai == TrangthaiModel.None);
            if (donhang == null)
            {
                return Ok(new {message="Không tìm thấy"});
            }
            donhang.Nguoinhan = mmodel.Nguoinhan;
            donhang.Diachi = mmodel.Diachi;
            donhang.sdt = mmodel.sdt;
            donhang.type_thanhtoan = LoaiThanhtoan.toankhinhan;
            donhang.Ngay_dat= DateTime.Now;
            donhang.trangthai = TrangthaiModel.Dangxuly;
            
            _context.Update(donhang);
            _context.SaveChanges();
            return Ok(new {message= "Thêm thông tin thành công" });
        }
        [HttpPut("Huydon")]
        [Authorize(Roles = $"{PhanQuyen.Custommer},{PhanQuyen.Admin}")]
        public IActionResult Huydon(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("Vui lòng đăng nhập!");
            }
            var donhang = _context.donhangs.FirstOrDefault(p => p.UserId == userId && p.trangthai != TrangthaiModel.Dangxuly && p.Id_donhang==id);
            if (donhang == null)
            {
                return NotFound("Không thể huỷ trang thái mua hàng");
            }
        
            donhang.trangthai = TrangthaiModel.Dahuy;

            var lichsu = new Lichsumuahang
            {
                Id_donhang = donhang.Id_donhang,
                Ngaynhan = DateTime.Now, // Lưu thời gian huỷ đơn hàng
                trangthai = TrangthaiModel.Dahuy// Lưu trạng thái dưới dạng chuỗi
            };
            _context.Add(lichsu);
            _context.Update(donhang);
            _context.SaveChanges();
       
            return Ok("Huỷ thành công");
        }
        [HttpPut("Danhan")]
        [Authorize(Roles = $"{PhanQuyen.Custommer},{PhanQuyen.Admin}")]
        public IActionResult Danhan(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("Vui lòng đăng nhập!");
            }
            var donhang = _context.donhangs.FirstOrDefault(p => p.UserId == userId && p.Id_donhang == id && p.trangthai == TrangthaiModel.Dangtrungchuyen );
            if (donhang == null ||donhang.trangthai==TrangthaiModel.Dagiao)
            {
                return NotFound("Dơn hàng đã giao!");
            }

            donhang.trangthai = TrangthaiModel.Dagiao;

            var lichsu = new Lichsumuahang
            {
                Id_donhang = donhang.Id_donhang,
                Ngaynhan = DateTime.Now, 
                trangthai = TrangthaiModel.Dagiao
            };
            _context.Add(lichsu);
            _context.Update(donhang);
            _context.SaveChanges();

            return Ok(new {message= "Nhận thành công" });
        }
        [HttpPut("Xacnhan")]
        [Authorize(Roles = PhanQuyen.Admin)]
        public IActionResult Dangtrungchuyen(int id)
        {
         
            var donhang = _context.donhangs.FirstOrDefault(p => p.Id_donhang==id && p.trangthai == TrangthaiModel.Dangxuly );
            if (donhang == null )
            {
                return NotFound(new {mesage="Không thấy!"});
            }

            donhang.trangthai = TrangthaiModel.Dangtrungchuyen;

          
            _context.Update(donhang);
            _context.SaveChanges();

            return Ok(new {mesage= "Xác nhận thành công" });
        }
        [HttpPut("HuyAdmin")]
        [Authorize(Roles = PhanQuyen.Admin)]
        public IActionResult HuydonhangAdmin(int id)
        {

            var donhang = _context.donhangs.FirstOrDefault(p =>p.Id_donhang==id && p.trangthai != TrangthaiModel.None);
            if (donhang == null)
            {
                return NotFound(new { mesage = "Không thấy!" });
            }

            donhang.trangthai = TrangthaiModel.Dahuy;

            var lichsu = new Lichsumuahang
            {
                Id_donhang = donhang.Id_donhang,
                Ngaynhan = DateTime.Now, // Lưu thời gian nhan
                trangthai = TrangthaiModel.Dahuy// Lưu trạng thái dưới dạng chuỗi
            };
            _context.Add(lichsu);
            _context.Update(donhang);
            _context.SaveChanges();

            return Ok(new { mesage = "Huỷ thành công" });

        }
        [HttpPost("Muatronggio")]
        public IActionResult Muasanphamtronggio(DonhangModel donhangmodel)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
            if (userId == null)
            {
                return Unauthorized(new { message = "Người dùng chưa đăng nhập hoặc không hợp lệ." });
            }

            if (donhangmodel == null || string.IsNullOrEmpty(donhangmodel.Nguoinhan) || string.IsNullOrEmpty(donhangmodel.Diachi) || string.IsNullOrEmpty(donhangmodel.sdt))
            {
                return BadRequest(new { message = "Thông tin đơn hàng không hợp lệ." });
            }

            var giohang = _context.giohang.SingleOrDefault(p => p.UserId == userId); 
            if (giohang == null || !_context.item_Giohangs.Any(p => p.Id_giohang == giohang.Id_giohang))
            {
                return BadRequest(new { message = "Giỏ hàng rỗng hoặc không tồn tại." });
            }

            var item = _context.item_Giohangs.Where(p => p.Id_giohang == giohang.Id_giohang).ToList();
            var danhMucs = _context.danhmucs.ToList();
            var hangSxes = _context.hangsxes.ToList();
            var listsp = new List<SanphamModelName>();

            foreach (var ss in item)
            {
                var sanpham = _context.sanphams.SingleOrDefault(p => p.Id_sanpham == ss.Id_sanpham);

                if (sanpham == null)
                {
                    continue;
                }

                if (sanpham.soluong < ss.soluong)
                {
                    return BadRequest(new { message = $"Sản phẩm {sanpham.Name_sanpham} không đủ số lượng." });
                }

                var tendanhm = danhMucs.SingleOrDefault(p => p.Id_danhmuc == sanpham.Id_danhmuc);
                var tenhang = hangSxes.SingleOrDefault(p => p.Id_hangsx == sanpham.Id_hangsx);

                listsp.Add(new SanphamModelName
                {
                    Id_sanpham = sanpham.Id_sanpham,
                    Name_sanpham = sanpham.Name_sanpham,
                    tenDM = tendanhm?.Ten_danhmuc,
                    tenHang = tenhang?.Ten_hangsx,
                    mota_sp = sanpham.mota_sp,
                    Mausac = sanpham.Mausac,
                    Loai = sanpham.Loai,
                    gia = sanpham.gia.ToString(),
                    soluong = ss.soluong,
                    image1 = sanpham.image1,
                    image2 = sanpham.image2,
                    Ngay_add = sanpham.Ngay_add,
                    Ngay_update = sanpham.Ngay_update,
                });
            }

            var tt = new Donhang
            {
                Ngay_dat = DateTime.Now,
                UserId = userId,
                trangthai = TrangthaiModel.Dangxuly,
                Nguoinhan = donhangmodel.Nguoinhan,
                Diachi = donhangmodel.Diachi,
                sdt = donhangmodel.sdt,
                type_thanhtoan = LoaiThanhtoan.toankhinhan,
            };

            _context.Add(tt);
            _context.SaveChanges();

            var itemDonhangs = new List<Item_donhang>();

            foreach (var itemdonhang in listsp)
            {
                itemDonhangs.Add(new Item_donhang
                {
                    Id_sanpham = itemdonhang.Id_sanpham,
                    Id_donhang = tt.Id_donhang,
                    soluong = itemdonhang.soluong,
                    gia = Decimal.Parse(itemdonhang.gia)
                });
            }
            _context.Item_Donhangs.AddRange(itemDonhangs);
            _context.item_Giohangs.RemoveRange(item);
            _context.SaveChanges();

            return Ok(new { message = "Thêm đơn hàng thành công!" });
        }
        [Authorize(Roles = $"{PhanQuyen.Custommer},{PhanQuyen.Admin}")]
        [HttpGet("trungchuyen")]
        public IActionResult GetDonhang()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("Bạn chưa đăng nhập!");
            }
            var thongtin = _context.donhangs.Where(p => p.UserId == userId &&( p.trangthai == TrangthaiModel.Dangtrungchuyen || p.trangthai==TrangthaiModel.Dangxuly )).Select(p => new DonhangModel
            {
                Id_donhang = p.Id_donhang,
                UserId = p.UserId,
                Ngay_dat = p.Ngay_dat,
                Nguoinhan = p.Nguoinhan,
                Diachi = p.Diachi,
                sdt = p.sdt,
                trangthai = p.trangthai,
                type_thanhtoan = p.type_thanhtoan,
            }).ToList();

           
            var list = new List<ThongTinDonHangModel>();
            
            foreach (var items in thongtin)
            {
                var listsp = _context.Item_Donhangs.Where(p => p.Id_donhang == items.Id_donhang).ToList();
                var soluong = 0;
                decimal tonggia = 0;
                var itemdonhang = new List<Item_donhangTTModel>();
                foreach (var item in listsp)
                    {
                        tonggia += item.gia * item.soluong;
                    soluong += item.soluong;
                    var sp = _context.sanphams.SingleOrDefault(p=>p.Id_sanpham== item.Id_sanpham);
                        itemdonhang.Add(new Item_donhangTTModel
                        {
                            Id_donhang=item.Id_donhang,
                            Id_sanpham=item.Id_sanpham,
                            gia=item.gia,
                            soluong=item.soluong,
                            Name_sanpham=sp.Name_sanpham,
                        });
                    }
                list.Add(new ThongTinDonHangModel
                {
                    Id_donhang = items.Id_donhang,
                    UserId = items.UserId,
                    Ngay_dat = items.Ngay_dat,
                    Nguoinhan = items.Nguoinhan,
                    Diachi = items.Diachi,
                    sdt = items.sdt,
                    trangthai = items.trangthai,
                    type_thanhtoan = items.type_thanhtoan,
                    soluong = soluong,
                    gia = tonggia,
                    item_donhang = itemdonhang,


                });



            }
            if (thongtin != null)
            {
                return Ok(list);
            }
            return Ok(new {message="Bạn chưa có đơn hàng nào!"});



           
        }
    }

}









    

