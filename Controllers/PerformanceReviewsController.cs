using Employee_Management_System.Data;
using Employee_Management_System.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System.Controllers
{
    public class PerformanceReviewsController : Controller
    {
        private readonly EmployeeContext _context;

        public PerformanceReviewsController(EmployeeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.PerformanceReviews.Include(p => p.Employee);
            return View(await employeeContext.ToListAsync());
        }

        public IActionResult Create()
        {
            var employees = _context.Employees
                .Select(e => new { e.Id, e.Name })
                .ToList();
            if (!employees.Any())
            {
                ModelState.AddModelError("", "No Employees available.");
            }

            var employeesSelectList = employees
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                })
                .ToList();
            var viewModel = new PerformanceReviewsViewModel
            {
                Employees = employeesSelectList
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PerformanceReviewsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.PerformanceReview == null) {
                    return NotFound();
                }
                 
                _context.Add(viewModel.PerformanceReview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performanceReview = await _context.PerformanceReviews.FindAsync(id);
            if (performanceReview == null)
            {
                return NotFound();
            }

            var viewModel = new PerformanceReviewsViewModel
            {
                PerformanceReview = performanceReview,
                Employees = _context.Employees
                   .Select(d => new SelectListItem
                   {
                       Value = d.Id.ToString(),
                       Text = d.Name
                   })
                   .ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PerformanceReviewsViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if(viewModel.PerformanceReview == null)
                    {
                        return NotFound();
                    }
                    _context.Update(viewModel.PerformanceReview);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerformanceReviewExists(viewModel.PerformanceReview.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performanceReview = await _context.PerformanceReviews
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (performanceReview == null)
            {
                return NotFound();
            }

            _context.PerformanceReviews.Remove(performanceReview);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "PerformanceReviews deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var performanceReview = await _context.PerformanceReviews.FindAsync(id);
            if (performanceReview != null)
            {
                _context.PerformanceReviews.Remove(performanceReview);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerformanceReviewExists(int id)
        {
            return _context.PerformanceReviews.Any(e => e.Id == id);
        }
    }
}
