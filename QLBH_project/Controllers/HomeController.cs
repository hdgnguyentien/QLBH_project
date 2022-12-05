﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QLBH_project.Helpers;
using QLBH_project.IRepositories;
using QLBH_project.Models;
using QLBH_project.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace QLBH_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProductDetailRepositories _productdetails;
        private IOrderRepositories _order;
        private IOrderDetailRepositories _orderdetail;
        private IEmployeeRepositories _employee;
       
        public HomeController(ILogger<HomeController> logger,
                              IProductDetailRepositories productdetails,
                              IOrderRepositories order,
                              IOrderDetailRepositories orderdetail,
                              IEmployeeRepositories employee
                               )
        {
            _logger = logger;
            _productdetails = productdetails;
            _order = order;
            _orderdetail = orderdetail;
            _employee = employee;
        }
        public IActionResult HienThiDonHang()
        {
            return View(_order.GetAll());
        }
        public IActionResult Chitietdonhang()
        {
            return View(_orderdetail.GetAll());
        }
        public IActionResult Index(Guid id)
        {
            var thongtin = HttpContext.Session.GetString("username");
            var result = _productdetails.GetAll();
            //HttpContext.Session.SetString("id_productdetails", id.ToString());
            if (thongtin != null)
            { 
                ViewData["thongtin"] = thongtin;
                return View(result);// truyền dư liệu đăng nhập từ session 
            }
            return View(result);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
