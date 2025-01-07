using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<PerformanceReview> PerformanceReviews { get; set; }

    }
}
