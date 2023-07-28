using System;
using System.Collections.Generic;

namespace CPDMS.Entities;

public partial class MState
{
    public int Id { get; set; }

    public string StateCode { get; set; } = null!;

    public string? StateName { get; set; }

    public string? StateCategory { get; set; }

    public virtual ICollection<MDistrict> MDistricts { get; } = new List<MDistrict>();
}
