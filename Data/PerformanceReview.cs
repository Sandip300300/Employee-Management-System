namespace Employee_Management_System.Data
{
    public class PerformanceReview
    {
        public int Id { get; set; } 

        public DateTime ReviewDate { get; set; }  = DateTime.Now;

        public int? ReviewScore { get; set; }

        public string? ReviewNotes { get; set; }

        public int? EmployeeId { get; set; }

        public Employee? Employee { get; set; }
    }
}
