using System;
using System.Collections.Generic;

namespace CPDMS.Entities;

public partial class MPchemicalProduct
{
    public int Id { get; set; }

    public string PchemicalCode { get; set; } = null!;

    public string PchemicalProductCode { get; set; } = null!;

    public string? PchemicalProductName { get; set; }

    public virtual MPchemical PchemicalCodeNavigation { get; set; } = null!;
}
