using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QLBH_project.IRepositories;
using QLBH_project.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;

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
                ViewData["ketqua"] = "Hãy nhập đúng email và mật khẩu tối thiểu 6 ký tự";
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
        public IActionResult Resetpassword(string emailSend)
        {
            Random rd = new Random();
            int rdom = rd.Next(1, 999999);
            var idEmployee = _employee.GetAll().FirstOrDefault(x => x.Email == emailSend);
            if(idEmployee == null)
            {
                return View();
            }
            else
            {
                MailAddress fromAddress = new MailAddress("tienncph18949@fpt.edu.vn", "Admin");
                MailAddress toAddress = new MailAddress(emailSend, "User");
                const string subject = "Reset mật khẩu";
                string body = "Bạn đã yêu cầu đổi mật khẩu. Mật khẩu mới của bạn là: " + rdom;

                MailMessage msg = new MailMessage(fromAddress.Address, toAddress.Address, subject, body);
                msg.IsBodyHtml = true;

                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("tienncph18949@fpt.edu.vn", "gvmftxjtyzjxjkwj"),
                    EnableSsl = true
                };

                try
                {
                    idEmployee.Password = rdom.ToString();
                    _employee.Updateemployees(idEmployee);
                    client.Send(msg);
                    ViewBag.oke = "Send mail oke";
                    return View();
                }
                catch (Exception ex)
                {
                    return Content("Error "+ex);
                }
            }
            
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
            var emailDb = _employee.GetAll().Where(x => x.Email == employees.Email).FirstOrDefault();
            if (emailDb == null)
            {
                _employee.Addemployees(employees);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.emailerror = "Email đã tồn tại";
            }
            ViewData["roleID"] = new SelectList(_context.roles, "Id", "Rolename", employees.roleID);
            return View(employees);
        }
    }
}
