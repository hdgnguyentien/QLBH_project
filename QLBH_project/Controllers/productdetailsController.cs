using System;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using QLBH_project.Models;
using Microsoft.EntityFrameworkCore;
using QLBH_project.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using QLBH_project.IRepositories;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using QLBH_project.ViewModels;
using Microsoft.AspNetCore.Http;

namespace QLBH_project.Controllers
{
    public class ProductdetailsController : Controller
    {
        private readonly CuaHangDbContext _context;
        public IProductDetailRepositories _productdetails;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductdetailsController(CuaHangDbContext context, IProductDetailRepositories productdetails,
                                        IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _productdetails = productdetails;
            webHostEnvironment = hostEnvironment;
        }

        // GET: productdetails
        public IActionResult Index()
        {
            var thongtin = HttpContext.Session.GetString("username");
            ViewData["thongtin"] = thongtin;

            return View(_productdetails.GetAll());
        }

        // GET: productdetails/Details/5
        public IActionResult Details(Guid id)
        {
            return View(_productdetails.GetByID(id));
        }

        // GET: productdetails/Create
        public IActionResult Create()
        {
            ViewData["CategoriesID"] = new SelectList(_context.categories, "Id", "Name");
            ViewData["ProductId"] = new SelectList(_context.products, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,CategoriesID,Name,OriginalPrice,Price,Stock,DateCreated,LinkImage,Status")] 
                                                productdetails productdetails,
                                                ViewProductdetails viewProductdetailsImg)
        {
            if (ModelState.IsValid)
            {
                if(viewProductdetailsImg.ProductdtImage!= null)
                {
                    string folder = "image/products/";
                    folder += Guid.NewGuid().ToString() + "_" + viewProductdetailsImg.ProductdtImage.FileName;
                    viewProductdetailsImg.LinkImage = "/"+folder;

                    string severfolder = Path.Combine(webHostEnvironment.WebRootPath, folder);

                    await viewProductdetailsImg.ProductdtImage.CopyToAsync(new FileStream(severfolder, FileMode.Create));
                }
                viewProductdetailsImg.Id = Guid.NewGuid();
                productdetails.LinkImage = viewProductdetailsImg.LinkImage;
                _productdetails.Addproductdetails(productdetails);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriesID"] = new SelectList(_context.categories, "Id", "Name", viewProductdetailsImg.CategoriesID);
            ViewData["ProductId"] = new SelectList(_context.products, "Id", "Name", viewProductdetailsImg.ProductId);
            return View(productdetails);
        }
        // GET: productdetails/Edit/5
        public IActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productdetails =  _productdetails.GetByID(id);
            if (productdetails == null)
            {
                return NotFound();
            }
            ViewData["CategoriesID"] = new SelectList(_context.categories, "Id", "Name", productdetails.CategoriesID);
            ViewData["ProductId"] = new SelectList(_context.products, "Id", "Name", productdetails.ProductId);
            return View(productdetails);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id,productdetails productdetail,
                                  ViewProductdetails viewProductdetailsImg)
        {
            if (id != productdetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (viewProductdetailsImg.ProductdtImage != null)
                    {
                        string folder = "image/products/";
                        folder += Guid.NewGuid().ToString() + "_" + viewProductdetailsImg.ProductdtImage.FileName;
                        viewProductdetailsImg.LinkImage = "/" + folder;
                        string severfolder = Path.Combine(webHostEnvironment.WebRootPath, folder);
                        viewProductdetailsImg.ProductdtImage.CopyTo(new FileStream(severfolder, FileMode.Create));
                    }
                    productdetail.LinkImage = viewProductdetailsImg.LinkImage;
                    _productdetails.Updateproductdetails(productdetail);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductdetailsExists(productdetail.Id))
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
            ViewData["CategoriesID"] = new SelectList(_context.categories, "Id", "Name", viewProductdetailsImg.CategoriesID);
            ViewData["ProductId"] = new SelectList(_context.products, "Id", "Name", viewProductdetailsImg.ProductId);
            return View(productdetail);
        }
        // GET: productdetails/Delete/5
        public IActionResult Delete(productdetails productdetail)
        {
            if (_productdetails.Removeproductdetails(productdetail))
            {
                return RedirectToAction("Index");
            }
            else return BadRequest();
        }
        private bool ProductdetailsExists(Guid id)
        {
            return _context.productdetails.Any(e => e.Id == id);
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
                var result = _productdetails.GetAll().Where(p => p.Name.Contains(name)).ToList();
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
                var result = _productdetails.GetAll().Where(p => p.Name.Contains(name)).ToList();
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
        public IActionResult SearchByPrice()
        {
            return View();

        }
        [HttpGet]
        public IActionResult SearchByPrice(decimal gia1, decimal gia2)
        {
            var lst = _productdetails.GetAll();
            foreach (var item in lst)
            {
                if (lst.Where(p => p.Price >= gia1 && p.Price <= gia2).ToList().Count > 0||
                    lst.Where(p => p.Price <= gia1 && p.Price >= gia2).ToList().Count>0)
                {
                    if (gia1 < gia2)
                    {
                        if (item.Price >= gia1 && item.Price <= gia2)
                        {
                            ViewData["amount"] = "Số lượng sản phẩm tìm kiếm được là: " + lst.Where(p => p.Price >= gia1 && p.Price <= gia2).ToList().Count;

                            return View("Index", lst.Where(p => p.Price >= gia1 && p.Price <= gia2));
                        }
                    }
                    else 
                    {

                        if (item.Price <= gia1 && item.Price >= gia2)
                        {
                            ViewData["amount"] = "Số lượng sản phẩm tìm kiếm được là: " + lst.Where(p => p.Price <= gia1 && p.Price >= gia2).ToList().Count;
                            return View("Index", lst.Where(p => p.Price <= gia1 && p.Price >= gia2));
                        }
                    }
                }
                else
                {
                    ViewData["result"] = "Không có sản phẩm nào trong khoảng từ " + gia1 +" tới "+ gia2;
                }
            }

            return View("Index");
        }
    }
}
