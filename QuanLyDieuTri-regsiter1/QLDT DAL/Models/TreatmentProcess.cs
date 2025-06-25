using System;
using System.Collections.Generic;

namespace QLDT_DAL.Models;

public partial class TreatmentProcess
{
    public int TreatmentProcessId { get; set; }

    public int RecordId { get; set; }

    public int ExaminationId { get; set; }

    public DateOnly? DateTreatment { get; set; }

    public string? Descriptions { get; set; }

    public string? PlanTreatment { get; set; }

    public virtual Examination Examination { get; set; } = null!;

    public virtual MedicalRecord Record { get; set; } = null!;
}
