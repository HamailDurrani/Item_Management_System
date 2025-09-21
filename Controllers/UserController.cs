//using ItemManagementSystem.Data;
//using ItemManagementSystem.Models;
//using Microsoft.AspNetCore.Mvc;
//using System.Text.RegularExpressions;

//namespace ItemManagementSystem.Controllers
//{
//    public class UserController : Controller
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly ILogger<UserController> _logger;

//        public UserController(ApplicationDbContext context, ILogger<UserController> logger)
//        {
//            _context = context;
//            _logger = logger;
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Register(User model)
//        {
//            if (!ModelState.IsValid) return View(model);

//            var pwd = model.Password ?? "";
//            if (!Regex.IsMatch(pwd, @"(?=^.{8,}$)(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?=.*\W)"))
//            {
//                ModelState.AddModelError("Password", "Password not strong enough.");
//                _logger.LogWarning("Weak password attempt for email: {Email}", model.Email);
//                return View(model);
//            }

//            if (_context.Users.Any(u => u.Email.ToLower() == model.Email.ToLower()))
//            {
//                ModelState.AddModelError("Email", "Email already exists.");
//                _logger.LogWarning("Duplicate registration attempt for email: {Email}", model.Email);
//                return View(model);
//            }

//            model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

//            _context.Users.Add(model);
//            _context.SaveChanges();

//            _logger.LogInformation("New user registered: {Email}", model.Email);

//            TempData["Success"] = "Registration successful! Please login.";
//            return RedirectToAction("Login");
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Login(string email, string password)
//        {
//            var user = _context.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

//            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
//            {
//                HttpContext.Session.SetString("UserEmail", user.Email);
//                HttpContext.Session.SetString("UserRole", user.Role);

//                _logger.LogInformation("User logged in: {Email}", email);

//                TempData["Success"] = $"Welcome back, {user.FullName}!";
//                return RedirectToAction("Index", "Items");
//            }

//            _logger.LogWarning("Failed login attempt for email: {Email}", email);
//            ViewBag.Error = "Invalid email or password!";
//            return View();
//        }


//        public IActionResult Logout()
//        {
//            var email = HttpContext.Session.GetString("UserEmail");
//            _logger.LogInformation("User logged out: {Email}", email);

//            HttpContext.Session.Clear();
//            return RedirectToAction("Login");
//        }
//    }
//}


using ItemManagementSystem.Data;
using ItemManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace ItemManagementSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserController> _logger;

        public UserController(ApplicationDbContext context, ILogger<UserController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Register
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User model)
        {
            if (!ModelState.IsValid) return View(model);

            var pwd = model.Password ?? "";
            if (!Regex.IsMatch(pwd, @"(?=^.{8,}$)(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?=.*\W)"))
            {
                ModelState.AddModelError("Password", "Password not strong enough.");
                return View(model);
            }

            if (_context.Users.Any(u => u.Email.ToLower() == model.Email.ToLower()))
            {
                ModelState.AddModelError("Email", "Email already exists.");
                return View(model);
            }

            model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            _context.Users.Add(model);
            _context.SaveChanges();

            TempData["Success"] = "Registration successful! Please login.";
            return RedirectToAction("Login");
        }

        // GET: Login
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                // Save session info
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("FullName", user.FullName);
                HttpContext.Session.SetString("Role", user.Role);

                TempData["Success"] = $"Welcome back, {user.FullName}!";

                return RedirectToAction("Index", "Item");
            }

            ViewBag.Error = "Invalid email or password!";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
