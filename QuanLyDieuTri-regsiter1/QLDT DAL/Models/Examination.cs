using System;
using System.Collections.Generic;

namespace QLDT_DAL.Models;

public partial class Examination
{
    public int ExaminationId { get; set; }

    public int BookingId { get; set; }

    public int DoctorUserId { get; set; }

    public DateOnly? DateMeet { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual User DoctorUser { get; set; } = null!;

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    public virtual ICollection<TreatmentProcess> TreatmentProcesses { get; set; } = new List<TreatmentProcess>();
}
