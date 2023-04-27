using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Vacation.Data;
using Vacation.Models;

namespace Vacation.Controllers
{
    public class SearchVacationController : Controller
    {
        private readonly VacationDbContext _context;

        public SearchVacationController(VacationDbContext context)
        {
            _context = context;
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

        public IActionResult Login()
        {
            return View();
        }
        public async Task<ActionResult> Index()
        {

            // Employees
            var employees = _context.Employees.ToList();
            ViewBag.EmployeeList = new SelectList(employees, "EmployeeId", "FullName");

            // Vacations/Leaves of absence
            var vacationList = await _context.VacationLists.ToListAsync();
            var vacationMonths = vacationList.Select(v => new { VacCreated = v.VacCreated.ToString("MMMM yyyy"), MonthDate = new DateTime(v.VacCreated.Year, v.VacCreated.Month, 1) })
                                               .OrderByDescending(x => x.MonthDate)
                                               .GroupBy(x => x.VacCreated)
                                               .Select(x => x.First())
                                               .ToList();
            ViewBag.VacationVBList = new SelectList(vacationMonths, "MonthDate", "VacCreated");


            return View();
        }
 

        public async Task<IActionResult> DetailsEmployee(int? EmployeeId)
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

        public async Task<IActionResult> DetailsMonth(DateTime? month)
        {
            if (month == null)
            {
                TempData["ErrorMessage"] = "You have to choose a month";
                return RedirectToAction("Index");
            }

            var vacMonth = await _context.VacationLists
                .Include(v => v.Employees)
                .Include(v => v.VacationTypes)
                .Where(m => m.VacCreated.Month == month.Value.Month)
                .ToListAsync();

            if (vacMonth == null)
            {
                return NotFound();
            }

            return View(vacMonth);
        }
    }
}
