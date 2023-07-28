using System;
using System.Collections.Generic;

namespace CPDMS.Entities;

public partial class MBlock
{
    public int Id { get; set; }

    public string? StateCode { get; set; }

    public string DistrictCode { get; set; } = null!;

    public string BlockCode { get; set; } = null!;

    public string? BlockName { get; set; }
}
