using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vacation.Data;
using Vacation.Models;

namespace Vacation.Controllers
{
    public class VacationListsController : Controller
    {
        private readonly VacationDbContext _context;

        public VacationListsController(VacationDbContext context)
        {
            _context = context;
        }


        // GET: VacationLists
        public async Task<IActionResult> Index()
        {
            var vacationDbContext = _context.VacationLists.Include(v => v.Employees).Include(v => v.VacationTypes);
            return View(await vacationDbContext.ToListAsync());
        }

        // GET: VacationLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VacationLists == null)
            {
                return NotFound();
            }

            var vacationList = await _context.VacationLists
                .Include(v => v.Employees)
                .Include(v => v.VacationTypes)
                .FirstOrDefaultAsync(m => m.VacationListId == id);
            if (vacationList == null)
            {
                return NotFound();
            }

            return View(vacationList);
        }

        // GET: VacationLists/Create
        public IActionResult Create()
        {
            ViewData["FK_EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Email");
            ViewData["FK_VacationTypeId"] = new SelectList(_context.VacationTypes, "VacationTypeId", "VacationTypeName");
            return View();
        }

        // POST: VacationLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VacationListId,StartDate,EndDate,VacCreated,FK_EmployeeId,FK_VacationTypeId")] VacationList vacationList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vacationList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FK_EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Email", vacationList.FK_EmployeeId);
            ViewData["FK_VacationTypeId"] = new SelectList(_context.VacationTypes, "VacationTypeId", "VacationTypeName", vacationList.FK_VacationTypeId);
            return View(vacationList);
        }

        // GET: VacationLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VacationLists == null)
            {
                return NotFound();
            }

            var vacationList = await _context.VacationLists.FindAsync(id);
            if (vacationList == null)
            {
                return NotFound();
            }
            ViewData["FK_EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Email", vacationList.FK_EmployeeId);
            ViewData["FK_VacationTypeId"] = new SelectList(_context.VacationTypes, "VacationTypeId", "VacationTypeName", vacationList.FK_VacationTypeId);
            return View(vacationList);
        }

        // POST: VacationLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VacationListId,StartDate,EndDate,VacCreated,FK_EmployeeId,FK_VacationTypeId")] VacationList vacationList)
        {
            if (id != vacationList.VacationListId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacationList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacationListExists(vacationList.VacationListId))
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
            ViewData["FK_EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Email", vacationList.FK_EmployeeId);
            ViewData["FK_VacationTypeId"] = new SelectList(_context.VacationTypes, "VacationTypeId", "VacationTypeName", vacationList.FK_VacationTypeId);
            return View(vacationList);
        }

        // GET: VacationLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VacationLists == null)
            {
                return NotFound();
            }

            var vacationList = await _context.VacationLists
                .Include(v => v.Employees)
                .Include(v => v.VacationTypes)
                .FirstOrDefaultAsync(m => m.VacationListId == id);
            if (vacationList == null)
            {
                return NotFound();
            }

            return View(vacationList);
        }

        // POST: VacationLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VacationLists == null)
            {
                return Problem("Entity set 'VacationDbContext.VacationLists'  is null.");
            }
            var vacationList = await _context.VacationLists.FindAsync(id);
            if (vacationList != null)
            {
                _context.VacationLists.Remove(vacationList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VacationListExists(int id)
        {
          return (_context.VacationLists?.Any(e => e.VacationListId == id)).GetValueOrDefault();
        }
    }
}
