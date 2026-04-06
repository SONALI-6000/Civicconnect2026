using Microsoft.EntityFrameworkCore;
using CivicConnect.Models;

namespace CivicConnect.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
    }
}