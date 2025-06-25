using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDT.Common.Req
{
    public class MedicalRecordReq
    {
        public int RecordId { get; set; }

        public int MemberId { get; set; }

        public string? Summary { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}