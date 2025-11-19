using CMCS.Web.Attributes;
using CMCS.Web.Helpers;
using CMCS.Web.Hubs;
using CMCS.Web.Models;
using CMCS.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CMCS.Web.Controllers
{
    [RoleAuthorize("Coordinator", "Manager", "HR")]
    public class ApprovalsController : Controller
    {
        private readonly IClaimStore _store;
        private readonly IHubContext<ClaimsHub> _hub;

        public ApprovalsController(IClaimStore store, IHubContext<ClaimsHub> hub)
        {
            _store = store;
            _hub = hub;
        }

        // Main approval dashboard (all roles)
        public IActionResult Pending()
        {
            var claims = _store.All()
                .Where(c =>
                    c.Status == ClaimStatus.PendingVerification ||
                    c.Status == ClaimStatus.Verified ||
                    c.Status == ClaimStatus.PendingFinalApproval)
                .OrderByDescending(c => c.CreatedAt)
                .ToList();

            return View(claims);
        }

        // -------------------------
        // COORDINATOR ACTIONS
        // -------------------------
        [RoleAuthorize("Coordinator", "HR")]
        public async Task<IActionResult> Verify(Guid id)
        {
            var claim = _store.Get(id);
            if (claim == null) return NotFound();

            claim.Status = ClaimStatus.Verified;
            claim.CoordinatorApprovedBy = GetLoggedInUserName();
            claim.CoordinatorApprovedAt = DateTime.Now;

            // Move to next step
            claim.Status = ClaimStatus.PendingFinalApproval;
            _store.Update(claim);

            await Notify(claim);

            TempData["Success"] = "Claim verified and forwarded to Manager.";
            return RedirectToAction(nameof(Pending));
        }

        [RoleAuthorize("Coordinator", "HR")]
        public async Task<IActionResult> Reject(Guid id)
        {
            var claim = _store.Get(id);
            if (claim == null) return NotFound();

            claim.Status = ClaimStatus.Rejected;
            claim.RejectedBy = GetLoggedInUserName();
            claim.RejectedAt = DateTime.Now;

            _store.Update(claim);
            await Notify(claim);

            TempData["Success"] = "Claim rejected.";
            return RedirectToAction(nameof(Pending));
        }

        // -------------------------
        // MANAGER ACTIONS
        // -------------------------
        [RoleAuthorize("Manager", "HR")]
        public async Task<IActionResult> Approve(Guid id)
        {
            var claim = _store.Get(id);
            if (claim == null) return NotFound();

            if (claim.Status != ClaimStatus.PendingFinalApproval)
            {
                TempData["Error"] = "Coordinator must verify this claim first!";
                return RedirectToAction(nameof(Pending));
            }

            claim.Status = ClaimStatus.Approved;
            claim.ManagerApprovedBy = GetLoggedInUserName();
            claim.ManagerApprovedAt = DateTime.Now;

            _store.Update(claim);
            await Notify(claim);

            TempData["Success"] = "Claim fully approved.";
            return RedirectToAction(nameof(Pending));
        }

        // -------------------------
        // SETTLE CLAIM
        // -------------------------
        [RoleAuthorize("Manager", "HR")]
        public async Task<IActionResult> Settle(Guid id)
        {
            var claim = _store.Get(id);
            if (claim == null) return NotFound();

            claim.Status = ClaimStatus.Settled;
            claim.SettledBy = GetLoggedInUserName();
            claim.SettledAt = DateTime.Now;

            _store.Update(claim);
            await Notify(claim);

            TempData["Success"] = "Claim marked as settled.";
            return RedirectToAction(nameof(Pending));
        }

        // Helper: broadcast status change
        private async Task Notify(Claim claim)
        {
            await _hub.Clients.All.SendAsync("statusChanged", claim.Id, claim.Status.ToString());
        }

        // Helper: who is logged in?
        private string GetLoggedInUserName()
        {
            var user = HttpContext.Session.GetObject<User>("User");
            return user?.FullName ?? "Unknown User";
        }
    }
}
