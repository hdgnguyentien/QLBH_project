using System;
using System.Collections.Generic;

namespace QLBH_project.Models
{
    public class productdetails
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid CategoriesID { get; set; }
        public string Name { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime DateCreated { get; set; }
        public string? LinkImage { get; set; }
        public bool Status { get; set; }
        public List<cart> carts { get; set; }
        public products products { get; set; }
        public categories categories { get; set; }
        public List<orderdetails> orderdetails { get; set; }
    }
}
