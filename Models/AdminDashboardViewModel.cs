using System.Collections.Generic;

namespace CivicConnect.Models
{
    public class AdminDashboardViewModel
    {
        public int Total { get; set; }
        public int Pending { get; set; }
        public int Resolved { get; set; }

        public List<Complaint> Complaints { get; set; }
        public List<User> Users { get; set; }
        public List<Feedback> Feedbacks { get; set; }
    }
}