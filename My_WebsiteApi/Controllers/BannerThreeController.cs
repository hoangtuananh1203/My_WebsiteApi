using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_WebsiteApi.Data;
using My_WebsiteApi.Model;

namespace My_WebsiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerThreeController : ControllerBase
    {

        private readonly MyDbcontext _context;

        public BannerThreeController(MyDbcontext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult getAll()
        {
            var banner = _context.bannerthrees.ToList();

            return Ok(banner);
        }
        [HttpGet("{id}")]


        public IActionResult getById(int id)
        {
            var banner = _context.bannerthrees.SingleOrDefault(p => p.Id == id);
            if (banner == null)
            {
                return NotFound(new { message = "Không tìm thấy!" });
            }
            return Ok(banner);
        }
        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            var banner = _context.bannerthrees.SingleOrDefault(p => p.Id == id);
            if (banner == null)
            {

                return NotFound(new { message = "Không tìm thấy!" });
            }
            _context.Remove(banner);
            _context.SaveChanges();

            return NoContent();
        }
        [HttpPut("id")]
        [Authorize(Roles = PhanQuyen.Admin)]
        public IActionResult Edit(int id, BannerthreeModel model)
        {
            if (model == null)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ!" });
            }

            var banner = _context.bannerthrees.SingleOrDefault(p => p.Id == id);
            if (banner == null)
            {
                return NotFound(new { message = "Không tìm thấy!" });
            }
            banner.Title = model.Title;
            banner.Image = model.Image;
            banner.mota =   model.mota;
            _context.Update(banner);
            _context.SaveChanges();

            return Ok(banner);
        }

        [HttpPost]

        [Authorize(Roles = PhanQuyen.Admin)]
        public IActionResult Create(BannerthreeModel model)
        {
            if (model == null)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ!" });
            }

            var banner = new Bannerthree
            {
               Title = model.Title,
                Image = model.Image,
                mota = model.mota,
            };

            _context.Add(banner);
            _context.SaveChanges();
            return CreatedAtAction(nameof(getById), new { id = banner.Id }, banner);
        }
    }
}
