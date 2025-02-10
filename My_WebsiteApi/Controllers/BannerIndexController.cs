using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_WebsiteApi.Data;
using My_WebsiteApi.Model;

namespace My_WebsiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerIndexController : ControllerBase
    {
        private readonly MyDbcontext _context;

        public BannerIndexController(MyDbcontext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult getAll()
        {
            var banner =  _context.bannerIndex.ToList();
          
            return Ok(banner);
        }
        [HttpGet("{id}")]
      
        public IActionResult getById(int id)
        {
            var banner = _context.bannerIndex.SingleOrDefault(p=>p.Id==id);
            if (banner == null)
            {
                return NotFound(new {message= "Không tìm thấy!" });
            }
            return Ok(banner);
        }
        [HttpDelete("id")]
        [Authorize(Roles = PhanQuyen.Admin)]
        public IActionResult Delete(int id)
        {
            var banner = _context.bannerIndex.SingleOrDefault(p => p.Id == id);
            if (banner == null)
            {

                return NotFound(new { message = "Không tìm thấy!" });
            }
            _context.Remove(banner);
            _context.SaveChanges();

            return Ok(new { message = "Xoá thành công!" });
        }
        [HttpPut("id")]
        [Authorize(Roles = PhanQuyen.Admin)]
        public IActionResult Edit(int id,BannerIndexModel model)
        {
            if (model == null)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ!" });
            }

            var banner = _context.bannerIndex.SingleOrDefault(p => p.Id == id);
            if (banner == null)
            {
                return NotFound(new { message = "Không tìm thấy!" });
            }
            banner.Name = model.Name;
            banner.Image = model.Image;
            _context.Update(banner);
            _context.SaveChanges();

            return Ok(banner);
        }

        [HttpPost]
        [Authorize(Roles = PhanQuyen.Admin)]
        public IActionResult Create(BannerIndexModel model)
        {
            if (model == null)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ!" });
            }

            var banner = new BannerIndex
            {
                Name = model.Name,
                Image = model.Image,
            };

            _context.Add(banner);
            _context.SaveChanges();
            return CreatedAtAction(nameof(getById), new { id = banner.Id }, banner);
        }
           


    }
}
