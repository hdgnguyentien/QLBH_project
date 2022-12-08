using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBH_project.IRepositories;
using QLBH_project.Models;

namespace QLBH_project.Controllers
{
    public class customersController : Controller
    {
        private readonly CuaHangDbContext _context;
        private readonly ICustomerRepositories _customer;
        public customersController(CuaHangDbContext context,ICustomerRepositories customer)
        {
            _context = context;
            _customer = customer;
        }

        // GET: customers
        public IActionResult Index()
        {
            return View(_customer.GetAll());
        }

        // GET: customers/Details/5
        public IActionResult Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = _customer.GetByID(id);
            if (customers == null)
            {
                return NotFound();
            }

            return View(customers);
        }

        // GET: customers/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create( customers customers)
        {
            if (_customer.Addcustomers(customers))
            {
                return RedirectToAction(nameof(Index));
            }
            else return View();
        }

        // GET: customers/Edit/5
        public IActionResult Edit(Guid id)
        {
            return View(_customer.GetByID(id));
        }
        [HttpPost]
        public IActionResult Edit(customers customers)
        {

            if (_customer.Updatecustomers(customers))
            {
                return RedirectToAction(nameof(Index));
            }
            else return BadRequest();
        }
        // GET: customers/Delete/5
        public IActionResult Delete(customers customers)
        {
            if (_customer.Removecustomers(customers))
            {
                return RedirectToAction(nameof(Index));
            }
            else return BadRequest();
        }
    }
}
