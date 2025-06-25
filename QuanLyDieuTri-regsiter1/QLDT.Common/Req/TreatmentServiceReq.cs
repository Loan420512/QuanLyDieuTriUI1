using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDT.Common.Req
{
    public class TreatmentServiceReq
    {
        public int TreatmentServiceId { get; set; }

        public int? UserId { get; set; }

        public string? Name { get; set; }

        public decimal? Price { get; set; }

        public string? Descriptions { get; set; }

        public string? Durations { get; set; }
    }
}
