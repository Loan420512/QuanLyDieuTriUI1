// QLDT_DAL/RegisterRep.cs
using Microsoft.EntityFrameworkCore;
using QLDT.Common.Res; // Đảm bảo namespace này đúng
using QLDT_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDT_DAL
{
    public class RegisterRep
    {
        private readonly Test6Context _context; // Đảm bảo Test6Context được tiêm vào

        // Constructor nhận Test6Context thông qua Dependency Injection
        public RegisterRep(Test6Context context)
        {
            _context = context;
        }

        // Phương thức để lấy tất cả người dùng
        public IQueryable<User> GetAll()
        {
            return _context.Users;
        }

        // Phương thức để đọc một người dùng theo ID
        public User Read(int id)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == id);
        }

        // Phương thức để tạo một người dùng mới
        public SingleRes CreateUser(User user)
        {
            var res = new SingleRes();
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges(); // Lưu thay đổi vào database
                
                res.Data = user; // Trả về đối tượng user đã tạo
            }
            catch (Exception ex)
            {
                res.SetError(ex.Message);
                // Ghi log ex.StackTrace nếu cần debug
            }
            return res;
        }

        // Phương thức để cập nhật một người dùng
        public SingleRes UpdateUser(User user)
        {
            var res = new SingleRes();
            try
            {
                _context.Users.Update(user); // Đánh dấu đối tượng là đã sửa đổi
                _context.SaveChanges();
                
                res.Data = user;
            }
            catch (Exception ex)
            {
                res.SetError(ex.Message);
            }
            return res;
        }

        // Bạn có thể thêm các phương thức xóa, đọc theo tiêu chí khác nếu cần
        // public SingleRes DeleteUser(int id) { ... }
        // public IQueryable<User> Find(Expression<Func<User, bool>> predicate) { ... }
    }
}