using Employee_Management_System.Data;
using Employee_Management_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Employee_Management_System.ViewModel
{
    public class EmployeeListViewModel
    {
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public PagingInfo PagingInfo { get; set; }
    }
}
