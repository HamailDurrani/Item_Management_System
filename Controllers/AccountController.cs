using ItemManagementSystem.Data;
using ItemManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace ItemManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AccountController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User model)
        {
            if (!ModelState.IsValid) return View(model);

            if (_db.Users.Any(u => u.Email.ToLower() == model.Email.ToLower()))
            {
                ModelState.AddModelError("Email", "Email is already registered.");
                return View(model);
            }

            if (!Regex.IsMatch(model.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$"))
            {
                ModelState.AddModelError("Password", "Password must contain at least 1 uppercase, 1 lowercase, 1 number, and 1 special character.");
                return View(model);
            }

            model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            model.Role = "User";

            _db.Users.Add(model);
            _db.SaveChanges();

            TempData["Success"] = "Registration successful! Please login.";
            return RedirectToAction("Login");
        }

        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Email and password are required.";
                return View();
            }

            var user = _db.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("FullName", user.FullName);
                HttpContext.Session.SetString("Role", user.Role);

                return RedirectToAction("Index", "Item");
            }

            ViewBag.Error = "Invalid email or password.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
