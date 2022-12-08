using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBH_project.IRepositories;
using QLBH_project.Models;
using QLBH_project.Repositories;

namespace QLBH_project.Controllers
{
    public class employeesController : Controller
    {
        private readonly CuaHangDbContext _context;
        private readonly IEmployeeRepositories _employee;
        public employeesController(CuaHangDbContext context,IEmployeeRepositories employee)
        {
            _context = context;
            _employee = employee;
        }

        // GET: employees
        public  IActionResult Index()
        {
            var thongtin = HttpContext.Session.GetString("username");
            ViewData["thongtin"] = thongtin;

            return View(_employee.GetAll());
        }
        // GET: employees/Details/5
        public IActionResult Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var employees = _employee.GetByID(id);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,roleID,Email,Password,Fullname,Address,Phone,Status")] employees employees)
        {
            if (ModelState.IsValid)
            {
                var emailDb =  _employee.GetAll().Where(x=>x.Email == employees.Email).FirstOrDefault();
                if (emailDb == null)
                {
                    _employee.Addemployees(employees);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.emailerror = "Email đã tồn tại";
                }

            }
            ViewData["roleID"] = new SelectList(_context.roles, "Id", "Rolename", employees.roleID);
            return View(employees);
        }

        // GET: employees/Edit/5
        public IActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees =  _employee.GetByID(id);
            if (employees == null)
            {
                return NotFound();
            }
            ViewData["roleID"] = new SelectList(_context.roles, "Id", "Rolename", employees.roleID);
            return View(employees);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,roleID,Email,Password,Fullname,Address,Phone,Status")] employees employees)
        {
            if (id != employees.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                    _employee.Updateemployees(employees);
                    return RedirectToAction(nameof(Index));
            }
            ViewData["roleID"] = new SelectList(_context.roles, "Id", "Rolename", employees.roleID);
            return View(employees);
        }

        // GET: employees/Delete/5
        public IActionResult Delete(employees employee)
        {
            if (_employee.Removeemployees(employee))
            {
                return RedirectToAction("Index");
            }
            return BadRequest();
        }
    }
}
