namespace CMCS.Web.Models
{
    public class ClaimItem
    {
        public Guid ItemId { get; set; }
        public Guid ClaimId { get; set; }
        public DateTime DateWorked { get; set; }
        public decimal HoursWorked { get; set; }
        public string ActivityType { get; set; } = string.Empty;
        public string? Notes { get; set; }
    }
}
