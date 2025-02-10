using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using My_WebsiteApi.Data;
using My_WebsiteApi.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace My_WebsiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole> identityRole;
        private readonly MyDbcontext context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, RoleManager<IdentityRole> identityRole,MyDbcontext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.identityRole = identityRole;
            this.context = context;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(SigInModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized(new { message = "Tài khoản hoặc mật khẩu không chính xác!" });
            }

            var checkpass = await userManager.CheckPasswordAsync(user, model.Password);
            if (!checkpass)
            {
                return Unauthorized(new { message = "Tài khoản hoặc mật khẩu không chính xác!" });
            }

            var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!result.Succeeded)
            {
                return Unauthorized(new { message = "Tài khoản hoặc mật khẩu không chính xác!" });
            }

            var thongtin = new List<Claim>
        {
            new Claim(ClaimTypes.Email, model.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim("Username", user.UserName),
            new Claim("SĐT", user.sdt),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                thongtin.Add(new Claim(ClaimTypes.Role, role));
            }

            var khoa = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                claims: thongtin,
                expires: DateTime.UtcNow.AddDays(3),
                signingCredentials: new SigningCredentials(khoa, SecurityAlgorithms.HmacSha512)
            );

            return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
        
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
           

            if (authHeader == null || !authHeader.StartsWith("Bearer "))
            {
                
                return Unauthorized(new { message = "Missing or invalid token" });
            }
            
            var token = authHeader.Substring("Bearer".Length).Trim();
           

            return Ok(new { message = "Đăng xuất thành công!" });
        }
        [HttpPost("SigUp")]
        public async Task<IActionResult> SigUp(SigUpModel model)
        {
            
            if (
                string.IsNullOrWhiteSpace(model.Password) ||
                string.IsNullOrWhiteSpace(model.Email))
            {
                return BadRequest(new {mesage= "Username, Password, and Email are required." });
            }

           
            var user = new ApplicationUser
            {
                FirtName = model.FirtName,
                LastName = model.LastName,
               // UserName = model.FirtName+model.LastName,
                UserName=model.Username,
                Ngay_sinh = model.Ngay_sinh,
                diachi = model.diachi,
                sdt = model.sdt,
                Email = model.Email,
            };

          
            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
               
                if (!await identityRole.RoleExistsAsync(PhanQuyen.Custommer))
                {
                    await identityRole.CreateAsync(new IdentityRole(PhanQuyen.Custommer));
                }

              
                await userManager.AddToRoleAsync(user, PhanQuyen.Custommer);

                return Ok(new {mesage= "User registered successfully." });
            }
            else
            {
               
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                return BadRequest(new {message= $"User registration failed: {errors}" });
            }
        }
        [HttpPost("SigUpAdmin")]
        public async Task<IActionResult> SigUpAdmin(SigUpModel model)
        {

            if (
                string.IsNullOrWhiteSpace(model.Password) ||
                string.IsNullOrWhiteSpace(model.Email))
            {
                return BadRequest("Username, Password, and Email are required.");
            }


            var user = new ApplicationUser
            {
                FirtName = model.FirtName,
                LastName = model.LastName,
                // UserName = model.FirtName+model.LastName,
                UserName = model.Username,
                Ngay_sinh = model.Ngay_sinh,
                diachi = model.diachi,
                sdt = model.sdt,
                Email = model.Email,
            };


            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {

                if (!await identityRole.RoleExistsAsync(PhanQuyen.Admin))
                {
                    await identityRole.CreateAsync(new IdentityRole(PhanQuyen.Admin));
                }


                await userManager.AddToRoleAsync(user, PhanQuyen.Admin);

                return Ok("User registered successfully.");
            }
            else
            {

                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                return BadRequest($"User registration failed: {errors}");
            }
        }
        [HttpPut]
        public async Task<IActionResult> Doimatkhau(Doimatkhau model) {

            // Kiểm tra xem mật khẩu mới và mật khẩu xác nhận có khớp không
            if (model.Passwordnew != model.ConfirmPass)
            {
                return Ok(new { message = "Mật khẩu mới và mật khẩu xác nhận không khớp!" });
            }

            // Lấy Id của người dùng đang đăng nhập
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new { message = "Bạn chưa đăng nhập!" });
            }

            // Lấy thông tin người dùng từ UserManager
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new { message = "Không tìm thấy người dùng!" });
            }

            // Kiểm tra mật khẩu hiện tại
            var checkPassword = await userManager.CheckPasswordAsync(user, model.Password);
            if (!checkPassword)
            {
                return Ok(new { message = "Mật khẩu hiện tại không chính xác!" });
            }

            // Đổi mật khẩu
            var result = await userManager.ChangePasswordAsync(user, model.Password, model.Passwordnew);
            if (!result.Succeeded)
            {
                return BadRequest(new { message = "Đổi mật khẩu không thành công!", errors = result.Errors });
            }

            return Ok(new { message = "Đổi mật khẩu thành công!" });


        }
        [HttpGet]
        public async Task<IActionResult> Thongkettaikhoan( )
        {
            
            // Lấy danh sách các UserRoles có vai trò là "Custommer"
            var userRoles = await userManager.GetUsersInRoleAsync("Custommer");
           var count = userRoles.Count();

            if (userRoles == null || !userRoles.Any())
            {
                return NotFound(new { message = "Không có người dùng nào trong vai trò 'Custommer'!" });
            }

            return Ok(new
            {
                souser=count,
                userRoles=userRoles,
            });
        }
        [HttpDelete("email")]
        public async Task<IActionResult> XoaTaiKhoanUser(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound(new { message = "Không tìm thấy tài khoản!" });
            }

            var isUser = await userManager.IsInRoleAsync(user, PhanQuyen.Custommer);
            if (!isUser)
            {
                return BadRequest(new { message = "Chỉ có thể xóa tài khoản với quyền Custommer!" });
            }

            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(new { message = "Xóa tài khoản không thành công!", errors = result.Errors });
            }

            return Ok(new { message = "Xóa tài khoản thành công!" });
        }
        [HttpGet("email")]
        public async Task<IActionResult> Timtaikhoan(string email)
        {
            var user =await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound(new { mesage = "Không tìm thấy tài khoản!" });
            }
            return Ok(user);

        }
        [HttpGet("ALL")]
        public IActionResult DonhangAll()
        {
            var thongtin = (from donhang in context.donhangs
                            join user in context.Users on donhang.UserId equals user.Id
                            where donhang.trangthai != TrangthaiModel.None
                            select new
                            {
                                donhang.Id_donhang,
                                Email = user.Email,
                                Username = user.UserName,
                                Ngay_dat = donhang.Ngay_dat.ToString("dd/MM/yyyy"),
                                Nguoinhan = donhang.Nguoinhan,
                                Diachi = donhang.Diachi,
                                sdt = donhang.sdt,
                                trangthai = donhang.trangthai,
                                type_thanhtoan = donhang.type_thanhtoan,
                                items = (from item in context.Item_Donhangs
                                         where item.Id_donhang == donhang.Id_donhang
                                         select new
                                         {
                                             item.soluong,
                                             item.gia,
                                             tonggia = (item.soluong * item.gia)
                                         }).ToList()
                            }).ToList();

            var result = thongtin.Select(donhang => new
            {
                donhang.Id_donhang,
                donhang.Email,
                donhang.Username,
                donhang.Ngay_dat,
                donhang.Nguoinhan,
                donhang.Diachi,
                donhang.sdt,
                donhang.trangthai,
                donhang.type_thanhtoan,
                soluong = donhang.items.Sum(i => i.soluong), // Tổng số lượng
                gia = donhang.items.Sum(i => i.tonggia).ToString("N0") // Tổng giá, định dạng theo số nghìn
            }).ToList();

            return Ok(result);
        }
        [HttpGet("Details")]
        public IActionResult GetDonhangDetails(int id_donhang)
        {



            var donhang = context.donhangs.SingleOrDefault(p => p.Id_donhang == id_donhang);
            if (donhang == null)
            {
                return NotFound("Không tìm thấy đơn hàng!");
            }


            var itemdonhang = context.Item_Donhangs.Where(p => p.Id_donhang == donhang.Id_donhang).ToList();


            var items = itemdonhang.Select(i => new
            {
                i.Id_sanpham,
                i.soluong,
                i.gia,
                sanpham = context.sanphams.FirstOrDefault(s => s.Id_sanpham == i.Id_sanpham)?.Name_sanpham // Lấy 
            }).ToList();

            var sp = new
            {
                items = items
            };

            return Ok(sp);
        }
        [HttpGet("DonhangEmail")]
        public IActionResult Timdonhang(string name)
        {
            // Kiểm tra xem email có được cung cấp không
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest(new { message = "Email không hợp lệ!" });
            }

            // Lọc đơn hàng theo email
            var thongtin = (from donhang in context.donhangs
                            join user in context.Users on donhang.UserId equals user.Id
                            where donhang.trangthai != TrangthaiModel.None && user.UserName == name
                            select new
                            {
                                Id_donhang = donhang.Id_donhang,
                                Email = user.Email,
                                Username = user.UserName,
                                Ngay_dat = donhang.Ngay_dat,
                                Nguoinhan = donhang.Nguoinhan,
                                Diachi = donhang.Diachi,
                                sdt = donhang.sdt,
                                trangthai = donhang.trangthai,
                                type_thanhtoan = donhang.type_thanhtoan
                            }).ToList();

            if (thongtin.Count == 0)
            {
                return Ok(new { mesage = 1 });
            }

            return Ok(thongtin);
        }
        [HttpGet("getitem")]
        public async Task<IActionResult> GetAll(int id)
        {
            // Lấy danh sách đánh giá của sản phẩm
            var tt = context.danhgia_Sps.Where(p => p.Id_sanpham == id).ToList();

            if (!tt.Any())
            {
                return Ok(new { message = "Không tìm thấy đánh giá nào của sản phẩm này!" });
            }

            var list = new List<Danhgia_spModel>();

            foreach (var s in tt)
            {
                // Lấy thông tin người dùng từ UserManager
                var user = await userManager.FindByIdAsync(s.UserId);
                var username = user?.UserName; 
                list.Add(new Danhgia_spModel
                {
                    Id_danhgia = s.Id_danhgia,
                    UserId = username,
                    Id_sanpham = s.Id_sanpham,
                    Ngay_add = s.Ngay_add,
                    noidung = s.noidung,
                    Diem = s.Diem
                });
            }

            return Ok(list);
        }



    }
}
