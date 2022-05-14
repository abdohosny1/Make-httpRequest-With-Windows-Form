using CourseApiDayOne.Models;
using DayOne.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseApiDayOne
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
    }
}
