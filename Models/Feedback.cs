using System.ComponentModel.DataAnnotations;

namespace CivicConnect.Models
{
    public class Feedback
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        public string Message { get; set; } = "";

        public int Rating { get; set; } // 1 to 5

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public User User { get; set; }
    }
}