using QLBH_project.Models;
using System;

namespace QLBH_project.ViewModels
{
    public class CartItem
    {
        public Guid IdProductdetails { get; set; }
        public string ProductdetailsName { get; set; }
        public int Quatity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get { return Quatity * Price; } }
        public string Image { get; set; }
        public CartItem() { }
        public  CartItem (productdetails productdetails)
        {
            IdProductdetails = productdetails.Id;
            ProductdetailsName = productdetails.Name;
            Quatity = 1;
            Price = productdetails.Price;
            Image = productdetails.LinkImage;
        }
    }
}
