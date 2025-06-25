// File: BLL/UserSvc.cs
using QLDT.Common.Req;
using QLDT_DAL.Models;
using QLDT_DAL;

public class UserSvc
{
    private readonly UserRep _userRep;

    public UserSvc(UserRep userRep)
    {
        _userRep = userRep;
    }

    public bool Register(RegisterReq req)
    {
        if (_userRep.UserExists(req.UserName))
            return false; // user đã tồn tại

        var newUser = new User
        {
            UserName = req.UserName,
            Email = req.Email,
            Password = req.Password // nên mã hóa mật khẩu
        };

        _userRep.AddUser(newUser);
        return true;
    }
}
