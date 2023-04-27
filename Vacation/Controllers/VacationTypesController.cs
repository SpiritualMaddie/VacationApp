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
    public class VacationTypesController : Controller
    {
        private readonly VacationDbContext _context;

        public VacationTypesController(VacationDbContext context)
        {
            _context = context;
        }

        // GET: VacationTypes
        public async Task<IActionResult> Index()
        {
              return _context.VacationTypes != null ? 
                          View(await _context.VacationTypes.ToListAsync()) :
                          Problem("Entity set 'VacationDbContext.VacationTypes'  is null.");
        }

        // GET: VacationTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VacationTypes == null)
            {
                return NotFound();
            }

            var vacationType = await _context.VacationTypes
                .FirstOrDefaultAsync(m => m.VacationTypeId == id);
            if (vacationType == null)
            {
                return NotFound();
            }

            return View(vacationType);
        }

        // GET: VacationTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VacationTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VacationTypeId,VacationTypeName")] VacationType vacationType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vacationType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vacationType);
        }

        // GET: VacationTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VacationTypes == null)
            {
                return NotFound();
            }

            var vacationType = await _context.VacationTypes.FindAsync(id);
            if (vacationType == null)
            {
                return NotFound();
            }
            return View(vacationType);
        }

        // POST: VacationTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VacationTypeId,VacationTypeName")] VacationType vacationType)
        {
            if (id != vacationType.VacationTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacationTypeExists(vacationType.VacationTypeId))
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
            return View(vacationType);
        }

        // GET: VacationTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VacationTypes == null)
            {
                return NotFound();
            }

            var vacationType = await _context.VacationTypes
                .FirstOrDefaultAsync(m => m.VacationTypeId == id);
            if (vacationType == null)
            {
                return NotFound();
            }

            return View(vacationType);
        }

        // POST: VacationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VacationTypes == null)
            {
                return Problem("Entity set 'VacationDbContext.VacationTypes'  is null.");
            }
            var vacationType = await _context.VacationTypes.FindAsync(id);
            if (vacationType != null)
            {
                _context.VacationTypes.Remove(vacationType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VacationTypeExists(int id)
        {
          return (_context.VacationTypes?.Any(e => e.VacationTypeId == id)).GetValueOrDefault();
        }
    }
}
