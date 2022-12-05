using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBH_project.Helpers;
using QLBH_project.IRepositories;
using QLBH_project.Models;
using QLBH_project.Repositories;
using QLBH_project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QLBH_project.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductDetailRepositories _productdetails;
        private readonly ICustomerRepositories _customer;
        private readonly IOrderRepositories _order;
        private readonly IOrderDetailRepositories _orderdetail;
        private readonly IEmployeeRepositories _employee;
        public CartController(IProductDetailRepositories productDetails, 
                              ICustomerRepositories customer, 
                              IOrderRepositories order,
                              IOrderDetailRepositories orderdetail,
                              IEmployeeRepositories employee)
        {
            _productdetails = productDetails;
            _customer = customer;
            _order = order;
            _orderdetail = orderdetail;
            _employee = employee;
        }
        public IActionResult Index()
        {
            var thongtin = HttpContext.Session.GetString("username");
            
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            ViewCart viewCart = new ViewCart
            {
                cartItems = cart,
                grandtotal = cart.Sum(x => x.Price * x.Quatity)
            };


            ViewData["thongtin"] = thongtin;
            return View(viewCart);
        }
        public  IActionResult Add(Guid id)
        {
            productdetails productdetails =  _productdetails.GetByID(id);
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ??new List<CartItem>();
            var idProdetail = _productdetails.GetAll().FirstOrDefault(x=>x.Id == id);
            CartItem cartItem = cart.FirstOrDefault(x => x.IdProductdetails == id);         
                if (cartItem == null)
                {
                    cart.Add(new CartItem(productdetails));
                }
                else
                {                
                    if (idProdetail.Stock == cartItem.Quatity)
                    {
                        ViewBag.errorStock = "Vượt quá số lượng cho phép";
                        return RedirectToAction("AddtoCart");
                    }
                    else
                    {
                        cartItem.Quatity += 1;
                    }   
                }
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            return RedirectToAction("Index");
        }
        public IActionResult AddtoCart(Guid id)
        {
            productdetails productdetails = _productdetails.GetByID(id);
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var idProdetail = _productdetails.GetAll().FirstOrDefault(x => x.Id == id);
            CartItem cartItem = cart.Where(x=>x.IdProductdetails == id).FirstOrDefault();
            if (cartItem == null)
            {
                cart.Add(new CartItem(productdetails));
            }
            else
            {
                if (idProdetail.Stock == cartItem.Quatity)
                {
                    ViewBag.errorStock = "Vượt quá số lượng cho phép";
                    return View();
                }
                else
                {
                    cartItem.Quatity += 1;
                }
            }
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult CheckOut(Guid id)
        {
            var thongtin = HttpContext.Session.GetString("username");
            ViewData["thongtin"] = thongtin;
            if (thongtin == null)
            {
                RedirectToAction("Index");
            }
            else
            {
                List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
                ViewCart viewCart = new ViewCart
                {
                    cartItems = cart,
                    grandtotal = cart.Sum(x => x.Price * x.Quatity)
                };
                return View(viewCart);
            }
            return RedirectToAction("Login", "Account");
        }
        public IActionResult CreateCustomer()
        {
            return RedirectToAction("Create", "customers");
        }
        public IActionResult Thanhtoan(Guid id,string phone,string name,string address)
        {
            try
            {
                HttpContext.Session.SetString("phone", phone);
                HttpContext.Session.SetString("name", name);
                HttpContext.Session.SetString("address", address);
                var phoneCus = HttpContext.Session.GetString("phone");
                var nameCus = HttpContext.Session.GetString("name");
                var addressCus = HttpContext.Session.GetString("address");
                var thongtin = HttpContext.Session.GetString("username");
                List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");

                ViewData["phone"] = phoneCus;

                var idCustomer = _customer.GetAll().Where(x => x.Phone == phoneCus).FirstOrDefault();
                var idEmployee = _employee.GetAll().Where(x => x.Email == thongtin).FirstOrDefault();
                var idProdetail = _productdetails.GetAll().Where(x => x.Id == id).Select(x=>x.Stock < 0).FirstOrDefault();

                    if (idCustomer == null)
                    {
                        customers customer = new customers
                        {
                            Id = Guid.NewGuid(),
                            Phone = phoneCus,
                            Name = nameCus,
                            Address = addressCus
                        };
                        _customer.Addcustomers(customer);
                        orders order = new orders
                        {
                            Id = Guid.NewGuid(),
                            EmployeeId = idEmployee.Id,
                            CustomerId = customer.Id,
                            DateCreate = DateTime.Now,
                            TotalPrice = cart.Sum(x => x.Quatity * x.Price),
                            Status = true
                        };
                        _order.Addorders(order);
                        foreach (var item in cart)
                        {
                            orderdetails orderdetail = new orderdetails
                            {
                                Id = Guid.NewGuid(),
                                ProductDetailID = item.IdProductdetails,
                                OrderId = order.Id,
                                Stock = item.Quatity,
                                Price = item.Price,
                                TotalPrice = item.Total
                            };
                            _orderdetail.Addorderdetails(orderdetail);
                            var pdetail = _productdetails.GetAll().FirstOrDefault(x => x.Id == item.IdProductdetails);
                            pdetail.Stock -= item.Quatity;
                            _productdetails.Updateproductdetails(pdetail);
                        }
                        HttpContext.Session.Remove("Cart");
                        return View();
                    }
                    else
                    {
                        orders order = new orders
                        {
                            Id = Guid.NewGuid(),
                            EmployeeId = idEmployee.Id,
                            CustomerId = idCustomer.Id,
                            DateCreate = DateTime.Now,
                            TotalPrice = cart.Sum(x => x.Quatity * x.Price),
                            Status = true
                        };
                        _order.Addorders(order);
                        foreach (var item in cart)
                        {
                            orderdetails orderdetail = new orderdetails
                            {
                                Id = Guid.NewGuid(),
                                ProductDetailID = item.IdProductdetails,
                                OrderId = order.Id,
                                Stock = item.Quatity,
                                Price = item.Price,
                                TotalPrice = item.Total
                            };
                            _orderdetail.Addorderdetails(orderdetail);
                            var pdetail = _productdetails.GetAll().FirstOrDefault(x => x.Id == item.IdProductdetails);
                            pdetail.Stock -= item.Quatity;
                            _productdetails.Updateproductdetails(pdetail);
                        }
                        HttpContext.Session.Remove("Cart");
                        return View();
                    }
                return View();

            }
               
            catch
            {
                return Content("Lỗi rồi");
            }

        }
        public IActionResult Tru(Guid id)
        {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");
            CartItem cartItem = cart.Where(x => x.IdProductdetails == id).FirstOrDefault();

            if (cartItem.Quatity >1)
            {
                --cartItem.Quatity;
            }
            else
            {
                cart.RemoveAll(x => x.IdProductdetails == id);
            }
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }

            return RedirectToAction("Index");
        }
        public IActionResult Remove(Guid id)
        {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");
            cart.RemoveAll(x => x.IdProductdetails == id);
            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }

            return RedirectToAction("Index");
        }
        public IActionResult Clear(Guid id)
        {
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index");
        }
    }
}
