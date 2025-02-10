using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_WebsiteApi.Data;
using My_WebsiteApi.Model;

namespace My_WebsiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DanhmucController : ControllerBase
    {
        private readonly MyDbcontext _context;

        public DanhmucController(MyDbcontext context)
        {
            _context = context;
        }
        [HttpGet]


        public IActionResult GetAll()
        {
            var tt = _context.danhmucs.ToList();
            return Ok(tt);
        }


        [HttpPost]

        [Authorize(Roles = PhanQuyen.Admin)]
        public IActionResult Create([FromBody] DanhmucModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Ten_danhmuc))
            {
                return BadRequest("Tên danh mục không được để trống.");
            }

            var tt = new Danhmuc
            {
                Ten_danhmuc = model.Ten_danhmuc,
                Mota_danhmuc = model.Mota_danhmuc,
            };
            _context.Add(tt);
            _context.SaveChanges();
            return Ok(tt);
        }
        [HttpPut("id")]
        [Authorize(Roles = PhanQuyen.Admin)]
        public IActionResult Update(int id, Danhmuc model)
        {
            var tt = _context.danhmucs.SingleOrDefault(tt => tt.Id_danhmuc == id);
            if (tt == null)
            {
                return NotFound();
            }
            tt.Ten_danhmuc = model.Ten_danhmuc;
            tt.Mota_danhmuc = model.Mota_danhmuc;

            _context.Update(tt);
            _context.SaveChanges();
            return Ok(tt);
        }
        [HttpDelete("id")]

        [Authorize(Roles = PhanQuyen.Admin)]

        public IActionResult Delete(int id)
        {
            var tt = _context.danhmucs.SingleOrDefault(tt => tt.Id_danhmuc == id);
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
