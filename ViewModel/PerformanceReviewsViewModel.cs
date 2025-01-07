using Employee_Management_System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Employee_Management_System.ViewModel
{
    public class PerformanceReviewsViewModel
    {
        public PerformanceReview? PerformanceReview { get; set; }

        public IEnumerable<SelectListItem>? Employees { get; set; }
    }
}
