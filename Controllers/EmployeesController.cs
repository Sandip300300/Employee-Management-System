using Employee_Management_System.Data;
using Employee_Management_System.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeContext _context;

        public EmployeesController(EmployeeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var employee = await _context.Employees.Include(x=>x.Department).ToListAsync();

            return View(employee);
        }

        public IActionResult Create()
        {
            var departments = _context.Departments
                .Select(d => new { d.Id, d.DepartmentName })
                .ToList();

            if (!departments.Any())
            {
                ModelState.AddModelError("", "No departments available.");
            }

            var departmentSelectList = departments
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.DepartmentName
                })
                .ToList();


            var viewModel = new EmployeeViewModel
            {
                Departments = departmentSelectList
            };

            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var employee = viewModel.Employee;
                if (employee == null)
                {
                    ModelState.AddModelError("", "Employee data is missing.");
                    return View(viewModel);
                }
                 _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var employee = await _context.Employees
                                          .Include(x => x.Department)
                                          .FirstOrDefaultAsync(x => x.Id == id);

            if (employee == null)
            {
                return NotFound();
            }
            var viewModel = new EmployeeViewModel
            {
                Employee = employee,
                Departments = _context.Departments
                    .Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = d.DepartmentName
                    })
                    .ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var employee = viewModel.Employee;
                    if (employee == null)
                    {
                        ModelState.AddModelError("", "Employee data is missing.");
                        return View(viewModel);
                    }
                     _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(viewModel.Employee.Id))
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

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
