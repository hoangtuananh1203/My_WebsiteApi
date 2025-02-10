using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_WebsiteApi.Data;

namespace My_WebsiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BieudoController : ControllerBase
    {
        private readonly MyDbcontext _context;

      public BieudoController( MyDbcontext context) {
            _context = context;

        }
        [HttpGet]
        public IActionResult Solieuthongke()
        {
            var tongdon = _context.donhangs.Count(p => p.trangthai != Model.TrangthaiModel.None);
            var xuly = _context.donhangs.Count(p => p.trangthai == Model.TrangthaiModel.Dangxuly);
            var trungchuyen = _context.donhangs.Count(p => p.trangthai == Model.TrangthaiModel.Dangtrungchuyen);
            var danhan = _context.donhangs.Count(p => p.trangthai == Model.TrangthaiModel.Dagiao);
            var dahuy = _context.donhangs.Count(p => p.trangthai == Model.TrangthaiModel.Dahuy);
            var giocohang = _context.giohang.ToList();
            var count = 0;
            foreach (var item in giocohang)
            {
                var sp = _context.item_Giohangs.FirstOrDefault(p => p.Id_giohang == item.Id_giohang);
                if (sp != null)
                {
                    count++;
                }

            }
            var onesao = _context.danhgia_Sps.Count(p => p.Diem == 1);
            var twosao = _context.danhgia_Sps.Count(p => p.Diem == 2);
            var threesao = _context.danhgia_Sps.Count(p => p.Diem == 3);
            var foursao = _context.danhgia_Sps.Count(p => p.Diem == 4);
            var fivesao = _context.danhgia_Sps.Count(p => p.Diem == 5);

            var alldon = _context.donhangs.Select(p => p.UserId).Distinct().ToList();
            var khachfirts = 0;
            var khach1don = 0; var khachndon = 0;
            foreach (var userId in alldon)
            {

                var danhanhang = _context.donhangs
                    .Where(p => p.UserId == userId && p.trangthai == Model.TrangthaiModel.Dagiao)
                    .FirstOrDefault();

                var khach = _context.donhangs.Count(p => p.UserId == userId && (p.trangthai == Model.TrangthaiModel.Dangxuly || p.trangthai == Model.TrangthaiModel.Dangtrungchuyen || p.trangthai == Model.TrangthaiModel.Danggiao || p.trangthai == Model.TrangthaiModel.Dagiao));
                if (khach == 1)
                {
                    khach1don++;

                }
                if (khach >= 2)
                {
                    khachndon++;
                }
                if (danhanhang != null)
                {
                    khachfirts++;
                }
            }

            return Ok(new 
            {
                tongdon = tongdon,
                xuly = xuly,
                trungchuyen = trungchuyen,
                dahuy = dahuy,
                danhan = danhan,
                giocohang = count,
                onesao = onesao,
                twosao = twosao,
                threesao = threesao,
                foursao = foursao,
                fivesao = fivesao,
                khachfirts = khachfirts,
                khach1don = khach1don,
                khachndon = khachndon,


            });



        }


    }
}
