using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_WebsiteApi.Data;
using My_WebsiteApi.Model;

namespace My_WebsiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TintucController : ControllerBase
    {
        private readonly MyDbcontext _context;

        public TintucController(MyDbcontext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var tt = _context.tintucs.ToList();
            return Ok(tt);
        }
        [HttpPost]
        [Authorize(Roles = PhanQuyen.Admin)]
        public IActionResult Create(TintucModel tt)
        {
            try
            {
                var tintuc = new Tintuc
                {
                    Tieude = tt.Tieude,
                    noidung = tt.noidung,
                    image = tt.image,
                    ngaythem = tt.ngaythem,
                };
                _context.Add(tintuc);
                _context.SaveChanges();
                return Ok(tintuc);

            }
            catch (Exception ex)
            {
                return BadRequest(new
                { mesage = ex.Message });
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetTTById(int id)
        {
            var tt = _context.tintucs.SingleOrDefault(p=>p.Id==id);
            if (tt == null)
            {
                return NotFound(new {message="Không thìm thấy thông tin!"});
            }
            return Ok(tt);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = PhanQuyen.Admin)]
        public IActionResult Edit(int id, TintucModel model)
        {
            var tt = _context.tintucs.SingleOrDefault(p => p.Id == id);
            if (tt == null)
            {
                return NotFound(new { message = "Không thìm thấy thông tin!" });
            }
           tt.Tieude = model.Tieude;
            tt.noidung = model.noidung;
            tt.image = model.image;
            tt.ngaythem = model.ngaythem;
            _context.Update(tt);
            _context.SaveChanges();
            return Ok(tt);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = PhanQuyen.Admin)]
        public IActionResult Delete(int id)
        {
            var tt = _context.tintucs.SingleOrDefault(p => p.Id == id);
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
