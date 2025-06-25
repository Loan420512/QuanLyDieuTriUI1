using System;
using System.Collections.Generic;

namespace QLDT_DAL.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int TreatmentServiceId { get; set; }

    public int MemberId { get; set; }

    public string? StatusBooking { get; set; }

    public DateOnly? CreateAt { get; set; }

    public virtual ICollection<Examination> Examinations { get; set; } = new List<Examination>();

    public virtual Member Member { get; set; } = null!;

    public virtual TreatmentService TreatmentService { get; set; } = null!;
}
