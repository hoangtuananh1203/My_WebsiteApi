using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_WebsiteApi.Data;
using My_WebsiteApi.Model;
using System.Xml.Linq;

namespace My_WebsiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LienheController : ControllerBase
    {
        private readonly MyDbcontext _context;

        public LienheController(MyDbcontext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var tt = _context.lienHes.ToList();
            return Ok(tt);
        }
        [HttpPost]
        public IActionResult Create(LienheModel tt)
        {
            try
            {
                var lh = new LienHe
                {
                     name = tt.name,
                     email = tt.email,
                     noidung = tt.noidung,
                     sdt = tt.sdt,
                    ngaythem = tt.ngaythem,
                };
                _context.Add(lh);
                _context.SaveChanges();
                return Ok(lh);

            }
            catch (Exception ex)
            {
                return BadRequest(new
                { mesage = ex.Message });
            }
        }
        [HttpGet("{id}")]
        [Authorize(Roles = PhanQuyen.Admin)]
        public IActionResult GetTTById(int id)
        {
            var tt = _context.lienHes.SingleOrDefault(p => p.Id == id);
            if (tt == null)
            {
                return NotFound(new { message = "Không thìm thấy thông tin!" });
            }
            return Ok(tt);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = PhanQuyen.Admin)]
        public IActionResult Edit(int id, LienheModel model)
        {
            var tt = _context.lienHes.SingleOrDefault(p => p.Id == id);
            if (tt == null)
            {
                return NotFound(new { message = "Không thìm thấy thông tin!" });
            }
                    tt.name = model.name;
                    tt.email = model.email;
                    tt. noidung = model.noidung;
                    tt.sdt = model.sdt;
                    tt.ngaythem = model.ngaythem;
       
                    tt.ngaythem = model.ngaythem;
            _context.Update(tt);
            _context.SaveChanges();
            return Ok(tt);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = PhanQuyen.Admin)]
        public IActionResult Delete(int id)
        {
            var tt = _context.lienHes.SingleOrDefault(p => p.Id == id);
            if (tt == null)
            {
                return NotFound(new { message = "Không thìm thấy thông tin!" });
            }
            _context.Remove(tt);
            _context.SaveChanges();
            return Ok(tt);
        }
    }
}
