using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDT.Common.Req
{
    public class InfoDoctorReq
    {
        public int InfoId { get; set; }

        public int UserId { get; set; }

        public string? Certificate { get; set; }

        public int? ExperianYear { get; set; }

        public string? FullName { get; set; }

        public string? Speciality { get; set; }

        public string? Degree { get; set; }

        public string? PhoneNumber { get; set; }
    }
}