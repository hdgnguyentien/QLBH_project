using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBH_project.IRepositories;
using QLBH_project.Models;

namespace QLBH_project.Controllers
{
    public class categoriesController : Controller
    {
        private readonly CuaHangDbContext _context;
        private readonly ICategoriesRepositories _category; 
        public categoriesController(CuaHangDbContext context,ICategoriesRepositories category)
        {
            _context = context;
            _category = category;
        }

        // GET: categories
        public IActionResult Index()
        {
            var thongtin = HttpContext.Session.GetString("username");
            ViewData["thongtin"] = thongtin;

            return View( _category.GetAll());
        }

        // GET: categories/Details/5
        public IActionResult Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = _category.GetByID(id);
            if (categories == null)
            {
                return NotFound();
            }
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create( categories categories)
        {
            if (_category.Addcategories(categories))
            {
                return RedirectToAction(nameof(Index));
            }
            else return View();
        }

        // GET: categories/Edit/5
        public IActionResult Edit(Guid id)
        {
            return View(_category.GetByID(id));
        }
        [HttpPost]
        public IActionResult Edit(categories categories)
        {


            if (_category.Updatecategories(categories))
            {
                return RedirectToAction(nameof(Index));
            }
            else return BadRequest();
        }

        // GET: categories/Delete/5
        public IActionResult Delete(categories categories)
        {
            if (_category.Removecategories(categories))
            {
                return RedirectToAction("Index");
            }

            else return BadRequest();
        }

    }
}
