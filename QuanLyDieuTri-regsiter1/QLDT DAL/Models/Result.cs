using System;
using System.Collections.Generic;

namespace QLDT_DAL.Models;

public partial class Result
{
    public int ResultId { get; set; }

    public int ExaminationId { get; set; }

    public string? ResultTest { get; set; }

    public virtual Examination Examination { get; set; } = null!;
}
