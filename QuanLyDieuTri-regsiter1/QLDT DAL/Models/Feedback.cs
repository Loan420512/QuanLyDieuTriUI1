using System;
using System.Collections.Generic;

namespace QLDT_DAL.Models;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int MemberId { get; set; }

    public string? ContentFeedback { get; set; }

    public int? Rating { get; set; }

    public string? TargetType { get; set; }

    public DateTime? CreateAt { get; set; }

    public virtual Member Member { get; set; } = null!;
}
