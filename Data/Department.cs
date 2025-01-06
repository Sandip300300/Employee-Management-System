namespace Employee_Management_System.Data
{
    public class Department
    {
        public int Id { get; set; }

        public string? DepartmentName { get; set; }

        public decimal? Budget { get; set; }

        public int? ManagerId { get; set; }

        public List<Employee>? Manager { get; set; }

    }
}
