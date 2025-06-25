using System;
using System.Collections.Generic;

namespace QLDT_DAL.Models;

public partial class TreatmentService
{
    public int TreatmentServiceId { get; set; }

    public string Name { get; set; } = null!;

    public decimal? Price { get; set; }

    public string? Descriptions { get; set; }

    public string? Durations { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
