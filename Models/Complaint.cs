using System.ComponentModel.DataAnnotations;

namespace CivicConnect.Models
{
    public class Complaint
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = "";

        [Required]
        public string Description { get; set; } = "";

        public string Location { get; set; } = "";

        public string ImagePath { get; set; } = "";

        public string Status { get; set; } = "Pending";

        public string Latitude { get; set; } = "";
        public string Longitude { get; set; } = "";

        public int UserId { get; set; }

        // 🔥 Admin features
        public string Department { get; set; } = "";
        public string AssignedTo { get; set; } = "";

        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}