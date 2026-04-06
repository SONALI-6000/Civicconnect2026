using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CivicConnect.Data;
using CivicConnect.Models;
using System.Linq;

namespace CivicConnect.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly AppDbContext _context;

        public FeedbackController(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 User only
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Role") != "User")
                return RedirectToAction("Login", "Account");

            return View();
        }

        // 🔹 Submit (User only)
        [HttpPost]
        public IActionResult Create(Feedback feedback)
        {
            if (HttpContext.Session.GetString("Role") != "User")
                return RedirectToAction("Login", "Account");

            var userId = HttpContext.Session.GetString("UserId");

            feedback.UserId = int.Parse(userId);

            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();

            TempData["Success"] = "Feedback submitted!";

            return RedirectToAction("UserDashboard", "User");
        }

        // 🔹 Admin only (VIEW)
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("Login", "Account");

            var data = _context.Feedbacks
                .Include(f => f.User)
                .ToList();

            return View(data);
        }
    }
}