using QLDT_DAL;
using System;
using System.Threading.Tasks;

namespace QLDT_BLL // Giả định đây là tầng Business Logic Layer (BLL)
{
    // Lớp dịch vụ người dùng, xử lý logic nghiệp vụ liên quan đến người dùng
    public class UserSvc
    {
        private readonly UserRep _userRep; // Khai báo một đối tượng UserRep để tương tác với DAL

        // Constructor để khởi tạo UserRep. Trong ứng dụng thực tế, nên dùng Dependency Injection.
        public UserSvc()
        {
            _userRep = new UserRep();
        }

        /// <summary>
        /// Cập nhật mật khẩu cho người dùng.
        /// </summary>
        /// <param name="userId">ID của người dùng.</param>
        /// <param name="oldPassword">Mật khẩu cũ của người dùng.</param>
        /// <param name="newPassword">Mật khẩu mới.</param>
        /// <param name="confirmNewPassword">Xác nhận lại mật khẩu mới.</param>
        /// <returns>True nếu cập nhật thành công, False nếu ngược lại.</returns>
        public bool UpdatePassword(int userId, string oldPassword, string newPassword, string confirmNewPassword)
        {
            // Có thể thêm các logic nghiệp vụ khác ở đây trước khi gọi DAL
            // Ví dụ: kiểm tra độ mạnh mật khẩu, kiểm tra các chính sách mật khẩu, v.v.

            // Gọi phương thức UpdatePassword từ UserRep để thực hiện cập nhật trong cơ sở dữ liệu
            return _userRep.UpdatePassword(userId, oldPassword, newPassword, confirmNewPassword);
        }

        
    }
}
