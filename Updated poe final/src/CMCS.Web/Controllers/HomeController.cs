using CMCS.Web.Services;
using CMCS.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CMCS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClaimStore _store;
        public HomeController(IClaimStore store)
        {
            _store = store;
        }

        public IActionResult Index()
        {
            var all = _store.All().ToList();
            var vm = new DashboardViewModel
            {
                DraftCount = all.Count(c => c.Status == ClaimStatus.Draft),
                SubmittedCount = all.Count(),
                ApprovedCount = all.Count(c => c.Status == ClaimStatus.Approved),
                RejectedCount = all.Count(c => c.Status == ClaimStatus.Rejected),
                SettledCount = all.Count(c => c.Status == ClaimStatus.Settled),
                TotalSubmittedAmount = all.Where(c => c.Status == ClaimStatus.Submitted).Sum(c => c.Amount),
                TotalApprovedAmount = all.Where(c => c.Status == ClaimStatus.Approved || c.Status == ClaimStatus.Settled).Sum(c => c.Amount),
                RecentPending = _store.Pending().OrderByDescending(c => c.CreatedAt).Take(5).ToList(),
                RecentActivity = all.OrderByDescending(c => (c.UpdatedAt > c.CreatedAt ? c.UpdatedAt : c.CreatedAt)).Take(5).ToList()
            };
            return View(vm);
        }
    }
}