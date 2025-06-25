using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDT.Common.Req
{
    public class FeedbackReq
    {
        public int FeedbackId { get; set; }

        public int MemberId { get; set; }

        public string? ContentFeedback { get; set; }

        public int? Rating { get; set; }

        public string? TargetType { get; set; }

        public DateTime? CreateAt { get; set; }
    }
}