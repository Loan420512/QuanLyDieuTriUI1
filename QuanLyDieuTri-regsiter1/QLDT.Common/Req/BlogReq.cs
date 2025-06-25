using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDT.Common.Req
{
    public class BlogReq
    {
        public int IdBlog { get; set; }

        public string? Type { get; set; }

        public string Title { get; set; } = null!;

        public string? Content { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}