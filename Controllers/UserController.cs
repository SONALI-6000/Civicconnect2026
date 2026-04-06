using Microsoft.AspNetCore.Mvc;
using CivicConnect.Data;
using CivicConnect.Models;
using System.Linq;

namespace CivicConnect.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 Dashboard
        public IActionResult UserDashboard()
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Account");

            return View();
        }

        // 🔹 Create Complaint (GET)
        public IActionResult CreateComplaint()
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Account");

            return View();
        }

        // 🔹 Create Complaint (POST)
        [HttpPost]
        public IActionResult CreateComplaint(Complaint complaint, IFormFile image)
        {
            var userIdStr = HttpContext.Session.GetString("UserId");

            if (userIdStr == null)
                return RedirectToAction("Login", "Account");

            if (image != null)
            {
                var fileName = Path.GetFileName(image.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    image.CopyTo(stream);
                }

                complaint.ImagePath = "/images/" + fileName;
            }

            complaint.UserId = int.Parse(userIdStr);
            complaint.Status = "Pending";

            _context.Complaints.Add(complaint);
            _context.SaveChanges();

            return RedirectToAction("UserDashboard");
        }

        // 🔹 My Complaints
        public IActionResult MyComplaints()
        {
            var userIdStr = HttpContext.Session.GetString("UserId");

            if (userIdStr == null)
                return RedirectToAction("Login", "Account");

            int userId = int.Parse(userIdStr);

            var complaints = _context.Complaints
                .Where(c => c.UserId == userId)
                .ToList();

            return View(complaints);
        }

        // 🔹 Rewards (FINAL CLEAN)
        public IActionResult Rewards()
        {
            var userIdStr = HttpContext.Session.GetString("UserId");

            if (userIdStr == null)
                return RedirectToAction("Login", "Account");

            int userId = int.Parse(userIdStr);

            var user = _context.Users.Find(userId);

            ViewBag.Points = user?.Points ?? 0;
            ViewBag.Level = user?.Level ?? "Beginner";

            return View();
        }
    }
}