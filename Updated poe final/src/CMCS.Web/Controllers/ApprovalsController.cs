using CMCS.Web.Attributes;
using CMCS.Web.Hubs;
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

        public IActionResult Index()
        {
            return View(_store.Pending());
        }
        [RoleAuthorize("Coordinator","Manager", "HR")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(string id)
        {
            _store.Approve(id);
            await _hub.Clients.All.SendAsync("statusChanged", id, "Approved");
            TempData["Success"] = $"Approved claim {id}.";
            return RedirectToAction(nameof(Index));
        }

        [RoleAuthorize("Coordinator","Manager","HR")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(string id)
        {
            _store.Reject(id);
            await _hub.Clients.All.SendAsync("statusChanged", id, "Rejected");
            TempData["Success"] = $"Rejected claim {id}.";
            return RedirectToAction(nameof(Index));
        }
    }
}
