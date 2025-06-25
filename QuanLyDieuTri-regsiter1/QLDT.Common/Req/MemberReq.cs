using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDT.Common.Req
{
    public class MemberReq
    {
        public int MemberId { get; set; }

        public int UserId { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Name { get; set; }

        public string? Gender { get; set; }

        
    }
}
