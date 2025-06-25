using QLDT.Common.DAL;
using QLDT_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDT_DAL
{
    public class LoginRep:GenericRep<Test6Context,User>
    {
        public LoginRep() 
        { 
        }
        public bool ValidateUser(string code,string pass)
        {
            var user = All.FirstOrDefault(c => c.UserName == code && c.Password == pass && c.IsEmailConfirmed == true);
            return user != null;
        }
       
    }
}
