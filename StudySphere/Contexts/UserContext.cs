using Microsoft.EntityFrameworkCore;
using StudySphere.Models;

namespace StudySphere.Contexts
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasKey(x => x.Username);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Users> Users { get; set; }
    }
}
