using CMCS.Web.Helpers;
using CMCS.Web.Models;
using CMCS.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace CMCS.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserStore _users;

        public AuthController(IUserStore users)
        {
            _users = users;
        }

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var user = _users.GetByEmail(vm.Email);

            if (user == null || user.Password != vm.Password)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(vm);
            }

            HttpContext.Session.SetObject("User", user);

            return user.Role switch
            {
                "HR" => RedirectToAction("Index", "Users"),
                "Lecturer" => RedirectToAction("Index", "Claims"),
                "Coordinator" => RedirectToAction("Index", "Approvals"),
                "Manager" => RedirectToAction("Index", "Approvals"),
                _ => RedirectToAction("Index", "Home")
            };

        }

        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            if (vm.Password != vm.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                return View(vm);
            }

            // Check if email already exists
            var existing = _users.GetByEmail(vm.Email);
            if (existing != null)
            {
                 ModelState.AddModelError("Email", "Email already registered.");
     }

            var user = new User
            {
                FullName = vm.FullName,
                Email = vm.Email,
                Password = vm.Password,
                Role = vm.Role,
                HourlyRate = vm.HourlyRate,
                IsActive = true
           };

           _users.Add(user);

            TempData["Success"] = "User registered successfully. You may now log in.";
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
