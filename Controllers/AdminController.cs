using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CivicConnect.Data;
using CivicConnect.Models;
using System.Linq;

namespace CivicConnect.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 Dashboard
        public IActionResult AdminPanel(string filter)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("Login", "Account");

            var complaints = _context.Complaints.AsQueryable();

            if (filter == "Pending")
                complaints = complaints.Where(c => c.Status == "Pending");

            else if (filter == "Resolved")
                complaints = complaints.Where(c => c.Status == "Verified");

            var model = new AdminDashboardViewModel
            {
                Total = _context.Complaints.Count(),
                Pending = _context.Complaints.Count(c => c.Status == "Pending"),
                Resolved = _context.Complaints.Count(c => c.Status == "Verified"),
                Complaints = complaints.ToList(),
                Users = _context.Users.ToList(),

                // 🔥 Feedback include
                Feedbacks = _context.Feedbacks
                    .Include(f => f.User)
                    .ToList()
            };

            return View(model);
        }

        // 🔹 Verify → Assign Task Page
        public IActionResult Verify(int id)
        {
            var complaint = _context.Complaints.Find(id);

            if (complaint == null)
                return NotFound();

            return View(complaint);
        }

        // 🔹 Assign Task + Gamification
        [HttpPost]
        public IActionResult AssignTask(int id, string department)
        {
            var complaint = _context.Complaints.Find(id);

            if (complaint == null)
                return NotFound();

            complaint.Status = "Verified";
            complaint.Department = department;

            var user = _context.Users.Find(complaint.UserId);

            if (user != null)
            {
                // ✅ Points
                user.Points += 10;

                // 🔥 LEVEL LOGIC (INSIDE METHOD)
                if (user.Points >= 100)
                    user.Level = "Champion 🏆";
                else if (user.Points >= 50)
                    user.Level = "Pro 🚀";
                else if (user.Points >= 20)
                    user.Level = "Active 💪";
                else
                    user.Level = "Beginner 🌱";
            }

            TempData["Notify"] = "Your complaint has been verified!";
            _context.SaveChanges();

            return RedirectToAction("AdminPanel");
        }

        // 🔹 Reject
        public IActionResult Reject(int id)
        {
            var complaint = _context.Complaints.Find(id);

            if (complaint != null)
            {
                complaint.Status = "Rejected";
                _context.SaveChanges();
            }

            return RedirectToAction("AdminPanel");
        }

        // 🔹 Delete
        public IActionResult Delete(int id)
        {
            var complaint = _context.Complaints.Find(id);

            if (complaint != null)
            {
                _context.Complaints.Remove(complaint);
                _context.SaveChanges();
            }

            return RedirectToAction("AdminPanel");
        }

        // 🔹 Mark as Read
        public IActionResult MarkRead(int id)
        {
            var complaint = _context.Complaints.Find(id);

            if (complaint != null)
            {
                complaint.IsRead = true;
                _context.SaveChanges();
            }

            return RedirectToAction("AdminPanel");
        }
    }
}