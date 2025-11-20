namespace CMCS.Web.Models
{
    public enum ClaimStatus
    {
        PendingVerification,    // Lecturer submitted → waiting for Coordinator
        Verified,               // Coordinator verified it
        PendingFinalApproval,   // Waiting for Manager approval
        Approved,               // Fully approved by Manager (and Coordinator already)
        Rejected,               // Coordinator rejected
        Settled,                 // Payment processed
        Submitted,
        Update
    }
}
