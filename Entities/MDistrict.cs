using System;
using System.Collections.Generic;

namespace CPDMS.Entities;

public partial class MDistrict
{
    public int Id { get; set; }

    public string? StateCode { get; set; }

    public string DistrictCode { get; set; } = null!;

    public string? DistrictName { get; set; }

    public string? DistrictShortName { get; set; }

    public virtual MState? StateCodeNavigation { get; set; }
}
