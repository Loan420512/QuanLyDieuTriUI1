using QLDT.Common.DAL;

using QLDT_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDT_DAL
{
    public class UserRep : GenericRep<Test6Context, User>
    {
        public UserRep()
        {
            // Constructor mặc định
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
            // Bước 1: Tìm người dùng trong cơ sở dữ liệu dựa trên ID của người dùng.
            // Giả định rằng lớp User có một thuộc tính 'Id' là khóa chính
            var user = All.FirstOrDefault(c => c.UserId == userId);

            // Kiểm tra xem người dùng có tồn tại không.
            if (user == null)
            {
                Console.WriteLine("Người dùng không tồn tại với ID này.");
                return false;
            }

            // Bước 2: Xác thực mật khẩu cũ.
            // CẢNH BÁO BẢO MẬT: Trong ứng dụng thực tế, bạn TUYỆT ĐỐI không so sánh mật khẩu
            // dưới dạng văn bản thuần (plain text). Thay vào đó, bạn PHẢI sử dụng HASHING
            // và SALTING. Bạn sẽ hash oldPassword mà người dùng cung cấp và so sánh
            // với hash mật khẩu đã lưu trong cơ sở dữ liệu.
            if (user.Password != oldPassword)
            {
                Console.WriteLine("Mật khẩu cũ không chính xác.");
                return false;
            }

            // Bước 3: Kiểm tra mật khẩu mới và mật khẩu xác nhận có khớp nhau không.
            if (newPassword != confirmNewPassword)
            {
                Console.WriteLine("Mật khẩu mới và mật khẩu xác nhận không khớp.");
                return false;
            }

            // Bước 4: (Tùy chọn nhưng nên có) Kiểm tra mật khẩu mới có khác mật khẩu cũ không.
            if (newPassword == oldPassword)
            {
                Console.WriteLine("Mật khẩu mới phải khác mật khẩu cũ.");
                return false;
            }

            // Bước 5: Cập nhật mật khẩu mới cho người dùng.
            // CẢNH BÁO BẢO MẬT: Tương tự như trên, bạn PHẢI HASH newPassword
            // trước khi gán nó vào user.Password và lưu vào cơ sở dữ liệu.
            user.Password = newPassword;

            // Bước 6: Lưu thay đổi vào cơ sở dữ liệu.
            try
            {
                Context.Set<User>().Update(user); // Đánh dấu đối tượng là đã được sửa đổi
                Context.SaveChanges(); // Lưu các thay đổi vào cơ sở dữ liệu
                Console.WriteLine("Cập nhật mật khẩu thành công.");
                return true;
            }
            catch (Exception ex)
            {
                // Ghi lại lỗi nếu có vấn đề khi cập nhật hoặc lưu.
                Console.WriteLine($"Lỗi khi cập nhật mật khẩu: {ex.Message}");
                return false;
            }
        }
    
    
    }
}
