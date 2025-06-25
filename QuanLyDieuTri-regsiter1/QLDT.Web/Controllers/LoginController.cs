using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using QLDT.BLL;
namespace QLDT.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private LoginSvc loginSvc;
        public LoginController() { 
            loginSvc = new LoginSvc();
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (loginSvc.Login(request.Username , request.Password))
            {
                return Ok(new { message = "Đăng nhập thành công meow~" });
            }

            return Unauthorized(new { message = "Sai tên đăng nhập hoặc mật khẩu meow~" });
        }

    }
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
