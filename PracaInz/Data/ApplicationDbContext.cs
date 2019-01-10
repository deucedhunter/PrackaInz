using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PracaInz.Models;

namespace PracaInz.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Course> Coursees { get; set; }
        public DbSet<Subject> Subjects { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Student>().ToTable("Student");
            builder.Entity<Employee>().ToTable("Employee");
            builder.Entity<Person>().ToTable("Person");
            builder.Entity<Class>().ToTable("Class");
            builder.Entity<Course>().ToTable("Course");
            builder.Entity<Subject>().ToTable("Subject");

            builder.Entity<ApplicationUser>()
                .HasOne(p => p.Person)
                .WithOne(s => s.ApplicationUser)
                .IsRequired();

            builder.Entity<Student>()
                .HasOne(s => s.Person)
                .WithOne(p => p.Student)
                .IsRequired(false);

            builder.Entity<Employee>()
                .HasOne(s => s.Person)
                .WithOne(p => p.Employee)
                .IsRequired(false);

            builder.Entity<Employee>()
                .HasOne(e => e.Course)
                .WithOne(b => b.Employee)
                .HasForeignKey<Course>(b => b.EmployeeID);
        }


        public DbSet<PracaInz.Models.Enrollment> Enrollment { get; set; }
    }
}
