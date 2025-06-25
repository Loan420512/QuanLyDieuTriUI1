using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDT.Common.Req
{
    public class ExaminationReq
    {
        public int ExaminationId { get; set; }

        public int BookingId { get; set; }

        public int DoctorUserId { get; set; }

        public DateOnly? DateMeet { get; set; }
    }
}