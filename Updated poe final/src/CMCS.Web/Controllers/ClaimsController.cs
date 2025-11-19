using System.IO;
using CMCS.Web.Attributes;
using CMCS.Web.Hubs;
using CMCS.Web.Models;
using CMCS.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CMCS.Web.Controllers
{
    [RoleAuthorize("Lecturer")]
    public class ClaimsController : Controller
    {
        private readonly IClaimStore _store;
        private readonly IWebHostEnvironment _env;
        private readonly IHubContext<ClaimsHub> _hub;
        private readonly IUserStore _userStore;

        private const long MaxBytes = 10 * 1024 * 1024; // 10 MB
        private static readonly string[] AllowedExtensions = { ".pdf", ".docx", ".xlsx" };

        public ClaimsController(
            IClaimStore store,
            IWebHostEnvironment env,
            IHubContext<ClaimsHub> hub,
            IUserStore userStore)
        {
            _store = store;
            _env = env;
            _hub = hub;
            _userStore = userStore;
        }

        // LIST CLAIMS (Lecturer dashboard)
        public IActionResult Index()
        {
            var claims = _store.All().OrderByDescending(c => c.CreatedAt).ToList();
            return View(claims);
        }

        // GET: NEW CLAIM FORM
        public IActionResult New()
        {
            var vm = new ClaimCreateViewModel();

            // Provide list of lecturers for dropdown
            ViewBag.Users = _userStore
                .All()
                .Select(u => new { u.UserId, u.FullName, u.HourlyRate })
                .ToList();

            return View(vm);
        }

        // POST: SUBMIT CLAIM
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(ClaimCreateViewModel vm)
        {
            try
            {
                // VALIDATION 1 – Hours limit
                if (vm.HoursWorked > 180)
                    ModelState.AddModelError("HoursWorked", "Hours worked cannot exceed 180 in a month.");

                // VALIDATION 2 – File check
                if (vm.Document != null)
                {
                    if (vm.Document.Length > MaxBytes)
                        ModelState.AddModelError("Document", "File too large (max 10 MB).");

                    var ext = Path.GetExtension(vm.Document.FileName).ToLowerInvariant();
                    if (!AllowedExtensions.Contains(ext))
                        ModelState.AddModelError("Document", "Only .pdf, .docx, .xlsx files are allowed.");
                }

                // If errors → redisplay form
                if (!ModelState.IsValid)
                {
                    ViewBag.Users = _userStore.All().Select(u => new { u.UserId, u.FullName, u.HourlyRate }).ToList();
                    return View(vm);
                }

                // STORE FILE
                string? storedFileName = null;
                if (vm.Document != null && vm.Document.Length > 0)
                {
                    var uploads = Path.Combine(_env.WebRootPath, "uploads");
                    Directory.CreateDirectory(uploads);

                    storedFileName = $"{Guid.NewGuid():N}{Path.GetExtension(vm.Document.FileName)}";
                    var savePath = Path.Combine(uploads, storedFileName);

                    using var fs = System.IO.File.Create(savePath);
                    await vm.Document.CopyToAsync(fs);
                }

                // GET LECTURER INFO FROM HR STORE
                string lecturerName = "Unknown Lecturer";
                decimal hourlyRate = vm.HourlyRate;

                if (vm.LecturerId.HasValue)
                {
                    var user = _userStore.Get(vm.LecturerId.Value);
                    if (user != null)
                    {
                        lecturerName = user.FullName;
                        hourlyRate = user.HourlyRate;
                    }
                }

                // CREATE CLAIM
                var claim = new Claim
                {
                    Lecturer = lecturerName,
                    HoursWorked = vm.HoursWorked,
                    HourlyRate = hourlyRate,
                    Notes = vm.Notes,
                    DocumentFileName = storedFileName,
                    Status = ClaimStatus.PendingVerification
                };

                _store.Add(claim);

                // Notify via SignalR
                await _hub.Clients.All.SendAsync("statusChanged", claim.Id, claim.Status.ToString());

                TempData["Success"] = "Claim submitted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An unexpected error occurred: {ex.Message}");
                ViewBag.Users = _userStore.All().Select(u => new { u.UserId, u.FullName, u.HourlyRate }).ToList();
                return View(vm);
            }
        }
    }
}
