using System;
using System.Collections.Generic;

namespace QLDT_DAL.Models;

public partial class InfoDoctor
{
    public int InfoId { get; set; }

    public int UserId { get; set; }

    public string? Certificate { get; set; }

    public int? ExperianYear { get; set; }

    public string? FullName { get; set; }

    public string? Speciality { get; set; }

    public string? Degree { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual User User { get; set; } = null!;
}
