namespace CMCS.Web.Models
{
    public class Approval
    {
        public Guid ApprovalId { get; set; }
        public Guid ClaimId { get; set; }
        public Guid ApproverId { get; set; }
        public string Stage { get; set; } = "Coordinator"; // Coordinator|Manager
        public string Decision { get; set; } = "Pending";  // Pending|Approved|Rejected
        public string? DecisionNote { get; set; }
        public DateTime? DecidedAt { get; set; }
    }
}
