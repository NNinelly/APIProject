using APIProject.Models;
using Microsoft.EntityFrameworkCore;

namespace APIProject.ApplicationDbContext
{
    public class APIDbContext : DbContext
    {
      public APIDbContext()
      {
      }

      public APIDbContext(DbContextOptions options) : base(options)
        {

        }
      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.Entity<employee>()
            .HasOne<User>(s => s.user)
            .WithMany(r => r.Employees)
            .HasForeignKey(s => s.UserId);
      }
      public DbSet<User> users { get; set; }
      public DbSet<employee> employees { get; set; }

   }
}
