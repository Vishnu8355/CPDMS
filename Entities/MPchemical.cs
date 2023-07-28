using System;
using System.Collections.Generic;

namespace CPDMS.Entities;

public partial class MPchemical
{
    public int Id { get; set; }

    public string PchemicalCode { get; set; } = null!;

    public string? PchemicalName { get; set; }

    public virtual ICollection<MPchemicalProduct> MPchemicalProducts { get; } = new List<MPchemicalProduct>();
}
