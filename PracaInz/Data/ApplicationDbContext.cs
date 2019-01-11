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
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Presence> Presence { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Student>().ToTable("Student");
            builder.Entity<Employee>().ToTable("Employee");
            builder.Entity<Person>().ToTable("Person");
            builder.Entity<Class>().ToTable("Class");
            builder.Entity<Course>().ToTable("Course");
            builder.Entity<Subject>().ToTable("Subject");
            builder.Entity<Enrollment>().ToTable("Enrollment");
            builder.Entity<Grade>().ToTable("Grade");
            builder.Entity<Presence>().ToTable("Presence");

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


            builder.Entity<Course>()
                .HasIndex(c => c.EmployeeID)
                .IsUnique(false);


            //builder.Entity<Course>()
            //    .HasOne(c => c.Employee)
            //    .WithMany(e => e.Courses)
            //    .HasForeignKey(c => c.CourseID);


            builder.Entity<Presence>()
                .HasOne(b => b.Course)
                .WithMany(b => b.Presence)
                .OnDelete(DeleteBehavior.Cascade);



        }
    }
}
