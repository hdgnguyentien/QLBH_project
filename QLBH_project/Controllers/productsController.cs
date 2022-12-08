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

namespace QLBH_project.Controllers
{
    public class productsController : Controller
    {
        private readonly CuaHangDbContext _context;
        public IProductRepositories _product;
        public productsController(CuaHangDbContext context, IProductRepositories productRepositories)
        {
            _context = context;
            _product = productRepositories;
        }

        // GET: products
        public IActionResult Index()
        {
            var thongtin = HttpContext.Session.GetString("username");
            ViewData["thongtin"] = thongtin;

            return View( _product.GetAll());
        }

        // GET: products/Details/5
        public IActionResult Details(Guid id)
        {
            return View(_product.GetByID(id));
        }

        // GET: products/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create( products products)
        {
            if (_product.Addproducts(products))
            {
                return RedirectToAction(nameof(Index));
            }
            else return View();
        }

        // GET: products/Edit/5
        public IActionResult Edit(Guid id)
        {
            return View(_product.GetByID(id));
        }
        [HttpPost]
        public IActionResult Edit( products products)
        {
            if (_product.Updateproducts(products))
            {
                return RedirectToAction(nameof(Index));
            }
            else return BadRequest();
        }

        // GET: products/Delete/5
        public IActionResult Delete(products products)
        {
            if (_product.Removeproducts(products))
            {
                return RedirectToAction(nameof(Index));
            }
            else return BadRequest();
        }
    }
}
