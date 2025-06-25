// File: DAL/UserRep.cs
using QLDT_DAL.Models;

public class UserRep
{ private readonly UserRep _rep;

    public UserSvc()
    {
        var context = new Test6Context(); // hoặc dùng DI
        _rep = new UserRep(context);
    }

    public bool Register(RegisterReq req)
    {
        if (_rep.UserExists(req.UserName))
            return false;

        var newUser = new User
        {
            UserName = req.UserName,
            Password = req.Password,
            Email = req.Email,
            RoleId = req.Role_ID
        };

        _rep.AddUser(newUser);
        return true;
    }

    public bool UpdatePassword(int userId, string oldPassword, string newPassword, string confirmPassword)
    {
        var user = _rep._context.Users.FirstOrDefault(u => u.UserId == userId && u.Password == oldPassword);
        if (user == null || newPassword != confirmPassword)
            return false;

        user.Password = newPassword;
        _rep._context.SaveChanges();
        return true;
    }
}
