using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BenifitCostDemo.Models;

namespace BenifitCostDemo.Controllers
{
    public class DependentController : Controller
    {
        private readonly HRMSContext _context;

        public DependentController(HRMSContext context)
        {
            _context = context;
        }

        // GET: Dependent
        public async Task<IActionResult> Index()
        {
            var hRMSContext = _context.dependents.Include(d => d.Employee);
            return View(await hRMSContext.ToListAsync());
        }

        // GET: Dependent/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependent = await _context.dependents
                .Include(d => d.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dependent == null)
            {
                return NotFound();
            }

            return View(dependent);
        }

        // GET: Dependent/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.employees, "EmployeeId", "EmployeeId");
            return View();
        }

        // POST: Dependent/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Dep_LastName,Dep_FirstName,Relationship,EmployeeId")] Dependent dependent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dependent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.employees, "EmployeeId", "EmployeeId", dependent.EmployeeId);
            return View(dependent);
        }

        // GET: Dependent/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependent = await _context.dependents.FindAsync(id);
            if (dependent == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.employees, "EmployeeId", "EmployeeId", dependent.EmployeeId);
            return View(dependent);
        }

        // POST: Dependent/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Dep_LastName,Dep_FirstName,Relationship,EmployeeId")] Dependent dependent)
        {
            if (id != dependent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dependent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DependentExists(dependent.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.employees, "EmployeeId", "EmployeeId", dependent.EmployeeId);
            return View(dependent);
        }

        // GET: Dependent/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependent = await _context.dependents
                .Include(d => d.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dependent == null)
            {
                return NotFound();
            }

            return View(dependent);
        }

        // POST: Dependent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dependent = await _context.dependents.FindAsync(id);
            _context.dependents.Remove(dependent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DependentExists(int id)
        {
            return _context.dependents.Any(e => e.Id == id);
        }
    }
}
