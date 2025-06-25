// File: QLDT.Common.Req/RegisterReq.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDT.Common.Req
{
  // Lớp RegisterReq chứa các thông tin yêu cầu khi người dùng đăng ký tài khoản mới.
  // Nó phải là 'public' để có thể truy cập từ các dự án khác (ví dụ: QLDT.BLL và QLDT.Web.Controllers).
  public class RegisterReq
  {
    // Tên người dùng để đăng nhập.
    public string UserName { get; set; }

    // Mật khẩu của tài khoản.
    public string Password { get; set; }

    // Địa chỉ email của người dùng.
    // Đây là trường cần thiết cho chức năng xác nhận email.
    public string Email { get; set; }
        public int Role_ID { get; set; }
    }
}
