using System;
using System.Collections.Generic;

namespace CPDMS.Entities;

public partial class MChemicalProduct
{
    public int Id { get; set; }

    public string ChemicalCode { get; set; } = null!;

    public string ChemicalProductCode { get; set; } = null!;

    public string? ChemicalProductName { get; set; }

    public virtual MChemical ChemicalCodeNavigation { get; set; } = null!;
}
