using System.Linq;
using CMCS.Web.Helpers;
using CMCS.Web.Models;
using CMCS.Web.Services;
using Microsoft.AspNetCore.Mvc;

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
            var user = HttpContext.Session.GetObject<User>("User");
            if (user == null)
                return RedirectToAction("Login", "Auth");

            var all = _store.All()?.ToList() ?? new List<Claim>();

            var vm = new DashboardViewModel
            {
                DraftCount = all.Count(c => c.Status == ClaimStatus.Draft),
                SubmittedCount = all.Count(),
                ApprovedCount = all.Count(c => c.Status == ClaimStatus.Approved),
                RejectedCount = all.Count(c => c.Status == ClaimStatus.Rejected),
                SettledCount = all.Count(c => c.Status == ClaimStatus.Settled),
                TotalSubmittedAmount = all.Where(c => c.Status == ClaimStatus.Submitted).Sum(c => c.Amount),
                TotalApprovedAmount = all.Where(c => c.Status == ClaimStatus.Approved || c.Status == ClaimStatus.Settled).Sum(c => c.Amount),
                RecentPending = _store.Pending()?.OrderByDescending(c => c.CreatedAt).Take(5).ToList() ?? new List<Claim>(),
                RecentActivity = all.OrderByDescending(c => (c.UpdatedAt > c.CreatedAt ? c.UpdatedAt : c.CreatedAt)).Take(5).ToList()
            };

            return View(vm);
        }

    }
}