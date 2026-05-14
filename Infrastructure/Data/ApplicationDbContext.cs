using Microsoft.EntityFrameworkCore;
using SirProject.Core.Entities;
using SirProject.Core.Enums;

namespace SirProject.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<User>().ToTable("users");
    modelBuilder.Entity<Pessoa>().ToTable("pessoas");
    modelBuilder.Entity<User>().HasData(
        new User { Id = 1, Username = "admin", PasswordHash = "admin123", Role = UserRole.Admin.ToString() },
        new User { Id = 2, Username = "user", PasswordHash = "user123", Role = UserRole.User.ToString() }
    );
}
    }
}
