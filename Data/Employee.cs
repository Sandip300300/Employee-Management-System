namespace Employee_Management_System.Data
{
    public class Employee
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Position { get; set; } 

        public DateTime JoiningDate { get; set; } = DateTime.Now;

        public int? DepartmentId { get; set; }

        public Department? Department { get; set; }
    }
}
