using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QLBH_project.Helpers;
using QLBH_project.IRepositories;
using QLBH_project.Models;
using QLBH_project.Repositories;
using QLBH_project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLBH_project.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductDetailRepositories _productdetails;
        public CartController(IProductDetailRepositories productDetails)
        {
            _productdetails = productDetails;
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
            CartItem cartItem = cart.Where(x => x.IdProductdetails == id).FirstOrDefault();

            if (cartItem == null)
            {
                cart.Add(new CartItem(productdetails));
            }
            else
            {
                cartItem.Quatity += 1;
            }
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToAction("Index");
        }
        public IActionResult AddtoCart(Guid id)
        {
            productdetails productdetails = _productdetails.GetByID(id);
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            CartItem cartItem = cart.Where(x => x.IdProductdetails == id).FirstOrDefault();

            if (cartItem == null)
            {
                cart.Add(new CartItem(productdetails));
            }
            else
            {
                cartItem.Quatity += 1;
            }
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return RedirectToAction("Index", "Home");
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
        public IActionResult CheckOut(Guid id)
        {
            return View();
        }
    }
}
