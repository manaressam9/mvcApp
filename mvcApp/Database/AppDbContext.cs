using Microsoft.EntityFrameworkCore;
using mvcApp.Models;

namespace mvcApp.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
    }
}
