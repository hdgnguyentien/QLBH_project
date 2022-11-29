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
    public class productsController : Controller
    {
        private readonly CuaHangDbContext _context;
        public IProductRepositories _productRepositories;
        public productsController(CuaHangDbContext context, IProductRepositories productRepositories)
        {
            _context = context;
            _productRepositories = productRepositories;
        }

        // GET: products
        public async Task<IActionResult> Index()
        {
            return View(await _context.products.ToListAsync());
        }

        // GET: products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Status")] products products)
        {
            if (ModelState.IsValid)
            {
                products.Id = Guid.NewGuid();
                _context.Add(products);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        // GET: products/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }

        // POST: products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Status")] products products)
        {
            if (id != products.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!productsExists(products.Id))
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
            return View(products);
        }

        // GET: products/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var products = await _context.products.FindAsync(id);
            _context.products.Remove(products);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool productsExists(Guid id)
        {
            return _context.products.Any(e => e.Id == id);
        }
        public IActionResult SearchByName()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SearchByName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var result = _productRepositories.GetAll().Where(p => p.Name.Contains(name)).ToList();
                if (result.Count > 0)
                {
                    return View("Index", result);
                }
                else
                {
                    ViewData["result"] = "Không có sản phẩm nào tên " + name;
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult SearchByName2(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var result = _productRepositories.GetAll().Where(p => p.Name.Contains(name)).ToList();
                if (result.Count > 0)
                {
                    return View("Index", result);
                }
                else
                {
                    ViewData["result"] = "Không có sản phẩm nào tên " + name;
                }
            }
            return View("Index");
        }
    }
}
