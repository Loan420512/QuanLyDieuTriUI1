using System;
using System.Collections.Generic;

namespace QLDT_DAL.Models;

public partial class MedicalRecord
{
    public int RecordId { get; set; }

    public int MemberId { get; set; }

    public string? Summary { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Member Member { get; set; } = null!;

    public virtual ICollection<TreatmentProcess> TreatmentProcesses { get; set; } = new List<TreatmentProcess>();
}
