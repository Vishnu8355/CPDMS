using System;
using System.Collections.Generic;

namespace CPDMS.Entities;

public partial class MChemical
{
    public int Id { get; set; }

    public string ChemicalCode { get; set; } = null!;

    public string? ChemicalName { get; set; }

    public virtual ICollection<MChemicalProduct> MChemicalProducts { get; } = new List<MChemicalProduct>();
}
