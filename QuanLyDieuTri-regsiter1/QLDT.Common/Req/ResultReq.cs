using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDT.Common.Req
{
    public class ResultReq
    {
        public int ResultId { get; set; }

        public int ExaminationId { get; set; }

        public string? ResultTest { get; set; }
    }
}