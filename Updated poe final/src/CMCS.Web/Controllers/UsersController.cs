using CMCS.Web.Attributes;
using CMCS.Web.Models;
using CMCS.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace CMCS.Web.Controllers
{
    [RoleAuthorize("HR")] // HR only
    public class UsersController : Controller
    {
        private readonly IUserStore _users;

        public UsersController(IUserStore users)
        {
            _users = users;
        }

        public IActionResult Index()
        {
            var list = _users.All().ToList();
            return View(list);
        }

        public IActionResult Create()
        {
            return View(new User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            if (string.IsNullOrWhiteSpace(vm.Password))
            {
                ModelState.AddModelError("Password", "Password is required.");
                return View(vm);
            }

            _users.Add(vm);

            TempData["Success"] = "User created successfully.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid id)
        {
            var user = _users.Get(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            if (string.IsNullOrWhiteSpace(vm.Password))
            {
                ModelState.AddModelError("Password", "Password is required.");
                return View(vm);
            }

            _users.Add(vm);

            TempData["Success"] = "User updated.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(Guid id)
        {
            var user = _users.Get(id);
            if (user == null)
                return NotFound();

            _users.Delete(id);
            TempData["Success"] = "User deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}
