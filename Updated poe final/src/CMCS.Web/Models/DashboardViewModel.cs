using System;
using System.Collections.Generic;

namespace CMCS.Web.Models
{
    public class DashboardViewModel
    {
        public int DraftCount { get; set; }
        public int SubmittedCount { get; set; }
        public int ApprovedCount { get; set; }
        public int RejectedCount { get; set; }
        public int SettledCount { get; set; }
        public decimal TotalSubmittedAmount { get; set; }
        public decimal TotalApprovedAmount { get; set; }
        public IEnumerable<Claim> RecentPending { get; set; } = Array.Empty<Claim>();
        public IEnumerable<Claim> RecentActivity { get; set; } = Array.Empty<Claim>();
    }
}