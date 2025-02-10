using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_WebsiteApi.Data;
using My_WebsiteApi.Model;

namespace My_WebsiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangsxController : ControllerBase
    {
        private readonly MyDbcontext _context;

        public HangsxController(MyDbcontext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var tt = _context.hangsxes.ToList();
            return Ok(tt);
        }
        [HttpPost]
        [Authorize(Roles = PhanQuyen.Admin)]
        public IActionResult Create(HangsxModel model)
        {
            var hangsx = new Hangsx
            {
                Ten_hangsx = model.Ten_hangsx,
                Mota_hangsx= model.Mota_hangsx,
                Diachi_hangsx=model.Diachi_hangsx,
              

            };
            _context.Add(hangsx);
            _context.SaveChanges();
            return Ok(hangsx);
        }
        [HttpPut("id")]

        [Authorize(Roles = PhanQuyen.Admin)]
        public IActionResult Update(int id, HangsxModel model)
        {
            var hangsx = _context.hangsxes.SingleOrDefault(hangsx => hangsx.Id_hangsx == id);
            if (hangsx == null)
            {
                return NotFound();
            }
            hangsx.Ten_hangsx = model.Ten_hangsx;
            hangsx.Mota_hangsx = model.Mota_hangsx;
            hangsx.Diachi_hangsx = model.Diachi_hangsx;

           

            _context.Update(hangsx);
            _context.SaveChanges();
            return Ok(hangsx);
        }
        [HttpDelete("id")]
        [Authorize(Roles = PhanQuyen.Admin)]
        public IActionResult Delete(int id)
        {
            var hangsx = _context.hangsxes.SingleOrDefault(hangsx => hangsx.Id_hangsx == id);
            if (hangsx == null)
            {
                return NotFound();
            }

            _context.Remove(hangsx);
            _context.SaveChanges();
            return Ok(hangsx);
        }
    }
}
