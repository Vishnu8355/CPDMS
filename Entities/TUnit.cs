using System;
using System.Collections.Generic;

namespace CPDMS.Entities;

public partial class TUnit
{
    public long Id { get; set; }

    public string UserId { get; set; } = null!;

    public string? Address { get; set; }

    public string StateCode { get; set; } = null!;

    public string DistrictCode { get; set; } = null!;

    public string BlockCode { get; set; } = null!;

    public string? Pincode { get; set; }

    public string? NdName { get; set; }

    public string? NdAddress { get; set; }

    public string? NdMobileNo { get; set; }

    public string? NdEmail { get; set; }

    public string? AdName { get; set; }

    public string? AdAddress { get; set; }

    public string? AdMobileNo { get; set; }

    public string? AdEmail { get; set; }

    public string? EntryBy { get; set; }

    public DateTime? EntryDate { get; set; }

    public string? IpAddress { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
