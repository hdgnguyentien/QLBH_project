using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBH_project.Models;
using QLBH_project.Repositories;

namespace QLBH_project.Controllers
{
    public class employeesController : Controller
    {
        private readonly CuaHangDbContext _context;
        public EmployeeRepositories _employeeRepositories;
        public employeesController(CuaHangDbContext context, EmployeeRepositories employeeRepositories)
        {
            _context = context;
            _employeeRepositories = employeeRepositories;
        }

        // GET: employees
        public async Task<IActionResult> Index()
        {
            var cuaHangDbContext = _context.employees.Include(e => e.roles);
            return View(await cuaHangDbContext.ToListAsync());
        }

        // GET: employees/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.employees
                .Include(e => e.roles)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        // GET: employees/Create
        public IActionResult Create()
        {
            ViewData["roleID"] = new SelectList(_context.roles, "Id", "Rolename");
            return View();
        }

        // POST: employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,roleID,Email,Password,Fullname,Address,Phone,Status")] employees employees)
        {
            if (ModelState.IsValid)
            {
                employees.Id = Guid.NewGuid();
                _context.Add(employees);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["roleID"] = new SelectList(_context.roles, "Id", "Rolename", employees.roleID);
            return View(employees);
        }

        // GET: employees/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.employees.FindAsync(id);
            if (employees == null)
            {
                return NotFound();
            }
            ViewData["roleID"] = new SelectList(_context.roles, "Id", "Rolename", employees.roleID);
            return View(employees);
        }

        // POST: employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,roleID,Email,Password,Fullname,Address,Phone,Status")] employees employees)
        {
            if (id != employees.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employees);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!employeesExists(employees.Id))
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
            ViewData["roleID"] = new SelectList(_context.roles, "Id", "Rolename", employees.roleID);
            return View(employees);
        }

        // GET: employees/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.employees
                .Include(e => e.roles)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        // POST: employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var employees = await _context.employees.FindAsync(id);
            _context.employees.Remove(employees);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool employeesExists(Guid id)
        {
            return _context.employees.Any(e => e.Id == id);
        }
    }
}
