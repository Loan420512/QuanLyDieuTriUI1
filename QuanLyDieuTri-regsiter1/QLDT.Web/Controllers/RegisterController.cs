// QLDT.Web.Controllers/RegisterController.cs
using Microsoft.AspNetCore.Mvc;
using QLDT.BLL;
using QLDT.Common.Req;
using QLDT.Common.Res;
// using QLDT_DAL.Models; // Không cần thiết ở đây nếu không dùng trực tiếp

namespace QLDT.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly RegisterSvc _registerSvc;

        public RegisterController(RegisterSvc registerSvc)
        {
            _registerSvc = registerSvc;
        }

        [HttpPost("register-with-email-confirmation")] // Đổi tên endpoint cho rõ ràng
        public async Task<IActionResult> CreateUserWithEmailConfirmation([FromBody] RegisterReq RegisterReq)
        {
            var res = await _registerSvc.CreateUserWithEmailConfirmation(RegisterReq);
            if (res.Success)
            {
                return Ok(res); // Trả về toàn bộ res để client biết thông báo
            }
            return BadRequest(res);
        }

        [HttpGet("confirm-email")] // Endpoint để xác nhận email
        public async Task<IActionResult> ConfirmEmail([FromQuery] int userId, [FromQuery] string token)
        {
            var res = await _registerSvc.ConfirmEmail(userId, token);
            
                // Có thể redirect đến trang thành công hoặc trả về thông báo JSON
                return Ok(res.Message);
            
            
        }
    }
}