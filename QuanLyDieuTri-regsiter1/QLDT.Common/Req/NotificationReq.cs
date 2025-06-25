using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDT.Common.Req
{
    public class NotificationReq
    {
        public int NotificationId { get; set; }

        public int UserId { get; set; }

        public string? ContentNoti { get; set; }

        public DateTime? CreatedAt { get; set; }

        public bool? IsRead { get; set; }
    }
}