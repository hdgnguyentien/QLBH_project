using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBH_project.IRepositories;
using QLBH_project.Models;
using System;
using System.Linq;

namespace QLBH_project.Controllers
{
    public class AccountController : Controller
    {
        private readonly CuaHangDbContext _context;
        private readonly IEmployeeRepositories _employee;
        public AccountController(IEmployeeRepositories employee, CuaHangDbContext context)
        {
            _employee = employee;
            _context = context;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login(string email, string password)
        {
            var thongtin = HttpContext.Session.GetString("username");
            ViewData["thongtin"] = thongtin;
            if (string.IsNullOrEmpty(email) || password.Length < 6)
            {
                ViewBag.ketqua= "Hãy lòng nhập đúng email và mật khẩu tối thiểu 6 ký tự";
                return View();
            }
            else
            {
                var getUser = _employee.GetAll().Where(x => x.Email == email && x.Password == password).FirstOrDefault();
                if (getUser != null)
                {
                    HttpContext.Session.SetString("username", email);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.errorEmail = "Sai thông tin đăng nhập, đăng nhập thất bại";
                }
            }
            return View();
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("username");

            return RedirectToAction("Index");
        }
        public IActionResult Register()
        {
            ViewData["roleID"] = new SelectList(_context.roles, "Id", "Rolename");
            return View();
        }
        [HttpPost]
        public IActionResult Register(employees employees)
        {
            if (ModelState.IsValid)
            {
                employees.Id = Guid.NewGuid();
                _context.Add(employees);
                _context.SaveChanges();
                return RedirectToAction(nameof(Login));
            }
            ViewData["roleID"] = new SelectList(_context.roles, "Id", "Rolename", employees.roleID);
            return View(employees);
        }
    }
}
