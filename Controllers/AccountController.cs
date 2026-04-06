using Microsoft.AspNetCore.Mvc;
using CivicConnect.Data;
using CivicConnect.Models;
using System.Linq;

namespace CivicConnect.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 Login Page
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
                return RedirectToAction("AdminPanel", "Admin");

            if (HttpContext.Session.GetString("Role") == "User")
                return RedirectToAction("UserDashboard", "User");

            return View();
        }

        // 🔹 Login POST
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                // ✅ Session store
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("Role", user.Role);

                TempData["LoginSuccess"] = "Login Successful!";

                // 🔥 CORRECT REDIRECT
                if (user.Role == "Admin")
                    return RedirectToAction("AdminPanel", "Admin");

                return RedirectToAction("UserDashboard", "User");
            }

            TempData["Error"] = "Invalid Email or Password!";
            return RedirectToAction("Login");
        }

        // 🔹 Register Page
        public IActionResult Register()
        {
            return View();
        }

        // 🔹 Register POST
        [HttpPost]
        public IActionResult Register(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            TempData["Success"] = "Registration Successful!";
            return RedirectToAction("Login");
        }

        // 🔹 Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}