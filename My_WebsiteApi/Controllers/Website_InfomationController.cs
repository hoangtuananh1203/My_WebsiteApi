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
    public class Website_InfomationController : ControllerBase
    {
        private readonly MyDbcontext _context;

        public Website_InfomationController(MyDbcontext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var tt = _context.website_Infomations.ToList();
            return Ok(tt);
        }
        [HttpPost]
        [Authorize(Roles = PhanQuyen.Admin)]
        public IActionResult Create(Website_infomationModel model)
        {
            var allRecords = _context.website_Infomations.ToList();
            if (allRecords.Any())
            {
                _context.website_Infomations.RemoveRange(allRecords);
                _context.SaveChanges(); 
            }
            var tt = new Website_infomation
            {
                email = model.email,
                diachi = model.diachi,
                mota = model.mota,
                sdt = model.sdt,
                tencongty = model.tencongty,
            };
            _context.Add(tt);
            _context.SaveChanges();
            return Ok(tt);
        }
        [HttpPut("id")]
        [Authorize(Roles = PhanQuyen.Admin)]

        public IActionResult Update(int id, Website_infomationModel model)
        {
            var tt = _context.website_Infomations.SingleOrDefault(tt => tt.Id == id);
            if (tt == null)
            {
                return NotFound();
            }
            tt.mota = model.mota;
            tt.sdt = model.sdt;
            tt.tencongty = model.tencongty;
            tt.diachi = model.diachi;
            tt.email = model.email;
            _context.Update(tt);
            _context.SaveChanges();
            return Ok(tt);
        }
        [HttpDelete("id")]
        [Authorize(Roles = PhanQuyen.Admin)]

        public IActionResult Delete(int id)
        {
            var tt = _context.website_Infomations.SingleOrDefault(tt => tt.Id == id);
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
