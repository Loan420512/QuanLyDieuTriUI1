using QLDT_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace QLDT.BLL
{
    public class LoginSvc
    {
        private LoginRep LoginRep;
        public LoginSvc() { 
            LoginRep = new LoginRep();
        }

        public bool Login(string username, string password)
        {
            return LoginRep.ValidateUser(username, password);
        }
        
    }
}
