using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ProjectsTask.Models
{
    public class ProjectManagmentContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        public DbSet<Project> Projects { get; set; }

        public DbSet<Employee> Employees { get; set; }


        public ProjectManagmentContext(IConfiguration configuration)
        {
            _configuration = configuration;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Manager)
                .WithMany()
                .HasForeignKey(p => p.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Employee>()
            //    .HasMany(e => e.Projects)
            //    .WithMany(p => p.Employees)
            //    .UsingEntity(j => j.ToTable("EmployeeProject"));

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Projects)
                .WithMany(p => p.Employees)
                .UsingEntity(j => j.ToTable("EmployeeProject"));                                                    
        }
    }
}
