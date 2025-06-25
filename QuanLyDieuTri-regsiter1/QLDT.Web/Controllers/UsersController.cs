using Microsoft.AspNetCore.Mvc; // Cần thiết cho các thuộc tính của Controller và API
using QLDT_BLL; // Tham chiếu đến tầng Business Logic Layer
using System.Threading.Tasks; // Để hỗ trợ các phương thức bất đồng bộ

namespace QLDT_API.Controllers // Giả định đây là không gian tên cho các Controller API của bạn
{
    // Định nghĩa đây là một API Controller
    [ApiController]
    // Định nghĩa đường dẫn cơ sở cho Controller này, ví dụ: /api/users
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserSvc _userSvc; // Khai báo một đối tượng UserSvc

        // Constructor để khởi tạo UserSvc. Trong ứng dụng thực tế, nên dùng Dependency Injection.
        public UsersController()
        {
            _userSvc = new UserSvc();
        }
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterReq req)
    {
       try
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = _userSvc.Register(req);

        if (result)
            return Ok(new { status = "success", message = "Đăng ký thành công." });

        return BadRequest(new { status = "fail", message = "Tài khoản đã tồn tại." });
    }
    catch (Exception ex)
    {
        Console.WriteLine("🔥 Lỗi back-end: " + ex.Message);
        return StatusCode(500, new { status = "error", message = "Lỗi máy chủ: " + ex.Message });
    }
      }

        /// <summary>
    /// Mô hình dữ liệu để nhận đầu vào từ client khi thay đổi mật khẩu.
    /// </summary>
    public class ChangePasswordModel
    {
      public int UserId { get; set; }
      public string OldPassword { get; set; }
      public string NewPassword { get; set; }
      public string ConfirmNewPassword { get; set; }
    }

        /// <summary>
        /// Endpoint API để thay đổi mật khẩu người dùng.
        /// Ví dụ: POST /api/users/change-password
        /// </summary>
        /// <param name="model">Dữ liệu chứa ID người dùng, mật khẩu cũ và mật khẩu mới.</param>
        /// <returns>IActionResult cho biết kết quả của thao tác.</returns>
        [HttpPost("change-password")] // Định nghĩa đây là một HTTP POST và đường dẫn cụ thể
        public IActionResult ChangePassword([FromBody] ChangePasswordModel model)
        {
            // Kiểm tra tính hợp lệ của dữ liệu đầu vào từ client
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Trả về lỗi nếu dữ liệu không hợp lệ
            }

            // Gọi phương thức UpdatePassword từ tầng dịch vụ (BLL)
            bool success = _userSvc.UpdatePassword(model.UserId, model.OldPassword, model.NewPassword, model.ConfirmNewPassword);

            if (success)
            {
                return Ok(new { message = "Mật khẩu đã được thay đổi thành công." }); // Trả về thành công
            }
            else
            {
                // Trả về lỗi nếu việc cập nhật mật khẩu không thành công
                // Có thể tùy chỉnh thông báo lỗi chi tiết hơn từ UserSvc/UserRep
                return BadRequest(new { message = "Không thể thay đổi mật khẩu. Vui lòng kiểm tra lại thông tin." });
            }
        }


    }
}
