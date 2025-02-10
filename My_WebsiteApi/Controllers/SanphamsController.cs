using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using My_WebsiteApi.Data;
using My_WebsiteApi.Model;
using System.Collections.Generic;
using System.Globalization;
using System.Net.WebSockets;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace My_WebsiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanphamsController : ControllerBase
    {
        private readonly MyDbcontext _context;
        private static int Page_SIZE { get; set; } = 20;
        public SanphamsController(MyDbcontext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var sp = _context.sanphams.ToList();
            if (sp == null)
            {
                return NotFound();
            }
            return Ok(sp);
        }
        [HttpGet("countrealy")]
        public IActionResult GetCountsp()
        {
            var totalSoluong = _context.sanphams
                                     .Where(p => p.soluong > 0)
                                     .Sum(p => p.soluong); // Tính tổng trực tiếp

            // Trả về JSON với thông tin tổng số lượng
            var result = new
            {
               
                totalSoluong = totalSoluong
            };

            return Ok(result);
        }
        [HttpGet("danhsachspnew")]
        public IActionResult GetBySanphamnew()
        {
            var culture = new CultureInfo("vi-VN");

            var sp = _context.sanphams
       .OrderByDescending(p => p.Ngay_add) 
       .Take(20) 
       .ToList();
            if (!sp.Any())
            {
                return NotFound();
            }
            var danhMucs = _context.danhmucs.ToList();
            var hangSxes = _context.hangsxes.ToList();
            var list = new List<SanphamModelName> { };
            foreach (var i in sp)
            {
                var tendanhm = danhMucs.SingleOrDefault(p => p.Id_danhmuc == i.Id_danhmuc); // tim danh muc theo id 
                var tenhang = hangSxes.SingleOrDefault(p => p.Id_hangsx == i.Id_hangsx); // tim danh muc theo id 
                var sanpham = new SanphamModelName
                {
                    Id_sanpham = i.Id_sanpham,
                    Name_sanpham = i.Name_sanpham,
                    tenDM = tendanhm.Ten_danhmuc,
                    tenHang = tenhang.Ten_hangsx,
                    mota_sp = i.mota_sp,
                    Mausac = i.Mausac,
                    Loai = i.Loai,
                    gia = i.gia.ToString("N0", culture), // Định dạng với dấu chấm
                   
                    soluong = i.soluong,
                    image1 = i.image1,
                    image2 = i.image2,
                    Ngay_add = i.Ngay_add,
                    Ngay_update = i.Ngay_update,


                };
                list.Add(sanpham);

            }

            return Ok(list);
        }
        [HttpGet("danhsachsp")]
        public IActionResult GetByDanhmuc()
        {
            var culture = new CultureInfo("vi-VN");

            var sp = _context.sanphams.Take(10).ToList(); // danh sach sp
            if (!sp.Any())
            {
                return NotFound();
            }
            var danhMucs = _context.danhmucs.ToList();
            var hangSxes = _context.hangsxes.ToList();
            var list = new List<SanphammodelAdminidd> { };
            foreach (var i in sp)
            {
                var tendanhm = danhMucs.SingleOrDefault(p => p.Id_danhmuc == i.Id_danhmuc); // tim danh muc theo id 
                var tenhang = hangSxes.SingleOrDefault(p => p.Id_hangsx == i.Id_hangsx); // tim danh muc theo id 
                var sanpham = new SanphammodelAdminidd
                {
                    Id_sanpham = i.Id_sanpham,
                    Name_sanpham = i.Name_sanpham,
                    tenDM = tendanhm.Ten_danhmuc,
                    tenHang = tenhang.Ten_hangsx,
                    mota_sp = i.mota_sp,
                    Mausac = i.Mausac,
                    Loai = i.Loai,
                    gia = i.gia.ToString("N0", culture), // Định dạng với dấu chấm

                    soluong = i.soluong,
                    image1 = i.image1,
                    image2 = i.image2,
                    Ngay_add = i.Ngay_add,
                    Ngay_update = i.Ngay_update,
                    madm=i.Id_danhmuc,
                    mahang=i.Id_hangsx,

                };
                list.Add(sanpham);

            }

            return Ok(list);
        }
        [HttpGet("danhsachsp1")]
        public IActionResult Danhsach()
        {
            var culture = new CultureInfo("vi-VN");

            var sp = _context.sanphams.ToList(); // danh sach sp
            if (!sp.Any())
            {
                return NotFound();
            }
            var danhMucs = _context.danhmucs.ToList();
            var hangSxes = _context.hangsxes.ToList();
            var list = new List<SanphammodelAdminidd> { };
            foreach (var i in sp)
            {
                var tendanhm = danhMucs.SingleOrDefault(p => p.Id_danhmuc == i.Id_danhmuc); // tim danh muc theo id 
                var tenhang = hangSxes.SingleOrDefault(p => p.Id_hangsx == i.Id_hangsx); // tim danh muc theo id 
                var sanpham = new SanphammodelAdminidd
                {
                    Id_sanpham = i.Id_sanpham,
                    Name_sanpham = i.Name_sanpham,
                    tenDM = tendanhm.Ten_danhmuc,
                    tenHang = tenhang.Ten_hangsx,
                    mota_sp = i.mota_sp,
                    Mausac = i.Mausac,
                    Loai = i.Loai,
                    gia = i.gia.ToString("N0", culture), // Định dạng với dấu chấm

                    soluong = i.soluong,
                    image1 = i.image1,
                    image2 = i.image2,
                    Ngay_add = i.Ngay_add,
                    Ngay_update = i.Ngay_update,
                    madm = i.Id_danhmuc,
                    mahang = i.Id_hangsx,

                };
                list.Add(sanpham);

            }

            return Ok(list);
        }
        [HttpGet("danhsachspshop")]
        public IActionResult GetByDanhmucshop(int page = 1)
        {
            var culture = new CultureInfo("vi-VN");

            var sp = _context.sanphams.AsQueryable();

            if (!sp.Any())
            {
                return NotFound();
            }

            // Lấy tổng số sản phẩm
            var totalItems = sp.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / Page_SIZE);

            // Phân trang
            sp = sp.Skip((page - 1) * Page_SIZE).Take(Page_SIZE);

            // Lấy danh mục và hãng sản xuất một lần
            var danhMucs = _context.danhmucs.ToList();
            var hangSxes = _context.hangsxes.ToList();

            var list = new List<SanphamModelName>();

            foreach (var i in sp)
            {
                var tendanhm = danhMucs.SingleOrDefault(p => p.Id_danhmuc == i.Id_danhmuc);
                var tenhang = hangSxes.SingleOrDefault(p => p.Id_hangsx == i.Id_hangsx);

                var sanpham = new SanphamModelName
                {
                    Id_sanpham = i.Id_sanpham,
                    Name_sanpham = i.Name_sanpham,
                    tenDM = tendanhm?.Ten_danhmuc ?? "Không xác định",
                    tenHang = tenhang?.Ten_hangsx ?? "Không xác định",
                    mota_sp = i.mota_sp,
                    Mausac = i.Mausac,
                    Loai = i.Loai,
                    gia = i.gia.ToString("N0", culture),
                    soluong = i.soluong,
                    image1 = i.image1,
                    image2 = i.image2,
                    Ngay_add = i.Ngay_add,
                    Ngay_update = i.Ngay_update,
                };

                list.Add(sanpham);
            }

            return Ok(new
            {
                items = list,
                totalPages = totalPages
            });
        }
        [HttpGet("iddanhmuc")]
        public IActionResult GetByDanhmuc(int id, int page = 1)
        {
            var culture = new CultureInfo("vi-VN");

            var sp = _context.sanphams.Where(p => p.Id_danhmuc == id).AsQueryable(); // danh sach sp
            if (!sp.Any())
            {
                return NotFound();
            }
            var tongsoSP = sp.Count();
            var sotrang = (int)Math.Ceiling((double)tongsoSP / Page_SIZE);
            sp = sp.Skip((page - 1) * Page_SIZE).Take(Page_SIZE);
            var danhMucs = _context.danhmucs.ToList();
            var hangSxes = _context.hangsxes.ToList();
            var list = new List<SanphamModelName> { };
            foreach (var i in sp)
            {
                var tendanhm = danhMucs.SingleOrDefault(p => p.Id_danhmuc == i.Id_danhmuc); // tim danh muc theo id 
                var tenhang = hangSxes.SingleOrDefault(p => p.Id_hangsx == i.Id_hangsx); // tim danh muc theo id 
                var sanpham = new SanphamModelName
                {
                    Id_sanpham = i.Id_sanpham,
                    Name_sanpham = i.Name_sanpham,
                    tenDM = tendanhm.Ten_danhmuc,
                    tenHang = tenhang.Ten_hangsx,
                    mota_sp = i.mota_sp,
                    Mausac = i.Mausac,
                    Loai = i.Loai,
                    gia = i.gia.ToString("N0", culture),
                    soluong = i.soluong,
                    image1 = i.image1,
                    image2 = i.image2,
                    Ngay_add = i.Ngay_add,
                    Ngay_update = i.Ngay_update,


                };
                list.Add(sanpham);

            }

            return Ok(
                new
                {
                    tongsoSP = list,
                    sotrang = sotrang
                });
        }
        [HttpGet("idhangsx")]
        public IActionResult GetByHangsx(int id, int page = 1)
        {
            var culture = new CultureInfo("vi-VN");

            var sp = _context.sanphams.Where(p => p.Id_hangsx == id).AsQueryable(); // danh sach sp
            if (!sp.Any())
            {
                return NotFound();
            }
            var tongsoSP = sp.Count();
            var sotrang = (int)Math.Ceiling((double)tongsoSP / Page_SIZE);
            sp = sp.Skip((page - 1) * Page_SIZE).Take(Page_SIZE);
            var danhMucs = _context.danhmucs.ToList();
            var hangSxes = _context.hangsxes.ToList();
            var list = new List<SanphamModelName> { };
            foreach (var i in sp)
            {
                var tendanhm = danhMucs.SingleOrDefault(p => p.Id_danhmuc == i.Id_danhmuc); // tim danh muc theo id 
                var tenhang = hangSxes.SingleOrDefault(p => p.Id_hangsx == i.Id_hangsx); // tim danh muc theo id 
                var sanpham = new SanphamModelName
                {
                    Id_sanpham = i.Id_sanpham,
                    Name_sanpham = i.Name_sanpham,
                    tenDM = tendanhm.Ten_danhmuc,
                    tenHang = tenhang.Ten_hangsx,
                    mota_sp = i.mota_sp,
                    Mausac = i.Mausac,
                    Loai = i.Loai,
                    gia = i.gia.ToString("N0", culture),
                    soluong = i.soluong,
                    image1 = i.image1,
                    image2 = i.image2,
                    Ngay_add = i.Ngay_add,
                    Ngay_update = i.Ngay_update,


                };
                list.Add(sanpham);

            }

            return Ok(
                new
                {
                    tongsoSP = list,
                    sotrang = sotrang
                });
        }
        [HttpGet("id")]
        public IActionResult GetById(int id)
        {
            var sp = _context.sanphams.SingleOrDefault(p => p.Id_sanpham == id);
            var culture = new CultureInfo("vi-VN");

            if (sp == null)
            {
                return NotFound();
            }
            var tendm = _context.danhmucs.SingleOrDefault(p => p.Id_danhmuc == sp.Id_danhmuc);
            var hangsx = _context.hangsxes.SingleOrDefault(p => p.Id_hangsx == sp.Id_hangsx);
            if(tendm==null || hangsx == null)
            {
                return NotFound(new
                {

                    message = "danh muuc hoac hãng không hợp lệ"
                }
                );
            }
            else
            {
                return Ok(new SanphamModelName
                {
                    Id_sanpham = sp.Id_sanpham,
                    Name_sanpham = sp.Name_sanpham,
                    tenDM = tendm.Ten_danhmuc,
                    tenHang = hangsx.Ten_hangsx,
                    mota_sp = sp.mota_sp,
                    Mausac = sp.Mausac,
                    Loai = sp.Loai,
                    gia = sp.gia.ToString("N0", culture),
                    soluong = sp.soluong,
                    image1 = sp.image1,
                    image2 = sp.image2,
                    Ngay_add = sp.Ngay_add,
                    Ngay_update = sp.Ngay_update


                });
            }

          
        } 
        [HttpGet("idsp_hangsx")]
        public IActionResult Getidsp_Hangsx(int id)
        {
            var sp = _context.sanphams.SingleOrDefault(p => p.Id_sanpham == id);
            var culture = new CultureInfo("vi-VN");

            if (sp == null)
            {
                return NotFound();
            }
            var tendm = _context.danhmucs.SingleOrDefault(p => p.Id_danhmuc == sp.Id_danhmuc);
            var hangsx = _context.hangsxes.SingleOrDefault(p => p.Id_hangsx == sp.Id_hangsx);
            if (tendm == null || hangsx == null)
            {
                return NotFound(new
                {

                    message = "danh muuc hoac hãng không hợp lệ"
                }
                );
            }
            else
            {
                return Ok(new SanphamModelName
                {
                    Id_sanpham = sp.Id_sanpham,
                    Name_sanpham = sp.Name_sanpham,
                    tenDM = tendm.Ten_danhmuc,
                    tenHang = hangsx.Ten_hangsx,
                    mota_sp = sp.mota_sp,
                    Mausac = sp.Mausac,
                    Loai = sp.Loai,
                    gia = sp.gia.ToString("N0", culture),
                    soluong = sp.soluong,
                    image1 = sp.image1,
                    image2 = sp.image2,
                    Ngay_add = sp.Ngay_add,
                    Ngay_update = sp.Ngay_update


                });
            }


        }
        [HttpGet("GetOr")]
        public IActionResult GetBySP(string? keyword, decimal? from, decimal? to, string? sortBy, string? tendm, int page = 1)
        {
            var sanpham = _context.sanphams.Include(p => p.madanhmuc).Include(p => p.mahang).AsQueryable();
            var danhMucs = _context.danhmucs.ToList();
            var hangSxes = _context.hangsxes.ToList();
            var culture = new CultureInfo("vi-VN");
            var list = new List<SanphamModelName> { };

            #region fillering
            if (!string.IsNullOrEmpty(keyword))
            {
                sanpham = sanpham.Where(p => p.Name_sanpham.ToLower().Contains(keyword.ToLower()));

            }
            if (!string.IsNullOrEmpty(tendm))
            {
                sanpham = sanpham.Where(p => p.madanhmuc.Ten_danhmuc.ToLower().Contains(tendm.ToLower()));

            }
            if (from.HasValue)
            {
                sanpham = sanpham.Where(p => p.gia >= from);

            }
            if (to.HasValue)
            {
                sanpham = sanpham.Where(p => p.gia <= to);

            }
            #endregion

            #region SORT    
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "tentang":
                        sanpham = sanpham.OrderBy(p => p.Name_sanpham);
                        break;
                    case "tengiam":
                        sanpham = sanpham.OrderByDescending(p => p.Name_sanpham);
                        break;
                    case "giagiam":
                        sanpham = sanpham.OrderByDescending(p => p.gia);
                        break;
                    case "giatang":
                        sanpham = sanpham.OrderBy(p => p.gia);
                        break;
                }
            }

            #endregion
            var tongsoSP = sanpham.Count();
            var sotrang = (int)Math.Ceiling((double)tongsoSP / Page_SIZE);
            #region paging
            sanpham = sanpham.Skip((page - 1) * Page_SIZE).Take(Page_SIZE);
            

            #endregion
            var result = sanpham.ToList();
            
            foreach(var i in result)
            {
                var tendanhm = danhMucs.SingleOrDefault(p => p.Id_danhmuc == i.Id_danhmuc); // tim danh muc theo id 
                var tenhang = hangSxes.SingleOrDefault(p => p.Id_hangsx == i.Id_hangsx); // tim 

                var sp = new SanphamModelName
                {
                    Id_sanpham = i.Id_sanpham,
                    Name_sanpham = i.Name_sanpham,
                    tenDM = tendanhm.Ten_danhmuc,
                    tenHang = tenhang.Ten_hangsx,
                    mota_sp = i.mota_sp,
                    Mausac = i.Mausac,
                    Loai = i.Loai,
                    gia = i.gia.ToString("N0", culture),
                    soluong = i.soluong,
                    image1 = i.image1,
                    image2 = i.image2,
                    Ngay_add = i.Ngay_add,
                    Ngay_update = i.Ngay_update,


                };
                list.Add(sp);



            }



            return Ok(
                   new
                   {
                       tongsoSP = list,
                       sotrang = sotrang
                   });



        }
        [HttpGet("GetOradmin")]
        public IActionResult GetBySP2(int? id,string? keyword, string? tendm, string? hang, int page = 1)
        {
            var sanpham = _context.sanphams.Include(p => p.madanhmuc).Include(p => p.mahang).AsQueryable();
            var danhMucs = _context.danhmucs.ToList();
            var hangSxes = _context.hangsxes.ToList();
            var culture = new CultureInfo("vi-VN");
            var list = new List<SanphamModelName> { };

            #region fillering
            if (id.HasValue)
            {
                sanpham = sanpham.Where(p => p.Id_sanpham==id);

            }
            if (!string.IsNullOrEmpty(keyword))
            {
                sanpham = sanpham.Where(p => p.Name_sanpham.ToLower().Contains(keyword.ToLower()));

            }
            if (!string.IsNullOrEmpty(tendm))
            {
                sanpham = sanpham.Where(p => p.madanhmuc.Ten_danhmuc.ToLower().Contains(tendm.ToLower()));

            }
            if (!string.IsNullOrEmpty(hang))
            {
                sanpham = sanpham.Where(p => p.mahang.Ten_hangsx.ToLower().Contains(hang.ToLower()));

            }

            #endregion

            var tongsoSP = sanpham.Count();
            var sotrang = (int)Math.Ceiling((double)tongsoSP / Page_SIZE);
            #region paging
            //sanpham = sanpham.Skip((page - 1) * Page_SIZE).Take(Page_SIZE);


            #endregion
            var result = sanpham.ToList();

            foreach (var i in result)
            {
                var tendanhm = danhMucs.SingleOrDefault(p => p.Id_danhmuc == i.Id_danhmuc); // tim danh muc theo id 
                var tenhang = hangSxes.SingleOrDefault(p => p.Id_hangsx == i.Id_hangsx); // tim 

                var sp = new SanphamModelName
                {
                    Id_sanpham = i.Id_sanpham,
                    Name_sanpham = i.Name_sanpham,
                    tenDM = tendanhm.Ten_danhmuc,
                    tenHang = tenhang.Ten_hangsx,
                    mota_sp = i.mota_sp,
                    Mausac = i.Mausac,
                    Loai = i.Loai,
                    gia = i.gia.ToString("N0", culture),
                    soluong = i.soluong,
                    image1 = i.image1,
                    image2 = i.image2,
                    Ngay_add = i.Ngay_add,
                    Ngay_update = i.Ngay_update,
                };
                list.Add(sp);
            }
            return Ok(
                   new
                   {
                       tongsoSP = list,
                       sotrang = sotrang
                   });
        }
        [HttpPost]
        [Authorize(Roles = PhanQuyen.Admin)]
        public IActionResult Create(SanphamModel model)
        {

            try
            {
                var sp = new Sanpham
                {
                    Name_sanpham = model.Name_sanpham,
                    mota_sp = model.mota_sp,
                    Mausac = model.Mausac,
                    Loai = model.Loai,
                    gia = model.gia,
                    soluong = model.soluong,
                    image1 = model.image1,
                    image2 = model.image2,
                    Ngay_add = model.Ngay_add,
                    Ngay_update = model.Ngay_update,
                    Id_danhmuc = model.Id_danhmuc,
                    Id_hangsx = model.Id_hangsx,
                };
                _context.Add(sp);
                _context.SaveChanges();
                return Ok(sp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }


        }
        [HttpPut("id")]
        [Authorize(Roles = PhanQuyen.Admin)]
        public IActionResult Update(int id, SanphamModel model)
        {
            try
            {
                var sp = _context.sanphams.SingleOrDefault(p => p.Id_sanpham == id);
                if (sp == null)
                {
                    return NotFound("Không tìm thấy sản phẩm!");

                }
                sp.Name_sanpham = model.Name_sanpham;
                sp.mota_sp = model.mota_sp;
                sp.Mausac = model.Mausac;
                sp.Loai = model.Loai;
                sp.gia = model.gia;
                sp.soluong = model.soluong;
                sp.image1 = model.image1;
                sp.image2 = model.image2;
                sp.Ngay_add = model.Ngay_add;
                sp.Ngay_update = model.Ngay_update;
                sp.Id_danhmuc = model.Id_danhmuc;
                sp.Id_hangsx = model.Id_hangsx;
                _context.Update(sp);
                _context.SaveChanges();
                return Ok(sp);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpDelete("id")]
        [Authorize(Roles = PhanQuyen.Admin)]
        public IActionResult Delete(int id)
        {
            try
            {
                var sp = _context.sanphams.SingleOrDefault(p => p.Id_sanpham == id);
                if (sp == null)
                {
                    return NotFound(new { message = "Không tìm thấy sản phẩm!" });
                }

                _context.Remove(sp);
                _context.SaveChanges();
                return Ok(new { message = "Xóa sản phẩm thành công!" });
            }
            catch (Exception ex)
            {
                // Trả về thông báo lỗi chi tiết nếu có
                return BadRequest(new { message = "Đã có lỗi xảy ra khi xóa sản phẩm.", error = ex.Message });
            }
        }
        [HttpGet("GetOr1")]
        public IActionResult GetBySP1(string keyword, int page = 1)
        {
            var sanpham = _context.sanphams.Include(p => p.madanhmuc).Include(p => p.mahang).AsQueryable();
            var danhMucs = _context.danhmucs.ToList();
            var hangSxes = _context.hangsxes.ToList();
            var culture = new CultureInfo("vi-VN");
            var list = new List<SanphamModelName> { };

            #region fillering
            if (!string.IsNullOrEmpty(keyword))
            {
                sanpham = sanpham.Where(p => p.Name_sanpham.ToLower().Contains(keyword.ToLower()));

            }
           
            #endregion

            var tongsoSP = sanpham.Count();
            var sotrang = (int)Math.Ceiling((double)tongsoSP / Page_SIZE);
            #region paging
            sanpham = sanpham.Skip((page - 1) * Page_SIZE).Take(Page_SIZE);


            #endregion
            var result = sanpham.ToList();

            foreach (var i in result)
            {
                var tendanhm = danhMucs.SingleOrDefault(p => p.Id_danhmuc == i.Id_danhmuc); // tim danh muc theo id 
                var tenhang = hangSxes.SingleOrDefault(p => p.Id_hangsx == i.Id_hangsx); // tim 

                var sp = new SanphamModelName
                {
                    Id_sanpham = i.Id_sanpham,
                    Name_sanpham = i.Name_sanpham,
                    tenDM = tendanhm.Ten_danhmuc,
                    tenHang = tenhang.Ten_hangsx,
                    mota_sp = i.mota_sp,
                    Mausac = i.Mausac,
                    Loai = i.Loai,
                    gia = i.gia.ToString("N0", culture),
                    soluong = i.soluong,
                    image1 = i.image1,
                    image2 = i.image2,
                    Ngay_add = i.Ngay_add,
                    Ngay_update = i.Ngay_update,


                };
                list.Add(sp);



            }



            return Ok(
                   new
                   {
                       tongsoSP = list,
                       sotrang = sotrang
                   });



        }
        [HttpGet("idspsame_hangsx")]
        public IActionResult GetspsameHangsx(int id)
        {
            var sanpham = _context.sanphams.SingleOrDefault(p => p.Id_sanpham == id);
            var culture = new CultureInfo("vi-VN");

            var sp = _context.sanphams.Where(p => p.Id_hangsx == sanpham.Id_hangsx).ToList();
           
         
            var lisst = new List<SanphamModelName>();
        
                foreach(var spp in sp)
                {
                    var tendm = _context.danhmucs.SingleOrDefault(p => p.Id_danhmuc == spp.Id_danhmuc);
                    var hangsx = _context.hangsxes.SingleOrDefault(p => p.Id_hangsx == spp.Id_hangsx);
                    lisst.Add(new SanphamModelName
                    {
                        Id_sanpham = spp.Id_sanpham,
                        Name_sanpham = spp.Name_sanpham,
                        tenDM = tendm.Ten_danhmuc,
                        tenHang = hangsx.Ten_hangsx,
                         mota_sp = spp.mota_sp,
                        Mausac = spp.Mausac,
                        Loai = spp.Loai,
                        gia = spp.gia.ToString("N0", culture),
                        soluong = spp.soluong,
                        image1 = spp.image1,
                        image2 = spp.image2,
                        Ngay_add = spp.Ngay_add,
                        Ngay_update = spp.Ngay_update


                    });
                
            }
            return Ok(lisst);


        }

    }
}
