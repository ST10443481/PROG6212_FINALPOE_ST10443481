using System.ComponentModel.DataAnnotations;

namespace CMCS.Web.Models
{
    public class Claim
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        [Required, Display(Name = "Lecturer Name")]
        public string Lecturer { get; set; } = "Lecturer A";
        [Range(0.5, 400), Display(Name = "Hours Worked")]
        public decimal HoursWorked { get; set; }
        [Range(0, 10000), Display(Name = "Hourly Rate (R)")]
        public decimal HourlyRate { get; set; }
        [MaxLength(1000)]
        public string? Notes { get; set; }
        public string? DocumentFileName { get; set; }
        public ClaimStatus Status { get; set; } = ClaimStatus.PendingVerification;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public decimal Amount => Math.Round(HoursWorked * HourlyRate, 2);
    }
}
