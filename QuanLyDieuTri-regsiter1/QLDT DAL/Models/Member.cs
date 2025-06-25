using System;
using System.Collections.Generic;

namespace QLDT_DAL.Models;

public partial class Member
{
    public int MemberId { get; set; }

    public int UserId { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Name { get; set; }

    public string? Gender { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual User User { get; set; } = null!;
}
