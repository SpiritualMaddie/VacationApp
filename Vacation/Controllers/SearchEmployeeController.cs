using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Vacation.Data;
using Vacation.Models;

namespace Vacation.Controllers
{
    public class SearchEmployeeController : Controller
    {
        private readonly VacationDbContext _context;

        public SearchEmployeeController(VacationDbContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }

        // Admin or no

        public async Task<IActionResult> AdminCheck(string email, string password)
        {
            var employee = _context.Employees.SingleOrDefault(e => e.Email == email && e.Password == password);

            if (employee != null && employee.Admin == true)
            {
                return RedirectToAction("Index");
                //return RedirectToAction("Index", new {loggedIn = true});
            }
            else if (employee != null && employee.Admin == false)
            {
                TempData["ErrorMessage"] = "You are not admin. \n Access denied";
                return RedirectToAction("Login");
                //return RedirectToAction("Login", new {loggedIn = false});
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid email or password.";
                return RedirectToAction("Login");
                //return RedirectToAction("Login", new {loggedIn = false});
            }
        }

        public ActionResult Index()
        {
            var employees = _context.Employees.ToList();
            ViewBag.EmployeeList = new SelectList(employees, "EmployeeId", "FullName");

            return View();
        }
        public async Task<IActionResult> Details(int? EmployeeId)
        {
            if (EmployeeId == null || _context.VacationLists == null)
            {
                TempData["ErrorMessage"] = "You have to choose an employee";
                return RedirectToAction("Index");
            }

            var vacationList = await _context.VacationLists
                .Include(v => v.Employees)
                .Include(v => v.VacationTypes)
                .Where(m => m.FK_EmployeeId == EmployeeId)
                .ToListAsync();

            if (vacationList == null)
            {
                return NotFound();
            }

            return View(vacationList);
        }
    }
}
