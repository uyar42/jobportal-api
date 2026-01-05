using Microsoft.EntityFrameworkCore;
using JobPortal.API.Entities;

namespace JobPortal.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Job> Jobs { get; set; }

        public DbSet<User> Users { get; set; }

    }
}
