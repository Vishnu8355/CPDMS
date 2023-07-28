namespace CPDMS.Models.Request
{
    public class TRegistrationDTO
    {
        public long Id { get; set; }

        public string? NameOfUnit { get; set; }

        public string? CpciicCode { get; set; }

        public string UserId { get; set; } = null!;

        public string? Password { get; set; }

        public string? Role { get; set; }

        public string? MobileNo { get; set; }

        public string? EmailId { get; set; }

        public int? MobileOtp { get; set; }

        public string? MobileOtpConfirm { get; set; }

        public int? EmailOtp { get; set; }

        public string? EmailOtpConfirm { get; set; }

        public DateTime? EntryDate { get; set; }

        public string? IpAddress { get; set; }

        public string? IsActive { get; set; }

        public DateTime? DeactiveDate { get; set; }

        public string? Islogin { get; set; }
    }
}
