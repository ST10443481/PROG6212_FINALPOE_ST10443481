using CMCS.Web.Attributes;
using CMCS.Web.Models;
using CMCS.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace CMCS.Web.Controllers
{
    [RoleAuthorize("HR")]
    public class ReportsController : Controller
    {
        private readonly IClaimStore _store;

        public ReportsController(IClaimStore store)
        {
            _store = store;
        }

        public IActionResult Index()
        {
            var approved = _store.All()
                .Where(c => c.Status == ClaimStatus.Approved || c.Status == ClaimStatus.Settled)
                .ToList();

            return View(approved);
        }
    }
}