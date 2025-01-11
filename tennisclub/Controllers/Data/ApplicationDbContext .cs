using Microsoft.EntityFrameworkCore;
using tennisclub.Models;

namespace tennisclub.Controllers.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        
        }

        public DbSet<User> Users { get; set; } // DbSet dla klasy User
        public DbSet<Court> Courts { get; set; } // DbSet dla klasy Court
    }
}
