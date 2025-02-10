using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_WebsiteApi.Data;
using My_WebsiteApi.Model;

namespace My_WebsiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanphamQuangBaController : ControllerBase
    {
        private readonly MyDbcontext _context;

        public SanphamQuangBaController(MyDbcontext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult getAll()
        {
            var sp = _context.sanphamQuangbas.ToList();

            return Ok(sp);
        }
        [HttpGet("{id}")]


        public IActionResult getById(int id)
        {
            var sp = _context.sanphamQuangbas.SingleOrDefault(p => p.Id == id);
            if (sp == null)
            {
                return NotFound(new { message = "Không tìm thấy!" });
            }
            return Ok(sp);
        }



      
        [HttpPost]
        [Authorize(Roles = PhanQuyen.Admin)]
        public IActionResult Create(SanphamQuangbaModel model)
        {
            var litsp = _context.sanphamQuangbas.ToList();
            if (litsp.Any())
            {
                _context.sanphamQuangbas.RemoveRange(litsp);
                _context.SaveChanges();
            }
            if (model == null)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ!" });
            }

            var sp = new SanphamQuangba
            {
                Title1 = model.Title1,
                Title2 = model.Title2,
                Image = model.Image,
                mota = model.mota,
            };

            _context.Add(sp);
            _context.SaveChanges();
            return CreatedAtAction(nameof(getById), new { id = sp.Id }, sp);
        }
    }
}
