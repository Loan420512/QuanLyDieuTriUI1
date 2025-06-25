// QLDT.BLL/RegisterSvc.cs
using Microsoft.EntityFrameworkCore; // Cần thiết cho FirstOrDefaultAsync nếu bạn dùng async
using QLDT.Common.Req;
using QLDT.Common.Res;
using QLDT_DAL; // Đảm bảo namespace này đúng
using QLDT_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLDT.BLL.Services; // Thêm dòng này để sử dụng EmailService

namespace QLDT.BLL
{
    // Không còn kế thừa từ GenericSvc nữa
    public class RegisterSvc
    {
        private readonly RegisterRep _userRegisterRepository;
        private readonly IEmailService _emailService;

        // Constructor nhận RegisterRep và IEmailService thông qua DI
        public RegisterSvc(RegisterRep userRegisterRepository, IEmailService emailService)
        {
            _userRegisterRepository = userRegisterRepository;
            _emailService = emailService;
        }

        // Phương thức để lấy tất cả người dùng (thay thế GetAllUsers())
        public List<User> GetAllUsers()
        {
            return _userRegisterRepository.GetAll().ToList();
        }

        public async Task<SingleRes> CreateUserWithEmailConfirmation(RegisterReq RegisterReq)
        {
            var res = new SingleRes();

            // 1. Kiểm tra email đã tồn tại chưa
            if (_userRegisterRepository.GetAll().Any(u => u.Email == RegisterReq.Email))
            {
                res.SetError("Email đã được đăng ký.");
                return res;
            }

            

            try
            {
                User user = new User();
                
                user.RoleId = 3;     // Role mặc định
                user.UserName = RegisterReq.UserName;
                user.Password = RegisterReq.Password; // Nên mã hóa mật khẩu ở đây
                user.Email = RegisterReq.Email;
                user.IsEmailConfirmed = false;
                user.EmailConfirmationToken = Guid.NewGuid().ToString();
                user.TokenExpiration = DateTime.UtcNow.AddHours(24);

                // Gọi phương thức CreateUser từ RegisterRep
                res = _userRegisterRepository.CreateUser(user);

                
                
                    // 5. Gửi email xác nhận
                    var confirmationLink = $"https://localhost:7240/api/register/confirm-email?userId={user.UserId}&token={user.EmailConfirmationToken}";
                    var emailSubject = "Xác nhận địa chỉ email của bạn";
                    var emailMessage = $"Cảm ơn bạn đã đăng ký. Vui lòng nhấp vào liên kết sau để xác nhận email của bạn: <a href='{confirmationLink}'>Xác nhận Email</a>";
                    Console.WriteLine($"co chay1=================:");

                await _emailService.SendEmailAsync(user.Email, emailSubject, emailMessage);
                Console.WriteLine($"co chay1=================:");
                res.msg = "Đăng ký thành công. Vui lòng kiểm tra email của bạn để xác nhận.";
                res.Data = new { user.UserId, user.UserName, user.Email };
                
            }
            catch (Exception ex)
            {
                res.SetError($"Đăng ký thất bại: {ex.Message}");
                Console.WriteLine($"Error during user registration: {ex.Message}");
            }
            return res;
        }

        public async Task<SingleRes> ConfirmEmail(int userId, string token)
        {
            var res = new SingleRes();
            // Sử dụng repository để truy vấn người dùng
            var user = await _userRegisterRepository.GetAll().FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                res.SetError("Người dùng không tồn tại.");
                return res;
            }

            if (user.IsEmailConfirmed)
            {
                res.SetError("Email đã được xác nhận trước đó.");
                return res;
            }

            if (user.EmailConfirmationToken != token || user.TokenExpiration < DateTime.UtcNow)
            {
                res.SetError("Mã xác nhận không hợp lệ hoặc đã hết hạn.");
                return res;
            }

            user.IsEmailConfirmed = true;
            user.EmailConfirmationToken = null;
            user.TokenExpiration = null;

            try
            {
                // Gọi UpdateUser trên RegisterRep
                res = _userRegisterRepository.UpdateUser(user);
                if (res.Success)
                {
                    res.msg = "Email của bạn đã được xác nhận thành công!";
                }
            }
            catch (Exception ex)
            {
                res.SetError($"Xác nhận email thất bại: {ex.Message}");
                Console.WriteLine($"Error during email confirmation: {ex.Message}");
            }
            return res;
        }
    }
}