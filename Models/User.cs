using System.ComponentModel.DataAnnotations;

namespace CivicConnect.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";

        public string Role { get; set; } = "User";

        // ✅ ADD THIS
        public int Points { get; set; } = 0;
        public string Level { get; set; } = "Beginner";
    }
}