﻿using Employee_Management_System.Data;
using Employee_Management_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Employee_Management_System.ViewModel
{
    public class EmployeeViewModel
    {
        public Employee? Employee { get; set; }

        public IEnumerable<SelectListItem>? Departments { get; set; }

    }
}
